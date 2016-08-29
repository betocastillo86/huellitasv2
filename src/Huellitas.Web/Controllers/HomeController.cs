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

        #region props
        private readonly IContentService _contentService;
        private readonly IRepository<Content> _contentRepository;
        #endregion
        #region ctor
        public HomeController(IContentService contentService,
            IRepository<Content> contentRepository)
        {
            _contentService = contentService;
            _contentRepository = contentRepository;
        }
        #endregion

        public ActionResult Index()
        {
            return View();
        }
    }
}
