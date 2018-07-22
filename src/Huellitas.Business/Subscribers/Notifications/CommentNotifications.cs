//-----------------------------------------------------------------------
// <copyright file="CommentNotifications.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Subscribers.Notifications
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Beto.Core.Data.Notifications;
    using Beto.Core.EventPublisher;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Comment notifications
    /// </summary>
    /// <seealso cref="Huellitas.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityInsertedMessage{Huellitas.Data.Entities.Comment}}" />
    public class CommentNotifications : ISubscriber<EntityInsertedMessage<Comment>>
    {
        /// <summary>
        /// The comment repository
        /// </summary>
        private readonly IRepository<Comment> commentRepository;

        /// <summary>
        /// The comment service
        /// </summary>
        private readonly ICommentService commentService;

        /// <summary>
        /// The content service
        /// </summary>
        private readonly IContentService contentService;

        /// <summary>
        /// The notification service
        /// </summary>
        private readonly INotificationService notificationService;

        /// <summary>
        /// The seo service
        /// </summary>
        private readonly ISeoService seoService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentNotifications"/> class.
        /// </summary>
        /// <param name="contentService">The content service.</param>
        /// <param name="commentService">The comment service.</param>
        /// <param name="seoService">The seo service.</param>
        /// <param name="notificationService">The notification service.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="commentRepository">The comment repository.</param>
        public CommentNotifications(
            IContentService contentService,
            ICommentService commentService,
            ISeoService seoService,
            INotificationService notificationService,
            IWorkContext workContext,
            IRepository<Comment> commentRepository)
        {
            this.contentService = contentService;
            this.commentService = commentService;
            this.seoService = seoService;
            this.notificationService = notificationService;
            this.workContext = workContext;
            this.commentRepository = commentRepository;
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task HandleEvent(EntityInsertedMessage<Comment> message)
        {
            if (message.Entity.ContentId.HasValue)
            {
                await this.NotifyCommentOnContent(message.Entity);
            }
            else
            {
                await this.NotifySubcomment(message.Entity);
            }
        }

        /// <summary>
        /// Notifies the content of the comment on.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>the task</returns>
        private async Task NotifyCommentOnContent(Comment comment)
        {
            Content content = null;

            if (comment.Content == null)
            {
                content = this.contentService.GetById(comment.ContentId.Value);
            }

            var contentUrl = this.seoService.GetContentUrl(content);

            var parameters = new List<NotificationParameter>();
            parameters.Add("Content.Name", content.Name);
            parameters.Add("Content.Url", contentUrl);

            await this.notificationService.NewNotification(
                content.User,
                this.workContext.CurrentUser,
                NotificationType.NewCommentOnContent,
                contentUrl,
                parameters);
        }

        /// <summary>
        /// Notifies the subcomment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>the task</returns>
        private async Task NotifySubcomment(Comment comment)
        {
            var parentComment = this.commentService.GetById(comment.ParentCommentId.Value, getContent: true, getUser: true);

            var contentUrl = this.seoService.GetContentUrl(parentComment.Content);

            var parameters = new List<NotificationParameter>();
            parameters.Add("Content.Name", parentComment.Content.Name);
            parameters.Add("Content.Url", contentUrl);

            if (parentComment.UserId != this.workContext.CurrentUserId)
            {
                ////Notifica al dueño del comentario padre
                await this.notificationService.NewNotification(
                    parentComment.User,
                    this.workContext.CurrentUser,
                    NotificationType.NewSubcommentOnMyComment,
                    contentUrl,
                    parameters);
            }

            ////Notifica a los otros que comentaron
            var others = this.commentRepository.Table
                .Include(c => c.User)
                .Where(c => c.ParentCommentId == parentComment.Id && c.UserId != parentComment.UserId && c.UserId != this.workContext.CurrentUserId)
                .Select(c => c.User)
                .Distinct()
                .ToList();

            await this.notificationService.NewNotification(
                others,
                this.workContext.CurrentUser,
                NotificationType.NewSubcommentOnSomeoneElseComment,
                contentUrl,
                parameters);
        }
    }
}