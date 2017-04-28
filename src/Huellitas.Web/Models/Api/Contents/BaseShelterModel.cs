namespace Huellitas.Web.Models.Api
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Base Shelter Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.ContentBaseModel" />
    public class BaseShelterModel : ContentBaseModel
    {
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [Required]
        [StringLength(80, MinimumLength = 5)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        [Required]
        [StringLength(11, MinimumLength = 7)]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the phone2.
        /// </summary>
        /// <value>
        /// The phone2.
        /// </value>
        [StringLength(11, MinimumLength = 7)]
        public string Phone2 { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        [Required]
        [StringLength(80, MinimumLength = 5)]
        public string Owner { get; set; }
    }
}