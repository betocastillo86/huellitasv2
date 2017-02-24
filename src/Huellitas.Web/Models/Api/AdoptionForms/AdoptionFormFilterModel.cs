//-----------------------------------------------------------------------
// <copyright file="AdoptionFormFilterModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.AdoptionForms
{
    using System;
    using Business.Services.Contents;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api.Common;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Adoption Form Filter Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.Common.BaseFilterModel" />
    public class AdoptionFormFilterModel : BaseFilterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdoptionFormFilterModel"/> class.
        /// </summary>
        public AdoptionFormFilterModel()
        {
            this.MaxPageSize = 30;
            this.ValidOrdersBy = new string[] { AdoptionFormOrderBy.Pet.ToString(), AdoptionFormOrderBy.CreationDate.ToString(), AdoptionFormOrderBy.Name.ToString() };
        }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the pet identifier.
        /// </summary>
        /// <value>
        /// The pet identifier.
        /// </value>
        public int? PetId { get; set; }

        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int? LocationId { get; set; }

        /// <summary>
        /// Gets or sets the shelter identifier.
        /// </summary>
        /// <value>
        /// The shelter identifier.
        /// </value>
        public int? ShelterId { get; set; }

        /// <summary>
        /// Gets or sets the shared to user identifier.
        /// </summary>
        /// <value>
        /// The shared to user identifier.
        /// </value>
        public int? SharedToUserId { get; set; }

        /// <summary>
        /// Gets or sets the order by enum.
        /// </summary>
        /// <value>
        /// The order by enum.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public AdoptionFormOrderBy OrderByEnum { get; set; }

        /// <summary>
        /// Gets or sets the content identifier.
        /// </summary>
        /// <value>
        /// The content identifier.
        /// </value>
        public int? ContentId { get; set; }

        /// <summary>
        /// Gets or sets the form user identifier.
        /// </summary>
        /// <value>
        /// The form user identifier.
        /// </value>
        public int? FormUserId { get; set; }

        /// <summary>
        /// Gets or sets the content user identifier.
        /// </summary>
        /// <value>
        /// The content user identifier.
        /// </value>
        public int? ContentUserId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public AdoptionFormAnswerStatus? Status { get; set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="contentService">The content service.</param>
        /// <param name="canSeeAll">if set to <c>true</c> [can see all].</param>
        /// <returns>
        ///   <c>true</c> if the specified user identifier is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid(int userId, IContentService contentService, bool canSeeAll)
        {
            ////TODO:Test again with shared userid
            var orderByEnum = AdoptionFormOrderBy.CreationDate;
            Enum.TryParse(this.OrderBy, out orderByEnum);
            this.OrderByEnum = orderByEnum;

            if (!canSeeAll)
            {
                if (this.ShelterId.HasValue)
                {
                    if (!contentService.IsUserInContent(userId, this.ShelterId.Value, Data.Entities.Enums.ContentUserRelationType.Shelter))
                    {
                        this.AddError("ShelterId", "No puede acceder a los datos del id de refugio enviado");
                    }
                }
                else if (this.FormUserId.HasValue)
                {
                    if (this.FormUserId != userId)
                    {
                        this.AddError("FormUserId", "No puede acceder a los datos del id de usuario enviado");
                    }
                }
                else if (this.ContentUserId.HasValue)
                {
                    if (this.ContentUserId != userId)
                    {
                        this.AddError("ContentUserId", "No puede acceder a los datos del id de usuario enviado");
                    }
                }
                else if (this.SharedToUserId.HasValue)
                {
                    if (this.SharedToUserId != userId)
                    {
                        this.AddError("SharedToUserId", "No puede acceder a los datos del id de usuario enviado");
                    }
                }
                else
                {
                    this.AddError("FormUserId", "Debe venir o el usuario dueño de la mascota, o el usuario que llenó el formulario o el refugio para realizar el filtro");
                }
            }

            return base.IsValid();
        }
    }
}