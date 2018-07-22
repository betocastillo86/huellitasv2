//-----------------------------------------------------------------------
// <copyright file="BannersController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using System;
    using System.Threading.Tasks;
    using Beto.Core.Data.Files;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Beto.Core.Web.Api.Filters;
    using Huellitas.Business.Configuration;
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api;
    using Huellitas.Web.Models.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Banners Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/banners")]
    public class BannersController : BaseApiController
    {
        /// <summary>
        /// The banner service
        /// </summary>
        private readonly IBannerService bannerService;

        /// <summary>
        /// The file service
        /// </summary>
        private readonly IFileService fileService;

        /// <summary>
        /// The files helper
        /// </summary>
        private readonly IFilesHelper filesHelper;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// The general settings
        /// </summary>
        private readonly IGeneralSettings generalSettings;

        /// <summary>
        /// The picture service
        /// </summary>
        private readonly IPictureService pictureService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BannersController"/> class.
        /// </summary>
        /// <param name="bannerService">The banner service.</param>
        /// <param name="generalSettings">The general settings.</param>
        /// <param name="filesHelper">file helper</param>
        /// <param name="fileService">file service</param>
        public BannersController(
            IBannerService bannerService,
            IGeneralSettings generalSettings,
            IFilesHelper filesHelper,
            IFileService fileService,
            IWorkContext workContext,
            IPictureService pictureService,
            IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
            this.bannerService = bannerService;
            this.generalSettings = generalSettings;
            this.filesHelper = filesHelper;
            this.fileService = fileService;
            this.workContext = workContext;
            this.pictureService = pictureService;
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the action</returns>
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            var banner = this.bannerService.GetById(id, false);
            if (banner != null)
            {
                await this.bannerService.Delete(banner);
                return this.Ok(new { result = true });
            }
            else
            {
                return this.NotFound();
            }
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the action</returns>
        [HttpGet]
        public IActionResult Get([FromQuery]BannerFilterModel filter)
        {
            ////TODO:Test
            filter = filter ?? new BannerFilterModel();

            var canSelectInactive = this.workContext.CurrentUser.IsSuperAdmin();

            if (filter.IsValid(canSelectInactive))
            {
                var banners = this.bannerService.GetAll(
                    filter.Section,
                    filter.Active,
                    filter.Keyword,
                    filter.Page,
                    filter.PageSize,
                    filter.OrderByEnum);

                var models = banners.ToModels(this.filesHelper, Url.Content, this.generalSettings.BannerPictureSizeWidth, this.generalSettings.BannerPictureSizeHeight);
                return this.Ok(models, banners.HasNextPage, banners.TotalCount);
            }
            else
            {
                return this.BadRequest(filter.Errors);
            }
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the action</returns>
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            ////TODO:Test
            var banner = this.bannerService.GetById(id);
            if (banner != null)
            {
                var model = banner.ToModel(
                    this.filesHelper,
                    Url.Content,
                    this.generalSettings.BannerPictureSizeWidth,
                    this.generalSettings.BannerPictureSizeHeight);

                return this.Ok(model);
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
        [RequiredModel]
        public async Task<IActionResult> Post([FromBody]BannerModel model)
        {
            ////TODO:Test
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            if (this.IsValidModel(model))
            {
                var banner = model.ToEntity();

                try
                {
                    await this.bannerService.Insert(banner);
                }
                catch (HuellitasException e)
                {
                    return this.BadRequest(e);
                }

                model.Id = banner.Id;

                if (banner.FileId.HasValue)
                {
                    var file = this.fileService.GetById(banner.FileId.Value);
                    var image = this.pictureService.GetPicturePath(file, this.generalSettings.BannerPictureSizeWidth, this.generalSettings.BannerPictureSizeHeight, true);
                    model.FileUrl = Url.Content(image);
                }

                return this.Ok(model);
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
        /// <returns>the action</returns>
        [HttpPut]
        [Authorize]
        [Route("{id:int}")]
        [RequiredModel]
        public async Task<IActionResult> Put(int id, [FromBody]BannerModel model)
        {
            ////TODO:Test
            if (!this.workContext.CurrentUser.IsSuperAdmin())
            {
                return this.Forbid();
            }

            if (this.IsValidModel(model))
            {
                var banner = this.bannerService.GetById(id, true);

                if (banner != null)
                {
                    banner.Name = model.Name;
                    banner.EmbedUrl = model.EmbedUrl;
                    banner.Body = model.Body;
                    banner.Section = model.Section.Value;
                    banner.Active = model.Active;
                    banner.DisplayOrder = model.DisplayOrder;
                    banner.FileId = model.FileId;

                    try
                    {
                        await this.bannerService.Update(banner);
                    }
                    catch (HuellitasException e)
                    {
                        return this.BadRequest(e);
                    }

                    if (banner.FileId.HasValue)
                    {
                        var file = banner.File;
                        if (file == null)
                        {
                            file = this.fileService.GetById(banner.FileId.Value);
                        }

                        var image = this.pictureService.GetPicturePath(file, this.generalSettings.BannerPictureSizeWidth, this.generalSettings.BannerPictureSizeHeight, true);
                        model.FileUrl = Url.Content(image);
                    }

                    return this.Ok(new { FileUrl = model.FileUrl });
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
        /// Determines whether [is valid model] [the specified banner].
        /// </summary>
        /// <param name="model">The banner.</param>
        /// <returns>
        ///   <c>true</c> if [is valid model] [the specified banner]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsValidModel(BannerModel model)
        {
            if (model == null)
            {
                return false;
            }

            if (model.Section != null && !Enum.IsDefined(typeof(BannerSection), model.Section.ToString()))
            {
                this.ModelState.AddModelError("Section", "La sección debe tener un valor válido");
            }

            return this.ModelState.IsValid;
        }
    }
}