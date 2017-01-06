﻿//-----------------------------------------------------------------------
// <copyright file="PetsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Contents
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Business.Caching;
    using Business.Configuration;
    using Business.Extensions.Entities;
    using Business.Services.Common;
    using Business.Services.Files;
    using Business.Utilities.Extensions;
    using Data.Entities;
    using Data.Entities.Enums;
    using Data.Extensions;
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Services.Contents;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api.Contents;
    using Huellitas.Web.Models.Extensions;
    using Infraestructure.Security;
    using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// The picture service
        /// </summary>
        private readonly IPictureService pictureService;

        /// <summary>
        /// The content settings
        /// </summary>
        private readonly IContentSettings contentSettings;

        /// <summary>
        /// The file service
        /// </summary>
        private readonly IFileService fileService;

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
        /// <param name="pictureService">the picture service</param>
        /// <param name="contentSettings">content settings</param>
        /// <param name="fileService">the file service</param>
        public PetsController(
            IContentService contentService,
            IFilesHelper filesHelper,
            ICacheManager cacheManager,
            ICustomTableService customTableService,
            IWorkContext workContext,
            IPictureService pictureService,
            IContentSettings contentSettings,
            IFileService fileService)
        {
            this.contentService = contentService;
            this.filesHelper = filesHelper;
            this.cacheManager = cacheManager;
            this.customTableService = customTableService;
            this.workContext = workContext;
            this.pictureService = pictureService;
            this.contentSettings = contentSettings;
            this.fileService = fileService;
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

            var canGetUnplublished = this.CanGetUnpublished(filter);

            if (filter.IsValid(canGetUnplublished, out filterData))
            {
                var contentList = this.contentService.Search(
                    filter.Keyword,
                    Data.Entities.ContentType.Pet,
                    filterData,
                    filter.PageSize,
                    filter.Page,
                    filter.OrderByEnum,
                    filter.LocationId,
                    filter.Status);

                var models = contentList.ToPetModels(
                    this.contentService, 
                    this.customTableService, 
                    this.cacheManager, 
                    contentUrlFunction: Url.Content,
                    filesHelper: this.filesHelper,
                    width: this.contentSettings.PictureSizeWidthDetail,
                    height: this.contentSettings.PictureSizeHeightDetail,
                    thumbnailWidth: this.contentSettings.PictureSizeWidthList,
                    thumbnailHeight: this.contentSettings.PictureSizeHeightList);

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
        [Route("{id:int}", Name = "Api_Pets_GetById")]
        public IActionResult Get(int id)
        {
            var content = this.contentService.GetById(id, true);

            if (content != null)
            {
                ////Only the user can see an unpublished pet if can edit it
                if (content.StatusType != StatusType.Published && !this.CanUserEditPet(content))
                {
                    return this.NotFound();
                }

                if (content.Type == ContentType.Pet)
                {
                    var model = content.ToPetModel(
                    this.contentService,
                    this.customTableService,
                    this.cacheManager,
                    this.filesHelper,
                    Url.Content,
                    true,
                    true,
                    this.contentSettings.PictureSizeWidthDetail,
                    this.contentSettings.PictureSizeHeightDetail,
                    this.contentSettings.PictureSizeWidthList,
                    this.contentSettings.PictureSizeHeightList);

                    return this.Ok(model);
                }
                else
                {
                    this.ModelState.AddModelError("Id", "Este id no pertenece a un animal");
                    return this.BadRequest(this.ModelState);
                }
            }
            else
            {
                return this.NotFound();
            }
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the pet id</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody]PetModel model)
        {
            if (this.IsValidModel(model, true))
            {
                Content content = null;

                try
                {
                    content = model.ToEntity(this.contentService, files: model.Files);

                    content.UserId = this.workContext.CurrentUserId;

                    ////TODO:Crear servicio para contenidos relacionados
                    ////Only if the user can aprove contents changes the status
                    if (this.workContext.CurrentUser.CanApproveContents())
                    {
                        content.StatusType = model.Status;
                    }
                    else
                    {
                        content.StatusType = StatusType.Created;
                    }

                    await this.contentService.InsertAsync(content);

                    if (content.ContentFiles.Count > 0)
                    {
                        var files = this.fileService.GetByIds(content.ContentFiles.Select(c => c.FileId).ToArray());

                        foreach (var file in files)
                        {
                            this.pictureService.GetPicturePath(file, this.contentSettings.PictureSizeWidthDetail, this.contentSettings.PictureSizeHeightDetail, true);
                            this.pictureService.GetPicturePath(file, this.contentSettings.PictureSizeWidthList, this.contentSettings.PictureSizeHeightList, true);
                        }
                    }
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
        /// <param name="model">The model.</param>
        /// <returns>
        /// the response
        /// </returns>
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody]PetModel model)
        {
            if (this.IsValidModel(model, false))
            {
                var content = this.contentService.GetById(id);

                if (content != null)
                {
                    if (content.Type != ContentType.Pet)
                    {
                        this.ModelState.AddModelError("Id", "Este id no pertenece a un animal");
                        return this.BadRequest(this.ModelState);
                    }

                    if (!this.CanUserEditPet(content))
                    {
                        return this.Forbid();
                    }

                    ////Only if the user can aprove contents changes the status
                    if (this.workContext.CurrentUser.CanApproveContents())
                    {
                        content.StatusType = model.Status;
                    }

                    content = model.ToEntity(this.contentService, content);

                    try
                    {
                        await this.contentService.UpdateAsync(content);
                        return this.Ok(new { result = true });
                    }
                    catch (HuellitasException e)
                    {
                        return this.BadRequest(e);
                    }
                }
                else
                {
                    return this.NotFound();
                }
            }
            else
            {
                return this.BadRequest(this.ModelState);
            }
        }

        /// <summary>
        /// Determines whether this instance [can user edit pet] the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can user edit pet] the specified content; otherwise, <c>false</c>.
        /// </returns>
        [NonAction]
        public bool CanUserEditPet(Content content)
        {
            return this.workContext.CurrentUser.CanUserEditPet(content, this.contentService);
        }

        /// <summary>
        /// Determines whether this instance [can get unpublished] the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can get unpublished] the specified filter; otherwise, <c>false</c>.
        /// </returns>
        [NonAction]
        public bool CanGetUnpublished(PetsFilterModel filter)
        {
            var user = this.workContext.CurrentUser;
            ////Si no tiene sesión no puede traerlos todos
            if (user != null)
            {
                ////Si es superadmin puede traerlos todos
                if (user.IsSuperAdmin())
                {
                    return true;
                }
                else
                {
                    ////Si no es super admin y pertenece a alguna de los refugios los puede traer todos                    
                    ////Si el filtro tiene más de un refugio no permite ver inactivos
                    if (filter.Shelter.ToIntList(false).Length == 1)
                    {
                        ////Puede traer inactivos si pertenece al refugio
                        var shelterId = filter.Shelter.ToIntList().FirstOrDefault();
                        return this.contentService.GetUsersByContentId(shelterId, ContentUserRelationType.Shelter).Any(c => c.UserId == user.Id);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether [is valid model] [the specified model].
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="isNew">if set to <c>true</c> [is new].</param>
        /// <returns>
        ///   <c>true</c> if [is valid model] [the specified model]; otherwise, <c>false</c>.
        /// </returns>
        [NonAction]
        public bool IsValidModel(PetModel model, bool isNew)
        {
            ////Removes shelter validation to avoid validate body and name properties
            this.ModelState.Remove("Shelter.Body");
            this.ModelState.Remove("Shelter.Name");

            if (isNew && (model.Files == null || model.Files.Count == 0))
            {
                this.ModelState.AddModelError("Files", "Al menos se debe cargar una imagen");
            }

            if (model.Shelter == null && model.Location == null)
            {
                this.ModelState.AddModelError("Location", "Si no ingresa la refugio debe ingresar ubicación");
                this.ModelState.AddModelError("Shelter", "Si no ingresa la ubicación debe ingresar refugio");
            }

            return this.ModelState.IsValid;
        }
    }
}