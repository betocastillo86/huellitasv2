//-----------------------------------------------------------------------
// <copyright file="PetsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using Business.Caching;
    using Business.Configuration;
    using Business.Extensions;
    using Business.Security;
    using Business.Services;
    using Business.Utilities.Extensions;
    using Data.Entities;
    using Huellitas.Business.EventPublisher;
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Subscribers;
    using Huellitas.Data.Core;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api;
    using Huellitas.Web.Models.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.JsonPatch.Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        /// <summary>
        /// The seo service
        /// </summary>
        private readonly ISeoService seoService;

        /// <summary>
        /// The location service
        /// </summary>
        private readonly ILocationService locationService;

        /// <summary>
        /// The log service
        /// </summary>
        private readonly ILogService logService;

        /// <summary>
        /// The adoption form service
        /// </summary>
        private readonly IAdoptionFormService adoptionFormService;

        /// <summary>
        /// The content repository
        /// </summary>
        private readonly IRepository<Content> contentRepository;

        /// <summary>
        /// The publisher
        /// </summary>
        private readonly IPublisher publisher;

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
            IFileService fileService,
            ISeoService seoService,
            ILocationService locationService,
            IRepository<Content> contentRepository,
            ILogService logService,
            IAdoptionFormService adoptionFormService,
            IPublisher publisher)
        {
            this.contentService = contentService;
            this.filesHelper = filesHelper;
            this.cacheManager = cacheManager;
            this.customTableService = customTableService;
            this.workContext = workContext;
            this.pictureService = pictureService;
            this.contentSettings = contentSettings;
            this.fileService = fileService;
            this.seoService = seoService;
            this.locationService = locationService;
            this.contentRepository = contentRepository;
            this.logService = logService;
            this.adoptionFormService = adoptionFormService;
            this.publisher = publisher;
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

            if (filter.IsValid(canGetUnplublished, this.workContext, out filterData))
            {
                DateTime? closingDateFilter = filter.WithinClosingDate.HasValue && filter.WithinClosingDate.Value ? DateTime.Now : (DateTime?)null;

                var contentList = this.contentService.Search(
                    filter.Keyword,
                    filter.ContentType,
                    filterData,
                    filter.PageSize,
                    filter.Page,
                    filter.OrderByEnum,
                    filter.LocationId,
                    filter.Status,
                    closingDateFrom: closingDateFilter,
                    startingDateFrom: filter.FromStartingDate,
                    belongsToUserId: filter.Mine ? this.workContext.CurrentUserId : (int?)null);

                IDictionary<int, int> formsByContent = null;
                if (filter.CountForms)
                {
                    formsByContent = this.adoptionFormService.CountAdoptionFormsByContents(contentList.Select(c => c.Id).ToArray(), AdoptionFormAnswerStatus.None);
                }

                var models = contentList.ToPetModels(
                    this.contentService,
                    this.customTableService,
                    this.cacheManager,
                    this.workContext,
                    contentUrlFunction: Url.Content,
                    filesHelper: this.filesHelper,
                    width: this.contentSettings.PictureSizeWidthDetail,
                    height: this.contentSettings.PictureSizeHeightDetail,
                    thumbnailWidth: this.contentSettings.PictureSizeWidthList,
                    thumbnailHeight: this.contentSettings.PictureSizeHeightList,
                    pendingForms: formsByContent);

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
        [Route("{friendlyName}", Name = "Api_Pets_GetById")]
        public IActionResult Get(string friendlyName)
        {
            int id = 0;
            Content content;
            if (int.TryParse(friendlyName, out id))
            {
                content = this.contentService.GetById(id, true);
            }
            else
            {
                content = this.contentService.GetByFriendlyName(friendlyName, true);
            }

            if (content != null)
            {
                ////Only the user can see an unpublished pet if can edit it
                if (content.StatusType != StatusType.Published && !this.CanUserEditPet(content))
                {
                    return this.NotFound();
                }

                if (content.Type == ContentType.Pet || content.Type == ContentType.LostPet)
                {
                    var model = content.ToPetModel(
                    this.contentService,
                    this.customTableService,
                    this.cacheManager,
                    this.workContext,
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
                    //this.ModelState.AddModelError("Id", "Este id no pertenece a un animal");
                    //return this.BadRequest(this.ModelState);
                    return this.NotFound();
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
                    content = model.ToEntity(this.contentSettings, this.contentService, this.workContext.CurrentUser.IsSuperAdmin(), files: model.Files);

                    content.FriendlyName = this.GenerateFriendlyName(model, content);

                    content.UserId = this.workContext.CurrentUserId;

                    ////Only if the user can aprove contents changes the status
                    if (this.workContext.CurrentUser.CanApproveContents())
                    {
                        content.StatusType = model.Status;
                    }
                    else
                    {
                        content.StatusType = model.Type == ContentType.Pet ? StatusType.Created : StatusType.Published;
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

                var createdUri = this.Url.Link("Api_Pets_GetById", new { friendlyName = content.Id });
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
                    var previousStatus = content.StatusType;

                    if (content.Type != ContentType.Pet && content.Type != ContentType.LostPet)
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
                    
                    content = model.ToEntity(this.contentSettings, this.contentService, this.workContext.CurrentUser.IsSuperAdmin(), content);

                    try
                    {
                        await this.contentService.UpdateAsync(content);

                        if (previousStatus == StatusType.Created && content.StatusType == StatusType.Published)
                        {
                            await this.publisher.Publish(new ContentAprovedModel() { Content = content });
                        }

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

        [Authorize]
        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<PetModel> patchDocument)
        {
            ////TODO:Test
            var content = this.contentService.GetById(id, true);

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

                var model = content.ToPetModel(this.contentService, this.customTableService, this.cacheManager, this.workContext, this.filesHelper);

                try
                {
                    patchDocument.ApplyTo(model);
                }
                catch (JsonPatchException e)
                {
                    this.logService.Error(e, this.workContext.CurrentUser);
                    return this.BadRequest(HuellitasExceptionCode.BadArgument, "Argumento invalido");
                }

                ////Un usuario sin permisos no puede cambiar un contenido de creado a publicado
                ////Si el estado no es creado si lo puede actualizar
                if (content.StatusType != StatusType.Created || (content.StatusType == StatusType.Created && content.StatusType != StatusType.Created && this.workContext.CurrentUser.CanApproveContents()))
                {
                    content.StatusType = model.Status;
                }

                content = model.ToEntity(this.contentSettings, this.contentService, this.workContext.CurrentUser.IsSuperAdmin(), content);

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
            this.ModelState.Remove("Shelter.Phone");
            this.ModelState.Remove("Shelter.Owner");
            this.ModelState.Remove("Shelter.Address");

            if (model.Type != ContentType.LostPet && model.Type != ContentType.Pet)
            {
                this.ModelState.AddModelError("Type", "Solo son validos los tipos lostPet y Pet");
            }

            if (isNew && (model.Files == null || model.Files.Count == 0))
            {
                this.ModelState.AddModelError("Files", "Al menos se debe cargar una imagen");
            }

            if (model.Type == ContentType.Pet)
            {
                if (model.Shelter == null && model.Location == null)
                {
                    this.ModelState.AddModelError("Location", "Si no ingresa la refugio debe ingresar ubicación");
                    this.ModelState.AddModelError("Shelter", "Si no ingresa la ubicación debe ingresar refugio");
                }
                else if (model.Shelter != null)
                {
                    if (!this.CanUserCreatePetsOnShelter(model.Shelter.Id))
                    {
                        this.ModelState.AddModelError("Shelter", "No tiene acceso a este refugio");
                    }
                }
            }
            else if (model.Type == ContentType.LostPet)
            {
                if (model.Location == null)
                {
                    this.ModelState.AddModelError("Location", "Si no ingresa la refugio debe ingresar ubicación");
                }

                if (!model.StartingDate.HasValue)
                {
                    this.ModelState.AddModelError("StartingDate", "Debe ingresar la fecha en que se perdió la mascota");
                }
                else if (model.StartingDate.Value > DateTime.Now)
                {
                    this.ModelState.AddModelError("StartingDate", "La fecha no puede ser mayor a la fecha actual");
                }

                if (model.Breed == null)
                {
                    this.ModelState.AddModelError("Breed", "Debe ingresar la raza del animal");
                }
            }

            return this.ModelState.IsValid;
        }

        /// <summary>
        /// Generates the friendly name
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the friendle name</returns>
        private string GenerateFriendlyName(PetModel model, Content content)
        {
            try
            {
                ////TODO:Test
                var subtype = this.customTableService.GetRowsByTableIdCached(CustomTableType.AnimalSubtype).FirstOrDefault(c => c.Id == model.Subtype.Value);
                var genre = this.customTableService.GetRowsByTableIdCached(CustomTableType.AnimalGenre).FirstOrDefault(c => c.Id == model.Genre.Value);
                var size = this.customTableService.GetRowsByTableIdCached(CustomTableType.AnimalSize).FirstOrDefault(c => c.Id == model.Size.Value);
                var location = this.locationService.GetCachedLocationById(content.LocationId.Value);
                var breed = content.Type == ContentType.LostPet ? this.customTableService.GetRowsByTableIdCached(CustomTableType.Breed).FirstOrDefault(c => c.Id == model.Breed.Value).Value : null;
                return this.seoService.GenerateFriendlyName($"{model.Name} {subtype.Value} {genre.Value} {size.Value} {location.Name} {breed}", this.contentRepository.Table);
            }
            catch (NullReferenceException)
            {
                throw new HuellitasException(HuellitasExceptionCode.InvalidForeignKey);
            }
        }

        /// <summary>
        /// Determines whether this instance [can user create pets on shelter] the specified shelter identifier.
        /// </summary>
        /// <param name="shelterId">The shelter identifier.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can user create pets on shelter] the specified shelter identifier; otherwise, <c>false</c>.
        /// </returns>
        public bool CanUserCreatePetsOnShelter(int shelterId)
        {
            if (this.workContext.CurrentUser.IsSuperAdmin())
            {
                return true;
            }
            else
            {
                return this.contentService.IsUserInContent(this.workContext.CurrentUserId, shelterId, ContentUserRelationType.Shelter);
            }
        }
    }
}