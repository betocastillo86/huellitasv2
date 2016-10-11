//-----------------------------------------------------------------------
// <copyright file="ModulesController.cs" company="Huellitas sin Hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Abstract
{
    using System.Collections.Generic;
    using Huellitas.Web.Infraestructure.WebApi;
    using Microsoft.AspNetCore.Mvc;
    using Models.Api.Abstract;

    /// <summary>
    /// Modules Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/[controller]")]
    public class ModulesController : BaseApiController
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>the value</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var modules = new List<ModuleModel>();
            modules.Add(new ModuleModel() { Id = 1, Name = "Animales", Key = "Animals", Url = "/pets" });
            modules.Add(new ModuleModel() { Id = 2, Name = "Usuarios", Key = "Users", Url = "/users" });
            modules.Add(new ModuleModel() { Id = 3, Name = "Fundaciones", Key = "Shelters", Url = "/shelters" });
            modules.Add(new ModuleModel() { Id = 4, Name = "Formularios", Key = "Forms", Url = "/adoptionforms" });
            return this.Ok(modules);
        }
    }
}