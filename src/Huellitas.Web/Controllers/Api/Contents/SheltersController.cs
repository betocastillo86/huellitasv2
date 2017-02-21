//-----------------------------------------------------------------------
// <copyright file="SheltersController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Contents
{
    using System.Linq;
    using System.Threading.Tasks;
    using Business.Configuration;
    using Business.Exceptions;
    using Business.Extensions.Entities;
    using Business.Security;
    using Business.Services.Contents;
    using Business.Services.Files;
    using Data.Entities;
    using Infraestructure.Security;
    using Infraestructure.WebApi;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Api.Common;
    using Models.Api.Contents;
    using Models.Extensions;
    using Models.Extensions.Contents;

    /// <summary>
    /// Shelters <c>Api</c> Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/[controller]")]
    public class SheltersController : BaseApiController
    {
        /// <summary>
        /// The content service
        /// </summary>
        private readonly IContentService contentService;

        /// <summary>
        /// The content settings
        /// </summary>
        private readonly IContentSettings contentSettings;

        /// <summary>
        /// The files helper
        /// </summary>
        private readonly IFilesHelper filesHelper;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// The file service
        /// </summary>
        private readonly IFileService fileService;

        /// <summary>
        /// The picture service
        /// </summary>
        private readonly IPictureService pictureService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SheltersController"/> class.
        /// </summary>
        /// <param name="contentService">The content service.</param>
        /// <param name="filesHelper">The files helper.</param>
        /// <param name="contentSettings">The content settings.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="fileService">The file service.</param>
        /// <param name="pictureService">The picture service.</param>
        public SheltersController(
            IContentService contentService,
            IFilesHelper filesHelper,
            IContentSettings contentSettings,
            IWorkContext workContext,
            IFileService fileService,
            IPictureService pictureService)
        {
            this.contentService = contentService;
            this.filesHelper = filesHelper;
            this.contentSettings = contentSettings;
            this.workContext = workContext;
            this.fileService = fileService;
            this.pictureService = pictureService;
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the list</returns>
        [HttpGet]
        public IActionResult Get([FromQuery]ShelterFilterModel filter)
        {
            var canGetUnplublished = this.CanGetUnpublished();
        
            if (filter.IsValid(canGetUnplublished))
            {
                var contents = this.contentService.Search(
                    contentType: ContentType.Shelter,
                    page: filter.Page,
                    pageSize: filter.PageSize,
                    keyword: filter.Keyword,
                    orderBy: filter.OrderByEnum,
                    locationId: filter.LocationId,
                    status: filter.Status);

                var models = contents.ToShelterModels(
                    this.contentService, 
                    this.filesHelper, 
                    Url.Content, 
                    withFiles: false,
                    width: this.contentSettings.PictureSizeWidthDetail,
                    height: this.contentSettings.PictureSizeHeightDetail,
                    thumbnailWidth: this.contentSettings.PictureSizeWidthList,
                    thumbnailHeight: this.contentSettings.PictureSizeHeightList);

                return this.Ok(models.ToList(), contents.HasNextPage, contents.TotalCount);
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
        /// <returns>the content</returns>
        [HttpGet]
        [Route("{id:int}", Name = "Api_Shelters_GetById")]
        public IActionResult Get(int id)
        {
            var content = this.contentService.GetById(id, true);

            if (content != null)
            {
                ////Only an admin user can see unpublished shelters
                if (content.StatusType != StatusType.Published && !this.workContext.CurrentUser.CanEditAnyContent())
                {
                    return this.NotFound();
                }

                if (content.Type == ContentType.Shelter)
                {
                    var model = content.ToShelterModel(
                    this.contentService,
                    this.filesHelper,
                    Url.Content,
                    true,
                    this.contentSettings.PictureSizeWidthDetail,
                    this.contentSettings.PictureSizeHeightDetail,
                    this.contentSettings.PictureSizeWidthList,
                    this.contentSettings.PictureSizeHeightList);

                    return this.Ok(model);
                }
                else
                {
                    this.ModelState.AddModelError("Id", "Este id no pertenece a un refugio");
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
        /// <returns>the action</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody]ShelterModel model)
        {
            if (this.IsValidModel(model, true))
            {
                Content content = null;

                try
                {
                    content = model.ToEntity(this.contentService, files: model.Files);

                    content.UserId = this.workContext.CurrentUserId;

                    ////Only if the user can aprove contents changes the status
                    if (this.workContext.CurrentUser.CanApproveContents())
                    {
                        content.StatusType = model.Status;
                        content.Featured = model.Featured;
                    }
                    else
                    {
                        content.StatusType = StatusType.Created;
                        content.Featured = false;
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

                var createdUri = this.Url.Link("Api_Shelters_GetById", new BaseModel { Id = content.Id });
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
        /// the action
        /// </returns>
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody]ShelterModel model)
        {
            if (this.IsValidModel(model, false))
            {
                var content = this.contentService.GetById(id);

                if (content != null)
                {
                    if (content.Type != ContentType.Shelter)
                    {
                        this.ModelState.AddModelError("Id", "Este id no pertenece a un refugio");
                        return this.BadRequest(this.ModelState);
                    }

                    if (!this.workContext.CurrentUser.CanUserEditShelter(content, this.contentService))
                    {
                        return this.Forbid();
                    }

                    ////Only if the user can aprove contents changes the status and featured
                    if (this.workContext.CurrentUser.CanApproveContents())
                    {
                        content.StatusType = model.Status;
                        content.Featured = model.Featured;
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
        /// Determines whether this instance [can get unpublished].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance [can get unpublished]; otherwise, <c>false</c>.
        /// </returns>
        public bool CanGetUnpublished()
        {
            return this.workContext.CurrentUser.IsSuperAdmin();
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
        public bool IsValidModel(ShelterModel model, bool isNew)
        {
            if (isNew && (model.Files == null || model.Files.Count == 0))
            {
                this.ModelState.AddModelError("Files", "Al menos se debe cargar una imagen");
            }

            if (model.Location == null)
            {
                this.ModelState.AddModelError("Location", "Si no ingresa la refugio debe ingresar ubicación");
            }

            return this.ModelState.IsValid;
        }
    }
}