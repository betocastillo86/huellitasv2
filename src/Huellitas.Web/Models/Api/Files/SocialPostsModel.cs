//-----------------------------------------------------------------------
// <copyright file="SocialPostsModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.Files
{
    using Huellitas.Business.Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Facebook Posts Model
    /// </summary>
    public class SocialPostsModel
    {
        /// <summary>
        /// Gets or sets the file identifier.
        /// </summary>
        /// <value>
        /// The file identifier.
        /// </value>
        public int? FileId { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public SocialPostColors Color { get; set; } = SocialPostColors.Blue;

        /// <summary>
        /// Gets or sets the social network.
        /// </summary>
        /// <value>
        /// The social network.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public SocialNetwork SocialNetwork { get; set; } = SocialNetwork.Facebook;
    }
}