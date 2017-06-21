//-----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers
{
    using Huellitas.Business.Configuration;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Services;
    using Huellitas.Business.Tasks;
    using Huellitas.Web.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Home Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class HomeController : Controller
    {
        #region ctor

        /// <summary>
        /// The general settings
        /// </summary>
        private readonly IGeneralSettings generalSettings;

        /// <summary>
        /// The security settings
        /// </summary>
        private readonly ISecuritySettings securitySettings;

        /// <summary>
        /// The log service
        /// </summary>
        private readonly ILogService logService;

        /// <summary>
        /// system setting service
        /// </summary>
        private readonly ISystemSettingService systemSettingService;

        /// <summary>
        /// the SEO service
        /// </summary>
        private readonly ISeoService seoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        public HomeController(
            IGeneralSettings generalSettings,
            ISecuritySettings securitySettings,
            ILogService logService,
            ISystemSettingService systemSettingService,
            ISeoService seoService)
        {
            this.generalSettings = generalSettings;
            this.securitySettings = securitySettings;
            this.logService = logService;
            this.systemSettingService = systemSettingService;
            this.seoService = seoService;
        }

        #endregion ctor

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>the value</returns>
        public IActionResult Index()
        {
            var model = new HomeModel();
            model.CacheKey = this.generalSettings.ConfigJavascriptCacheKey;
            model.GoogleAnalyticsCode = this.generalSettings.GoogleAnalyticsCode;

            return this.View(model);
        }

        public IActionResult RedirectPrevious(int id, string name)
        {
            var friendlyName = this.systemSettingService.GetCachedSetting<string>($"RedirectionSettings.{name}");

            if (!string.IsNullOrEmpty(friendlyName))
            {
                return this.RedirectPermanent(this.seoService.GetFullRoute("shelter", friendlyName));
            }
            else
            {
                return this.Redirect(this.seoService.GetFullRoute("notfound"));
            }
        }
    }
}