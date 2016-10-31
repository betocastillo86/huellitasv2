//-----------------------------------------------------------------------
// <copyright file="PetsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Huellitas.Web.Controllers.Api.Contents
{
    using System.Collections.Generic;
    using Data.Entities;
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Services.Contents;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api.Contents;
    using Huellitas.Web.Models.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Models.Api.Common;

    /// <summary>
    /// Pets Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/pets")]
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

                var models = contentList.ToPetModels(Url.Content);

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
        [HttpGet]
        [Route("{id}", Name = "Api_Pets_GetById")]
        public IActionResult Get(int id)
        {
            var model = this.contentService.GetById(id, true).ToModel(contentUrlFunction:Url.Content);
            return this.Ok(model);
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the pet id</returns>
        [HttpPost]
        public IActionResult Post([FromBody]PetModel model)
        {
            if (this.ModelState.IsValid & model.IsValid(this.ModelState))
            {
                var content = model.ToEntity(this.contentService);
                content.UserId = 1;

                for (int i = 0; i < model.Files.Count; i++)
                {
                    if (i == 0)
                    {
                        content.FileId = model.Files[i].Id;
                    }
                    else
                    {
                        content.ContentFiles.Add(new ContentFile()
                        {
                            FileId = model.Files[i].Id
                        });
                    }
                }

                try
                {
                    this.contentService.Insert(content);
                }
                catch (HuellitasException e)
                {
                    return this.BadRequest(e);
                }

                var createdUri = this.Url.Link("Api_Pets_GetById", new BaseModel { Id = content.Id });
                return this.Created(createdUri, new BaseModel { Id = content.Id });
            }
            else
            {
                return this.BadRequest(this.ModelState);
            }
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