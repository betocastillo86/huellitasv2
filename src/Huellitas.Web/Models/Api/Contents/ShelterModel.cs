//-----------------------------------------------------------------------
// <copyright file="ShelterModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using Huellitas.Web.Models.Api.Users;

namespace Huellitas.Web.Models.Api.Contents
{
    /// <summary>
    /// Shelter Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.Contents.ContentBaseModel" />
    public class ShelterModel : ContentBaseModel
    {
        /// <summary>
        /// Gets or sets the video.
        /// </summary>
        /// <value>
        /// The video.
        /// </value>
        public string Video { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the phone2.
        /// </summary>
        /// <value>
        /// The phone2.
        /// </value>
        public string Phone2 { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public string Owner { get; set; }

        /// <summary>
        /// Gets or sets the lat.
        /// </summary>
        /// <value>
        /// The lat.
        /// </value>
        public decimal Lat { get; set; }

        /// <summary>
        /// Gets or sets the LNG.
        /// </summary>
        /// <value>
        /// The LNG.
        /// </value>
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
        /// Gets or sets the instagram.
        /// </summary>
        /// <value>
        /// The instagram.
        /// </value>
        public string Instagram { get; set; }
    }
}