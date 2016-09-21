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
            if (filter.IsValid())
            {
                var filterData = new List<FilterAttribute>();
                filterData.AddRangeAttribute(ContentAttributeType.Age, filter.age);
                filterData.Add(ContentAttributeType.Genre, filter.genre);
                filterData.Add(ContentAttributeType.Size, filter.size.ToStringList(), FilterAttributeType.Multiple);
                filterData.Add(ContentAttributeType.Shelter, filter.shelter.ToStringList(), FilterAttributeType.Multiple);
                filterData.Add(ContentAttributeType.Subtype, filter.type.ToStringList(), FilterAttributeType.Multiple);

                var contentList = _contentService.Search(filter.keyword,
                    Data.Entities.ContentType.Pet,
                    filterData,
                    filter.pageSize,
                    filter.page);

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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post(PetsFilterModel model)
        {
            if (model.IsValid())
            {

            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
