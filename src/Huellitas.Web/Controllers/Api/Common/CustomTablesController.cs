using Huellitas.Business.Services.Common;
using Huellitas.Web.Infraestructure.WebApi;
using Huellitas.Web.Models.Api.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Web.Controllers.Api.Common
{
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
            return this.Ok(this.customTableService.GetRowsByTableId(id));
        }
    }
}
