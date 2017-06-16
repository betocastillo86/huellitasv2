namespace Huellitas.Data.Migrations
{
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;

    public static class SeedingSettings
    {
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

            list.Add(new SystemSetting() { Name = "GeneralSettings.SiteUrl", Value = "http://localhost:23178/" });

            list.Add(new SystemSetting() { Name = "NotificationSettings.BodyBaseHtml", Value = "<html><body><h1>Huellitas sin Hogar</h2>%%Body%%</body></html>" });
            list.Add(new SystemSetting() { Name = "NotificationSettings.EmailSenderName", Value = "Huellitas sin hogar" });

            list.Add(new SystemSetting() { Name = "GeneralSettings.DefaultPageSize", Value = "10" });

            list.Add(new SystemSetting() { Name = "GeneralSettings.BannerPictureSizeWidth", Value = "1500" });
            list.Add(new SystemSetting() { Name = "GeneralSettings.BannerPictureSizeHeight", Value = "1159" });

            list.Add(new SystemSetting() { Name = "GeneralSettings.FacebookPublicToken", Value = "" });
            list.Add(new SystemSetting() { Name = "GeneralSettings.FacebookSecretToken", Value = "" });

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