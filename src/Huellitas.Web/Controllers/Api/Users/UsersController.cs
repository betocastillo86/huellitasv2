//-----------------------------------------------------------------------
// <copyright file="UsersController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Users
{
    using System.Collections.Generic;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api.Users;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Users Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/users")]
    public class UsersController : BaseApiController
    {
        /// <summary>
        /// Gets the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the filtered users</returns>
        [HttpGet]
        public IActionResult Get([FromQuery]UsersFilterModel model)
        {
            ////TODO:Implementar
            var users = new List<BaseUserModel>();
            users.Add(new BaseUserModel { Id = 1, Name = "Pepito" });
            users.Add(new BaseUserModel { Id = 2, Name = "Erica" });
            users.Add(new BaseUserModel { Id = 3, Name = "Gabriel" });
            return this.Ok(users, false, 3);
        }
    }
}