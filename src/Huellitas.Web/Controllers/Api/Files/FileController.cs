using Huellitas.Web.Infraestructure.WebApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Web.Controllers.Api.Files
{
    [Route("api/files")]
    public class FileController : BaseApiController
    {
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            return this.Ok(new { deleted = true});
        }
    }
}
