//-----------------------------------------------------------------------
// <copyright file="ModulesController.cs" company="Huellitas sin Hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using System.Collections.Generic;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Security;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Api;

    /// <summary>
    /// Modules Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/[controller]")]
    public class ModulesController : BaseApiController
    {
        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModulesController"/> class.
        /// </summary>
        /// <param name="workContext">The work context.</param>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        public ModulesController(IWorkContext workContext, IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
            this.workContext = workContext;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>
        /// the value
        /// </returns>
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var modules = new List<ModuleModel>();

            if (this.workContext.CurrentUser.IsSuperAdmin())
            {
                modules.Add(new ModuleModel() { Id = 1, Name = "Animales", Key = "Animals", Url = "/pets", Icon = "fa-paw" });
                modules.Add(new ModuleModel() { Id = 2, Name = "Fundaciones", Key = "Shelters", Url = "/shelters", Icon = "fa-home" });
                modules.Add(new ModuleModel() { Id = 3, Name = "Formularios", Key = "Forms", Url = "/adoptionforms", Icon = "fa-newspaper-o" });
                modules.Add(new ModuleModel() { Id = 4, Name = "Usuarios", Key = "Users", Url = "/users", Icon = "fa-users" });
                modules.Add(new ModuleModel() { Id = 1, Name = "Perdidos", Key = "Perdidos", Url = "/lostpets", Icon = "fa-search" });

                var settings = new ModuleModel() { Id = 5, Name = "Configuracion", Key = "SettingsParent", Icon = "fa-cogs", Children = new List<ModuleModel>(), Url = "#" };
                modules.Add(settings);
                settings.Children.Add(new ModuleModel() { Id = 6, Name = "Notificaciones", Key = "Notifications", Url = "/notifications", Icon = "fa-tasks" });
                settings.Children.Add(new ModuleModel() { Id = 7, Name = "Ajustes", Key = "Settings", Url = "/systemsettings", Icon = "fa-cogs" });
                settings.Children.Add(new ModuleModel() { Id = 8, Name = "Recursos", Key = "TextResources", Url = "/textresources", Icon = "fa-font" });
                settings.Children.Add(new ModuleModel() { Id = 9, Name = "Notificaciones Correo", Key = "EmailNotifications", Url = "/emailnotifications", Icon = "fa-send" });
                settings.Children.Add(new ModuleModel() { Id = 10, Name = "Log de errores", Key = "Logs", Url = "/logs", Icon = "fa-list" });

                modules.Add(new ModuleModel() { Id = 10, Name = "Banners", Key = "Banners", Url = "/banners", Icon = "fa-image" });
            }

            return this.Ok(modules);
        }
    }
}