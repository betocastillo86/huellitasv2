//-----------------------------------------------------------------------
// <copyright file="PetsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Contents
{
    using System.Collections.Generic;
    using Business.Caching;
    using Business.Services.Common;
    using Business.Services.Files;
    using Data.Entities;
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Services.Contents;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api.Contents;
    using Huellitas.Web.Models.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Api.Common;
    using Infraestructure.Security;

    /// <summary>
    /// Pets Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/pets")]
    public class PetsController : BaseApiController
    {
        #region props

        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager cacheManager;

        /// <summary>
        /// The content service
        /// </summary>
        private readonly IContentService contentService;

        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICustomTableService customTableService;

        /// <summary>
        /// The files helper
        /// </summary>
        private readonly IFilesHelper filesHelper;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;
        #endregion props

        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="PetsController"/> class.
        /// </summary>
        /// <param name="contentService">The content service.</param>
        /// <param name="filesHelper">the file helper</param>
        /// <param name="cacheManager">the cache manager</param>
        /// <param name="customTableService">the custom table service</param>
        /// <param name="workContext">the work context</param>
        public PetsController(
            IContentService contentService,
            IFilesHelper filesHelper,
            ICacheManager cacheManager,
            ICustomTableService customTableService,
            IWorkContext workContext)
        {
            this.contentService = contentService;
            this.filesHelper = filesHelper;
            this.cacheManager = cacheManager;
            this.customTableService = customTableService;
            this.workContext = workContext;
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
            var claimAdmin = this.User.FindFirst("IsAdmin");
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

                var models = contentList.ToPetModels(this.contentService, this.customTableService, this.cacheManager, contentUrlFunction: Url.Content);

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
            var content = this.contentService.GetById(id, true);

            var model = content.ToPetModel(this.contentService, this.customTableService, this.cacheManager, this.filesHelper, Url.Content, true, true);

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
                content.UserId = this.workContext.CurrentUserId;

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