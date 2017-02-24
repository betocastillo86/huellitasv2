//-----------------------------------------------------------------------
// <copyright file="AdoptionFormUserModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.AdoptionForms
{
    using System.ComponentModel.DataAnnotations;
    using Huellitas.Web.Models.Api.Users;

    /// <summary>
    /// Adoption Form User Model
    /// </summary>
    public class AdoptionFormUserModel : BaseUserModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the adoption form identifier.
        /// </summary>
        /// <value>
        /// The adoption form identifier.
        /// </value>
        public int AdoptionFormId { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public BaseUserModel User { get; set; }

        /// <summary>
        /// Gets or sets the adoption form.
        /// </summary>
        /// <value>
        /// The adoption form.
        /// </value>
        public AdoptionFormModel AdoptionForm { get; set; }
    }
}