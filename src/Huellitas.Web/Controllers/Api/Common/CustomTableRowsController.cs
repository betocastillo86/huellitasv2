//-----------------------------------------------------------------------
// <copyright file="CustomTableRowsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Common
{
    using Huellitas.Business.Services.Common;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api.Common;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Customtable rows controller
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
        public CustomTableRowsController(ICustomTableService customTableService)
        {
            this.customTableService = customTableService;
        }

        [HttpGet]
        public IActionResult Get(CustomTableRowFilter filter)
        {
            //this.customTableService.GetRowsByTableId(filter)
            return null;
        }
    }
}