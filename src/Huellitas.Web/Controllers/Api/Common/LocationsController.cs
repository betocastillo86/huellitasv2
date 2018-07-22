//-----------------------------------------------------------------------
// <copyright file="LocationsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Business.Services;
    using Microsoft.AspNetCore.Mvc;
    using Models.Api;
    using Models.Extensions;

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
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        public LocationsController(
            ILocationService locationService,
            IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
            this.locationService = locationService;
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the contents</returns>
        [HttpGet]
        public IActionResult Get([FromQuery]LocationFilterModel filter)
        {
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