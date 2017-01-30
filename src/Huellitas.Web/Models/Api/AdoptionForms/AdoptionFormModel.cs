//-----------------------------------------------------------------------
// <copyright file="AdoptionFormModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.AdoptionForms
{
    using System;
    using System.Collections.Generic;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api.Common;
    using Huellitas.Web.Models.Api.Contents;
    using Huellitas.Web.Models.Api.Users;

    /// <summary>
    /// Adoption Form Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.Common.BaseModel" />
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
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
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
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the family members.
        /// </summary>
        /// <value>
        /// The family members.
        /// </value>
        public short FamilyMembers { get; set; }

        /// <summary>
        /// Gets or sets the job.
        /// </summary>
        /// <value>
        /// The job.
        /// </value>
        public ContentAttributeModel<int> Job { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public LocationModel Location { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the town.
        /// </summary>
        /// <value>
        /// The town.
        /// </value>
        public string Town { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public AdoptionFormAnswerStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public BaseUserModel User { get; set; }

        /// <summary>
        /// Gets or sets the answers.
        /// </summary>
        /// <value>
        /// The answers.
        /// </value>
        public IList<AdoptionFormAttributeModel> Answers { get; set; }
    }
}