//-----------------------------------------------------------------------
// <copyright file="AdminController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Admin Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class AdminController : Controller
    {
        /// <summary>
        /// the index method
        /// </summary>
        /// <returns>the view</returns>
        public ActionResult Index()
        {
            //if (this.User.Identity.IsAuthenticated)
            //{
            //    return this.View();
            //}
            //else
            //{
            //    return this.RedirectToAction("Login");
            //}          
            return this.View();
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        /// <returns>the view</returns>
        public ActionResult Login()
        {
            //if (this.User.Identity.IsAuthenticated)
            //{
            //    return this.RedirectToAction("Index");
            //}
            //else
            //{
            //    return this.View();
            //}
            return this.View();
        }
    }
}