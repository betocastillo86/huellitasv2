//-----------------------------------------------------------------------
// <copyright file="ShelterModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Shelter Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.ContentBaseModel" />
    public class ShelterModel : ContentBaseModel
    {
        /// <summary>
        /// Gets or sets the video.
        /// </summary>
        /// <value>
        /// The video.
        /// </value>
        [StringLength(200, MinimumLength = 5)]
        public string Video { get; set; }

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

        /// <summary>
        /// Gets or sets the latitude
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        [Required]
        public decimal Lat { get; set; }

        /// <summary>
        /// Gets or sets the LNG.
        /// </summary>
        /// <value>
        /// The LNG.
        /// </value>
        [Required]
        public decimal Lng { get; set; }

        /// <summary>
        /// Gets or sets the facebook.
        /// </summary>
        /// <value>
        /// The facebook.
        /// </value>
        public string Facebook { get; set; }

        /// <summary>
        /// Gets or sets the twitter.
        /// </summary>
        /// <value>
        /// The twitter.
        /// </value>
        public string Twitter { get; set; }

        /// <summary>
        /// Gets or sets the <c>instagram</c>.
        /// </summary>
        /// <value>
        /// The <c>instagram</c>.
        /// </value>
        public string Instagram { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ShelterModel"/> is autoreply.
        /// </summary>
        /// <value>
        ///   <c>true</c> if autoreply; otherwise, <c>false</c>.
        /// </value>
        public bool AutoReply { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public IList<ContentUserModel> Users { get; set; }
    }
}