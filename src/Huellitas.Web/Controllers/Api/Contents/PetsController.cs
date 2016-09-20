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

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Huellitas.Web.Controllers.Api.Contents
{
    [Route("api/[controller]")]
    public class PetsController : Controller
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
        public IActionResult Get(PetsFilterModel model)
        {
            var filter = new List<FilterAttribute>();
            filter.AddRangeAttribute(ContentAttributeType.Age, model.age);
            filter.Add(ContentAttributeType.Genre, model.genre);
            filter.Add(ContentAttributeType.Size, model.size.ToStringList(), FilterAttributeType.Multiple);
            filter.Add(ContentAttributeType.Shelter, model.shelter.ToStringList(), FilterAttributeType.Multiple);
            filter.Add(ContentAttributeType.Subtype, model.type.ToStringList(), FilterAttributeType.Multiple);



            var contentList = _contentService.Search(model.keyword, Data.Entities.ContentType.Pet, filter, model.pageSize, model.page).ToModels();

            //System.Threading.Thread.Sleep(3000);
            return Ok(contentList);
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
        public void Delete(int id)
        {
        }
    }
}
