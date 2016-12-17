//-----------------------------------------------------------------------
// <copyright file="LocationsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Common
{
    using System.Collections.Generic;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Infraestructure.WebApi;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Locations Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/locations")]
    public class LocationsController : BaseApiController
    {
        /// <summary>
        /// Gets the specified parent identifier.
        /// </summary>
        /// <param name="parentId">if set to <c>true</c> [parent identifier].</param>
        /// <returns>the locations</returns>
        [HttpGet]
        public IActionResult Get(bool parentId)
        {
            ////TODO:Implementar
            var list = new List<Location>();
            var bog = new Location() { Id = 2, Name = "Bogota", ParentLocationId = 1 };
            list.Add(bog);
            list.Add(new Location() { Id = 3, Name = "Suba", ParentLocationId = 2 });
            list.Add(new Location() { Id = 4, Name = "Bosa", ParentLocationId = 2 });
            return this.Ok(list);
        }
    }
}