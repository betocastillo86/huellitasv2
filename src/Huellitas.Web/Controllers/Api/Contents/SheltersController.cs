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
    using Business.Services.Contents;
    using Business.Services.Files;
    using Data.Entities;
    using Infraestructure.Security;
    using Infraestructure.WebApi;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Api.Common;
    using Models.Api.Contents;
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
        /// <param name="filesHelper">files helper</param>
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
        /// Gets this instance.
        /// </summary>
        /// <returns>the result</returns>
        [HttpGet]
        public IActionResult Get([FromQuery]ShelterFilterModel filter)
        {
            if (filter.IsValid())
            {
                var contents = this.contentService.Search(
                    contentType: ContentType.Shelter,
                    page: filter.Page,
                    pageSize: filter.PageSize,
                    keyword: filter.Keyword,
                    orderBy: filter.OrderByEnum,
                    locationId: filter.LocationId);

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
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}", Name = "Api_Shelters_GetById")]
        public IActionResult Get(int id)
        {
            var content = this.contentService.GetById(id, true);

            if (content != null)
            {
                if (content.Type == ContentType.Shelter)
                {
                    var model = content.ToShelterModel(
                    this.contentService,
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
                    this.ModelState.AddModelError("Id", "Este id no pertenece a un refugio");
                    return this.BadRequest(this.ModelState);
                }
            }
            else
            {
                return this.NotFound();
            }
        }


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

                var createdUri = this.Url.Link("Api_Shelters_GetById", new BaseModel { Id = content.Id });
                return this.Created(createdUri, new BaseModel { Id = content.Id });
            }
            else
            {
                return this.BadRequest(this.ModelState);
            }
        }

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