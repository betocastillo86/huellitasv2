//-----------------------------------------------------------------------
// <copyright file="LocationsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Common
{
    using System.Collections.Generic;
    using Business.Services.Common;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Infraestructure.WebApi;
    using Microsoft.AspNetCore.Mvc;
    using Models.Api.Common;
    using Models.Extensions.Common;

    /// <summary>
    /// Locations Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/locations")]
    public class LocationsController : BaseApiController
    {
        /// <summary>
        /// The location service
        /// </summary>
        private readonly ILocationService locationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationsController"/> class.
        /// </summary>
        /// <param name="locationService">The location service.</param>
        public LocationsController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        /// <summary>
        /// Gets the specified parent identifier.
        /// </summary>
        /// <param name="parentId">if set to <c>true</c> [parent identifier].</param>
        /// <returns>the locations</returns>
        [HttpGet]
        public IActionResult Get([FromQuery]LocationFilterModel filter)
        {
            ////TODO:test
            if (filter.IsValid())
            {
                var locations = this.locationService.GetAll(
                    filter.Name, 
                    filter.ParentId, 
                    filter.Page, 
                    filter.PageSize);

                var models = locations.ToModels();

                return this.Ok(models, locations.HasNextPage, locations.TotalCount);
            }
            else
            {
                return this.BadRequest(this.ModelState);
            }
        }
    }
}