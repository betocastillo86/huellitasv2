//-----------------------------------------------------------------------
// <copyright file="ModulesController.cs" company="Huellitas sin Hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Abstract
{
    using System.Collections.Generic;
    using Huellitas.Web.Infraestructure.WebApi;
    using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public IActionResult Get()
        {
            var modules = new List<ModuleModel>();
            modules.Add(new ModuleModel() { Id = 1, Name = "Animales", Key = "Animals", Url = "/pets" });
            modules.Add(new ModuleModel() { Id = 2, Name = "Usuarios", Key = "Users", Url = "/users" });
            modules.Add(new ModuleModel() { Id = 3, Name = "Fundaciones", Key = "Shelters", Url = "/shelters" });
            modules.Add(new ModuleModel() { Id = 4, Name = "Formularios", Key = "Forms", Url = "/adoptionforms" });
            modules.Add(new ModuleModel() { Id = 5, Name = "Notificaciones", Key = "Notifications", Url = "/notifications" });
            modules.Add(new ModuleModel() { Id = 6, Name = "Ajustes", Key = "Settings", Url = "/systemsettings" });
            modules.Add(new ModuleModel() { Id = 7, Name = "Recursos", Key = "TextResources", Url = "/textresources" });
            modules.Add(new ModuleModel() { Id = 8, Name = "Notificaciones Correo", Key = "EmailNotifications", Url = "/emailnotifications" });
            return this.Ok(modules);
        }
    }
}