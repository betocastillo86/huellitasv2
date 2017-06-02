//-----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers
{
    using Huellitas.Business.Configuration;
    using Huellitas.Web.Models;
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
        private IGeneralSettings generalSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        public HomeController(
            IGeneralSettings generalSettings)
        {
            this.generalSettings = generalSettings;
        }

        #endregion ctor

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>the value</returns>
        public ActionResult Index()
        {
            var model = new HomeModel();
            model.CacheKey = this.generalSettings.ConfigJavascriptCacheKey;

            return this.View(model);
        }
    }
}