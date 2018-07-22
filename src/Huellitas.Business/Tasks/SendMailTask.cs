//-----------------------------------------------------------------------
// <copyright file="SendMailTask.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Tasks
{
    using System;
    using System.Linq;
    using Beto.Core.Data;
    using Huellitas.Business.Configuration;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Services;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;
    using MailKit.Net.Smtp;
    using MimeKit;
    using MimeKit.Text;

    /// <summary>
    /// Send the pending emails
    /// </summary>
    public class SendMailTask : ITask
    {
        /// <summary>
        /// The log service
        /// </summary>
        private readonly ILogService logService;

        /// <summary>
        /// The notification service
        /// </summary>
        private readonly IRepository<EmailNotification> notificationRepository;

        /// <summary>
        /// The notification settings
        /// </summary>
        private readonly INotificationSettings notificationSettings;

        /// <summary>
        /// The task settings
        /// </summary>
        private readonly ITaskSettings taskSettings;

        public SendMailTask(
            IRepository<EmailNotification> notificationRepository,
            ITaskSettings taskSettings,
            ILogService logService,
            INotificationSettings notificationSettings)
        {
            this.notificationRepository = notificationRepository;
            this.taskSettings = taskSettings;
            this.logService = logService;
            this.notificationSettings = notificationSettings;
        }

        /// <summary>
        /// Send the pending mails
        /// </summary>
        public void SendPendingMails()
        {
            var mails = this.notificationRepository.Table
                .Where(c => c.SentDate == null && c.SentTries < this.notificationSettings.MaxAttemtpsToSendEmail)
                .Take(this.notificationSettings.TakeEmailsToSend)
                .ToList();

            try
            {
                foreach (EmailNotification mail in mails)
                {
                    try
                    {
                        this.SendMessage(mail);
                        mail.SentDate = DateTime.Now;
                    }
                    catch (Exception e)
                    {
                        mail.SentTries++;
                        this.logService.Error(e);
                    }
                }
            }
            catch (Exception e)
            {
                this.logService.Error(e);
            }
            finally
            {
                if (mails.Count > 0)
                {
                    this.notificationRepository.Update(mails);
                }
            }
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="notification">The notification.</param>
        private void SendMessage(EmailNotification notification)
        {
            if (this.notificationSettings.SendEmailEnabled)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(this.notificationSettings.EmailSenderName, this.notificationSettings.EmailSenderEmail));
                message.To.Add(new MailboxAddress(notification.ToName, notification.To));
                message.Subject = notification.Subject;
                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = notification.Body
                };

                using (var client = new SmtpClient())
                {
                    //client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect(
                        this.notificationSettings.SmtpHost,
                        this.notificationSettings.SmtpPort,
                        this.notificationSettings.SmtpUseSsl);

                    //client.AuthenticationMechanisms.Remove("XOAUTH2");

                    client.Authenticate(this.notificationSettings.SmtpUser, this.notificationSettings.SmtpPassword);

                    client.Send(message);

                    client.Disconnect(true);
                }
            }
        }
    }
}