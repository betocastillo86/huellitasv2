//-----------------------------------------------------------------------
// <copyright file="SeedingSettings.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Migrations
{
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Seeding Settings
    /// </summary>
    public static class SeedingSettings
    {
        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void Seed(HuellitasContext context)
        {
            var list = new List<SystemSetting>();

            list.Add(new SystemSetting() { Name = "SecuritySettings.AuthenticationAudience", Value = "AudienceAuthentication" });
            list.Add(new SystemSetting() { Name = "SecuritySettings.AuthenticationIssuer", Value = "AuthenticationIssuer" });
            list.Add(new SystemSetting() { Name = "SecuritySettings.AuthenticationSecretKey", Value = "TheSecretKey132456789" });
            list.Add(new SystemSetting() { Name = "SecuritySettings.ExpirationTokenMinutes", Value = "60" });

            list.Add(new SystemSetting() { Name = "ContentSettings.PictureSizeWidthDetail", Value = "1000" });
            list.Add(new SystemSetting() { Name = "ContentSettings.PictureSizeHeightDetail", Value = "666" });
            list.Add(new SystemSetting() { Name = "ContentSettings.PictureSizeWidthList", Value = "345" });
            list.Add(new SystemSetting() { Name = "ContentSettings.PictureSizeHeightList", Value = "230" });
            list.Add(new SystemSetting() { Name = "ContentSettings.DaysToAutoClosingPet", Value = "30" });

            list.Add(new SystemSetting() { Name = "GeneralSettings.SiteUrl", Value = "http://localhost:8609/" });

            list.Add(new SystemSetting() { Name = "NotificationSettings.BodyBaseHtml", Value = "<html> <body style=\"background: #F6F6F7;\">     <table width=\"600\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\" style=\"background:#FFF; font-family: sans-serif; color: #666666;\">         <tbody>             <tr style=\"height:30px\">                 <td colspan=\"3\">&nbsp;</td>             </tr>             <tr>                 <td style=\"width: 30px;\">&nbsp;</td>                 <td>                     <img src=\"%%RootUrl%%img/front/logomail.png\" alt=\"huellitas sin hogar\" style=\"display:block; margin: 0 auto;\" />                     %%Body%% 					<div style=\"text-align: center; margin: 50px 0 0;\">                         <a href=\"https://www.facebook.com/huellitas.social/\" target=\"_blank\" style=\"margin: 0 5px;\"><img src=\"%%RootUrl%%img/front/fb.png\" alt=\"facebook huellitas sin hogar\" width=\"40\" height=\"40\" class=\"CToWUd\"></a>                         <a href=\"https://www.instagram.com/huellitas.social/\" target=\"_blank\" style=\"margin: 0 5px;\"><img src=\"%%RootUrl%%img/front/twt.png\" alt=\"instagram huellitas sin hogar\" width=\"40\" height=\"40\" class=\"CToWUd\"></a>                     </div>                 </td>                 <td style=\"width: 30px;\">&nbsp;</td>             </tr>             <tr style=\"height:40px\">                 <td colspan=\"3\">&nbsp;</td>             </tr>         </tbody>     </table> </body> </html>" });
            list.Add(new SystemSetting() { Name = "NotificationSettings.EmailSenderName", Value = "Huellitas sin hogar" });

            list.Add(new SystemSetting() { Name = "GeneralSettings.DefaultPageSize", Value = "10" });

            list.Add(new SystemSetting() { Name = "GeneralSettings.BannerPictureSizeWidth", Value = "1500" });
            list.Add(new SystemSetting() { Name = "GeneralSettings.BannerPictureSizeHeight", Value = "1159" });

            list.Add(new SystemSetting() { Name = "GeneralSettings.FacebookPublicToken", Value = string.Empty });
            list.Add(new SystemSetting() { Name = "GeneralSettings.FacebookSecretToken", Value = string.Empty });

            list.Add(new SystemSetting() { Name = "GeneralSettings.SeoImage", Value = "/img/front/porque-adoptar-una-mascota.png" });

            list.Add(new SystemSetting() { Name = "SecuritySettings.MaxRequestFileUploadMB", Value = "5" });

            list.Add(new SystemSetting() { Name = "NotificationSettings.TakeEmailsToSend", Value = "30" });
            list.Add(new SystemSetting() { Name = "NotificationSettings.MaxAttemtpsToSendEmail", Value = "5" });

            list.Add(new SystemSetting() { Name = "NotificationSettings.EmailSenderEmail", Value = "elemail@elemail.com" });
            list.Add(new SystemSetting() { Name = "NotificationSettings.SmtpUser", Value = "user@smtp.com" });
            list.Add(new SystemSetting() { Name = "NotificationSettings.SmtpPassword", Value = "password@smtp.com" });
            list.Add(new SystemSetting() { Name = "NotificationSettings.SmtpPort", Value = "587" });
            list.Add(new SystemSetting() { Name = "NotificationSettings.SmtpUseSsl", Value = "True" });
            list.Add(new SystemSetting() { Name = "NotificationSettings.SendEmailEnabled", Value = "False" });
            list.Add(new SystemSetting() { Name = "TaskSettings.SendEmailsInterval", Value = "30" });
            list.Add(new SystemSetting() { Name = "NotificationSettings.SmtpHost", Value = "elhostemail" });
            list.Add(new SystemSetting() { Name = "SecuritySettings.TrackHomeRequests", Value = "False" });
            list.Add(new SystemSetting() { Name = "GeneralSettings.GoogleAnalyticsCode", Value = string.Empty });

            list.Add(new SystemSetting() { Name = "GeneralSettings.PostImageWidthFacebook", Value = "1200" });
            list.Add(new SystemSetting() { Name = "GeneralSettings.PostImageHeightFacebook", Value = "900" });
            list.Add(new SystemSetting() { Name = "GeneralSettings.PostImageWidthInstagram", Value = "1080" });
            list.Add(new SystemSetting() { Name = "GeneralSettings.PostImageHeightInstagram", Value = "1080" });

            list.Add(new SystemSetting() { Name = "RedirectionSettings.Dame-Vida-ORG", Value = "organizacion-dame-vida-tenjo" });
            list.Add(new SystemSetting() { Name = "RedirectionSettings.Hogar-de-paso-Perro-Amor", Value = "hogar-de-paso-perro-amor-cota" });

            list.Add(new SystemSetting() { Name = "GeneralSettings.MaxHeightPictureSize", Value = "1500" });
            list.Add(new SystemSetting() { Name = "GeneralSettings.MaxWidthPictureSize", Value = "1500" });

            list.Add(new SystemSetting() { Name = "GeneralSettings.AdsenseEnabled", Value = "False" });

            list.Add(new SystemSetting() { Name = "NotificationSettings.SmsKey", Value = "False" });
            list.Add(new SystemSetting() { Name = "NotificationSettings.SmsMessage", Value = "Llenaron un formulario de adopción por %%Pet.Name%%. Respondelo aquí %%Url%%" });
            list.Add(new SystemSetting() { Name = "NotificationSettings.SendSmsEnabled", Value = "False" });
            list.Add(new SystemSetting() { Name = "NotificationSettings.SmsCountryCode", Value = "57" });
            

            foreach (var item in list)
            {
                if (!context.SystemSettings.Any(c => c.Name.Equals(item.Name)))
                {
                    context.SystemSettings.Add(item);
                }
            }

            context.SaveChanges();
        }
    }
}