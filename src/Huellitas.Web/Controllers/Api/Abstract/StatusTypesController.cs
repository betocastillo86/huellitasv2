//-----------------------------------------------------------------------
// <copyright file="StatusTypesController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Abstract
{
    using System;
    using System.Collections.Generic;
    using Data.Entities;
    using Huellitas.Web.Infraestructure.WebApi;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Status Type controller
    /// </summary>
    [Route("api/statustypes")]
    public class StatusTypesController : BaseApiController
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>the list</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var list = new List<object>();
            list.Add(new { Id = Convert.ToInt32(StatusType.Created), Name = "Creado" });
            list.Add(new { Id = Convert.ToInt32(StatusType.Published), Name = "Publicado" });
            list.Add(new { Id = Convert.ToInt32(StatusType.Hidden), Name = "Oculto" });
            list.Add(new { Id = Convert.ToInt32(StatusType.Closed), Name = "Cerrado" });
            return this.Ok(list);
        }
    }
}