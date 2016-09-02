using Huellitas.Business.Services.Contents;
using Huellitas.Data.Core;
using Huellitas.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Huellitas.Web.Controllers
{
    public class HomeController : Controller
    {

        #region ctor
        public HomeController()
        {
            
        }
        #endregion

        public ActionResult Index()
        {
            return View();
        }
    }
}
