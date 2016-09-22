using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Huellitas.Web.Models.Api.Contents;
using Huellitas.Business.Services.Contents;
using Huellitas.Data.Entities;
using Huellitas.Business.Extensions.Services;
using Huellitas.Web.Extensions;
using Huellitas.Web.Models.Extensions;
using Huellitas.Web.Infraestructure.WebApi;
using Huellitas.Business.Exceptions;
using Huellitas.Business.Services.Common;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Huellitas.Web.Controllers.Api.Contents
{
    [Route("api/[controller]")]
    public class PetsController : BaseApiController
    {
        #region props
        private readonly IContentService _contentService;
        #endregion
        #region ctor
        public PetsController(IContentService contentService)
        {
            _contentService = contentService;
        }
        #endregion


        // GET: api/values
        [HttpGet]
        public IActionResult Get(PetsFilterModel filter)
        {
            IList<FilterAttribute> filterData = null;

            if (filter.IsValid(out filterData))
            {
                var contentList = _contentService.Search(filter.Keyword,
                    Data.Entities.ContentType.Pet,
                    filterData,
                    filter.PageSize,
                    filter.Page,
                    filter.OrderByEnum);

                var models = contentList.ToModels(Url.Content);

                return Ok(models, contentList.HasNextPage, contentList.TotalCount);
            }
            else
            {
                return BadRequest(HuellitasExceptionCode.BadArgument, filter.Errors);
            }



        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //System.Threading.Thread.Sleep(3000);
            var model = _contentService.GetById(id);
            return Ok(model);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]string value)
        {
            System.Threading.Thread.Sleep(3000);
            return Ok(new { Id = 3 });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            return Ok(new { result = true });
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //System.Threading.Thread.Sleep(3000);
            return Ok(new { result = true });
        }
    }
}
