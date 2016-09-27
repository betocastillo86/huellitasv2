//-----------------------------------------------------------------------
// <copyright file="PetsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Huellitas.Web.Controllers.Api.Contents
{
    using System.Collections.Generic;
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Services.Contents;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api.Contents;
    using Huellitas.Web.Models.Extensions;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Pets Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/[controller]")]
    public class PetsController : BaseApiController
    {
        #region props

        /// <summary>
        /// The content service
        /// </summary>
        private readonly IContentService contentService;

        #endregion props

        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="PetsController"/> class.
        /// </summary>
        /// <param name="contentService">The content service.</param>
        public PetsController(IContentService contentService)
        {
            this.contentService = contentService;
        }

        #endregion ctor

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the value</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ////System.Threading.Thread.Sleep(3000);
            return this.Ok(new { result = true });
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the value</returns>
        [HttpGet]
        public IActionResult Get(PetsFilterModel filter)
        {
            IList<FilterAttribute> filterData = null;

            if (filter.IsValid(out filterData))
            {
                var contentList = this.contentService.Search(
                    filter.Keyword,
                    Data.Entities.ContentType.Pet,
                    filterData,
                    filter.PageSize,
                    filter.Page,
                    filter.OrderByEnum);

                var models = contentList.ToModels(Url.Content);

                return this.Ok(models, contentList.HasNextPage, contentList.TotalCount);
            }
            else
            {
                return this.BadRequest(HuellitasExceptionCode.BadArgument, filter.Errors);
            }
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the value</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ////System.Threading.Thread.Sleep(3000);
            var model = this.contentService.GetById(id);
            return this.Ok(model);
        }

        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>the value</returns>
        [HttpPost]
        public IActionResult Post([FromBody]string value)
        {
            System.Threading.Thread.Sleep(3000);
            return this.Ok(new { Id = 3 });
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns>the value</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            return this.Ok(new { result = true });
        }
    }
}