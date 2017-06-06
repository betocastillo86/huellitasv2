//-----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers
{
    using Huellitas.Business.Configuration;
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

        private readonly SendMailTask mail;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        public HomeController(
            IGeneralSettings generalSettings,
            SendMailTask mail)
        {
            this.generalSettings = generalSettings;
            this.mail = mail; ////TODO: quitar esta inyeccion
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