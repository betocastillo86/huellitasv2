//-----------------------------------------------------------------------
// <copyright file="AdminController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers
{
    using Huellitas.Business.Configuration;
    using Huellitas.Web.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Admin Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class AdminController : Controller
    {
        /// <summary>
        /// The general settings
        /// </summary>
        private IGeneralSettings generalSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="generalSettings">The general settings.</param>
        public AdminController(
            IGeneralSettings generalSettings)
        {
            this.generalSettings = generalSettings;
        }

        /// <summary>
        /// the index method
        /// </summary>
        /// <returns>the view</returns>
        public IActionResult Index()
        {
            var model = new AdminModel();
            model.CacheKey = this.generalSettings.ConfigJavascriptCacheKey;
            return this.View(model);
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        /// <returns>the view</returns>
        public IActionResult Login()
        {
            var model = new AdminModel();
            model.CacheKey = this.generalSettings.ConfigJavascriptCacheKey;
            return this.View(model);
        }
    }
}