using Huellitas.Data.Entities;
using Huellitas.Web.Infraestructure.WebApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Web.Controllers.Api.Common
{
    [Route("api/locations")]
    public class LocationsController : BaseApiController
    {
        [HttpGet]
        public IActionResult Get(bool parentId)
        {
            var list = new List<Location>();
            var bog = new Location() { Id = 2, Name = "Bogota", ParentLocationId = 1 };
            list.Add(bog);
            list.Add(new Location() { Id = 3, Name = "Suba", ParentLocationId = 2 });
            list.Add(new Location() { Id = 4, Name = "Bosa", ParentLocationId = 2 });
            return this.Ok(list);
        }
    }
}
