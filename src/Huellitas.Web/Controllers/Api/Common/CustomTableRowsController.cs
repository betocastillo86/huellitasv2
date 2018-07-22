//-----------------------------------------------------------------------
// <copyright file="CustomTableRowsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Huellitas.Business.Services;
    using Huellitas.Web.Models.Api;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Custom table rows controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("/api/customtablerows")]
    public class CustomTableRowsController : BaseApiController
    {
        /// <summary>
        /// The custom table service
        /// </summary>
        private readonly ICustomTableService customTableService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTableRowsController"/> class.
        /// </summary>
        /// <param name="customTableService">The custom table service.</param>
        public CustomTableRowsController(ICustomTableService customTableService,
            IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
            this.customTableService = customTableService;
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the result</returns>
        [HttpGet]
        public IActionResult Get(CustomTableRowFilter filter)
        {
            return null;
        }
    }
}