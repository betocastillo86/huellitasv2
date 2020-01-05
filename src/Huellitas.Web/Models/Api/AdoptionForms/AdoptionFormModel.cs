//-----------------------------------------------------------------------
// <copyright file="AdoptionFormModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Adoption Form Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.BaseModel" />
    public class AdoptionFormModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public ContentBaseModel Content { get; set; }

        /// <summary>
        /// Gets or sets the content identifier.
        /// </summary>
        /// <value>
        /// The content identifier.
        /// </value>
        [Required]
        public int? ContentId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        [Required]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the family members.
        /// </summary>
        /// <value>
        /// The family members.
        /// </value>
        [Required]
        public short? FamilyMembers { get; set; }

        /// <summary>
        /// Gets or sets the family members age.
        /// </summary>
        /// <value>
        /// The family members age.
        /// </value>
        [Required]
        public string FamilyMembersAge { get; set; }

        /// <summary>
        /// Gets or sets the job.
        /// </summary>
        /// <value>
        /// The job.
        /// </value>
        [Required]
        public ContentAttributeModel<int> Job { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        [Required]
        public LocationModel Location { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [Required]
        [StringLength(100, MinimumLength = 7)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the town.
        /// </summary>
        /// <value>
        /// The town.
        /// </value>
        [Required]
        public string Town { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public AdoptionFormAnswerStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public BaseUserModel User { get; set; }

        public BaseUserModel LastResponseUser { get; set; }

        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        public IList<AdoptionFormAttributeModel> Attributes { get; set; }

        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        public IList<AdoptionFormAnswerModel> Answers { get; set; }
    }
}