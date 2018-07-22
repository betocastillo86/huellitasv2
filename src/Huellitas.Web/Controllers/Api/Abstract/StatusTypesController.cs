//-----------------------------------------------------------------------
// <copyright file="StatusTypesController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Data.Entities;
    using Huellitas.Web.Infraestructure.WebApi;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Status Type controller
    /// </summary>
    [Route("api/statustypes")]
    public class StatusTypesController : BaseApiController
    {
        public StatusTypesController(IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>the list</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var list = new List<object>();
            list.Add(new { Id = Convert.ToInt32(StatusType.Created), Name = "Creado", Enum = StatusType.Created.ToString() });
            list.Add(new { Id = Convert.ToInt32(StatusType.Published), Name = "Publicado", Enum = StatusType.Published.ToString() });
            list.Add(new { Id = Convert.ToInt32(StatusType.Hidden), Name = "Oculto", Enum = StatusType.Hidden.ToString() });
            list.Add(new { Id = Convert.ToInt32(StatusType.Closed), Name = "Cerrado", Enum = StatusType.Closed.ToString() });
            list.Add(new { Id = Convert.ToInt32(StatusType.Rejected), Name = "Rechazado", Enum = StatusType.Rejected.ToString() });
            return this.Ok(list);
        }
    }
}