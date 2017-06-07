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
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        public HomeController(
            IGeneralSettings generalSettings,
            ISecuritySettings securitySettings,
            ILogService logService)
        {
            this.generalSettings = generalSettings;
            this.securitySettings = securitySettings;
            this.logService = logService;
        }

        #endregion ctor

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>the value</returns>
        public ActionResult Index()
        {
            if (this.securitySettings.TrackHomeRequests)
            {
                this.logService.Information("Visita registrada en el home");
            }

            var model = new HomeModel();
            model.CacheKey = this.generalSettings.ConfigJavascriptCacheKey;

            return this.View(model);
        }
    }
}