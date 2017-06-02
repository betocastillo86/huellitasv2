//-----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Home Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class HomeController : Controller
    {
        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        public HomeController()
        {
        }

        #endregion ctor

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>the value</returns>
        public ActionResult Index()
        {
            return this.View();
        }
    }
}