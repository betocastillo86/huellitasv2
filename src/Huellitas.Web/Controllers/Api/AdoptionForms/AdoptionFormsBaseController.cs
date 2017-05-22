//-----------------------------------------------------------------------
// <copyright file="AdoptionFormsBaseController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using Business.Services;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Security;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Infraestructure.WebApi;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Adoption Form Base Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    public class AdoptionFormsBaseController : BaseApiController
    {
        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// The content service
        /// </summary>
        private readonly IContentService contentService;

        /// <summary>
        /// The adoption form service
        /// </summary>
        private readonly IAdoptionFormService adoptionFormService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdoptionFormsBaseController"/> class.
        /// </summary>
        /// <param name="workContext">The work context.</param>
        /// <param name="contentService">The content service.</param>
        /// <param name="adoptionFormService">The adoption form service.</param>
        public AdoptionFormsBaseController(
            IWorkContext workContext,
            IContentService contentService,
            IAdoptionFormService adoptionFormService)
        {
            this.workContext = workContext;
            this.contentService = contentService;
            this.adoptionFormService = adoptionFormService;
        }

        /// <summary>
        /// Determines whether this instance [can see form] the specified form.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can see form] the specified form; otherwise, <c>false</c>.
        /// </returns>
        [NonAction]
        public bool CanSeeForm(AdoptionForm form)
        {
            ////Si es el que llena el formulario
            if (this.workContext.CurrentUserId == form.UserId)
            {
                return true;
            }
            else if (this.workContext.CurrentUserId == form.Content.UserId)
            {
                ////Si es el dueño del contenido
                return true;
            }
            else if (this.workContext.CurrentUser.IsSuperAdmin())
            {
                ////Si es admin
                return true;
            }
            else if (this.contentService.IsUserInContent(this.workContext.CurrentUserId, form.ContentId, ContentUserRelationType.Parent))
            {
                ////Si es padrino
                return true;
            }
            else if (this.adoptionFormService.IsUserInAdoptionForm(this.workContext.CurrentUserId, form.Id))
            {
                ////Si está entre los usuarios habilitados
                return true;
            }
            else
            {
                ////Valida si el usuario es perteneciente al refugio
                var shelter = this.contentService.GetContentAttribute<int?>(form.ContentId, ContentAttributeType.Shelter);
                if (shelter.HasValue)
                {
                    return this.contentService.IsUserInContent(this.workContext.CurrentUserId, shelter.Value, ContentUserRelationType.Shelter);
                }
                else
                {
                    return false;
                }
            }
        }
    }
}