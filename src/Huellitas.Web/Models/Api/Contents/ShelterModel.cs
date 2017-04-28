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
    public class ShelterModel : BaseShelterModel
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