//-----------------------------------------------------------------------
// <copyright file="CustomTablesController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Common
{
    using Huellitas.Business.Services.Common;
    using Huellitas.Web.Infraestructure.WebApi;
    using Microsoft.AspNetCore.Mvc;
    using Models.Extensions.Common;

    /// <summary>
    /// Custom Table Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("/api/customtables")]
    public class CustomTablesController : BaseApiController
    {
        /// <summary>
        /// The custom table service
        /// </summary>
        private readonly ICustomTableService customTableService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTablesController"/> class.
        /// </summary>
        /// <param name="customTableService">The custom table service.</param>
        public CustomTablesController(ICustomTableService customTableService)
        {
            this.customTableService = customTableService;
        }

        /// <summary>
        /// Gets the by table.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the action</returns>
        [HttpGet]
        [Route("{id}/rows")]
        public IActionResult GetByTable(int id)
        {
            var rows = this.customTableService.GetRowsByTableId(id);
            var model = rows.ToModels();
            return this.Ok(model);
        }
    }
}