//-----------------------------------------------------------------------
// <copyright file="RolesController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Business.Extensions;
    using Business.Security;
    using Huellitas.Business.Services;
   
    using Infraestructure.Security;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Extensions;

    /// <summary>
    /// Roles Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/[controller]")]
    public class RolesController : BaseApiController
    {
        /// <summary>
        /// The role service
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesController"/> class.
        /// </summary>
        /// <param name="roleService">The role service.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        public RolesController(
            IRoleService roleService,
            IWorkContext workContext,
            IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
            this.roleService = roleService;
            this.workContext = workContext;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>the list</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            var entities = await this.roleService.GetAll();
            var models = entities.ToModels();

            return this.Ok(models);
        }
    }
}