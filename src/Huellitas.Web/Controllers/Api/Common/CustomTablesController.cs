//-----------------------------------------------------------------------
// <copyright file="CustomTablesController.cs" company="Huellitas sin hogar">
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
    using Models.Extensions;

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
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        public CustomTablesController(
            ICustomTableService customTableService,
            IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
            this.customTableService = customTableService;
        }

        /// <summary>
        /// Gets the by table.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>the return</returns>
        [HttpGet]
        [Route("{id}/rows")]
        public IActionResult GetByTable(int id, [FromQuery] CustomTableRowFilter filter)
        {
            filter = filter ?? new CustomTableRowFilter();

            if (filter.IsValid())
            {
                var rows = this.customTableService.GetRowsByTableId(id, filter.Keyword, page: filter.Page, pageSize: filter.PageSize);
                var model = rows.ToModels();
                return this.Ok(model, rows.HasNextPage, rows.TotalCount);
            }
            else
            {
                return this.BadRequest(filter.Errors);
            }
        }
    }
}