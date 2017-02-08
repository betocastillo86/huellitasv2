//-----------------------------------------------------------------------
// <copyright file="AdoptionFormAnswerModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.AdoptionForms
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api.Common;
    using Huellitas.Web.Models.Api.Users;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Adoption Form Answer Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.Common.BaseModel" />
    public class AdoptionFormAnswerModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the adoption form identifier.
        /// </summary>
        /// <value>
        /// The adoption form identifier.
        /// </value>
        public int AdoptionFormId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public AdoptionFormAnswerStatus? Status { get; set; }

        /// <summary>
        /// Gets or sets the additional information.
        /// </summary>
        /// <value>
        /// The additional information.
        /// </value>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public BaseUserModel User { get; set; }
    }
}