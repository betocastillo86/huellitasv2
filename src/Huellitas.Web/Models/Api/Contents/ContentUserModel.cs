//-----------------------------------------------------------------------
// <copyright file="ContentUserModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.Contents
{
    using System.ComponentModel.DataAnnotations;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Entities.Enums;
    using Huellitas.Web.Models.Api.Common;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Content User Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.Common.BaseModel" />
    public class ContentUserModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the content identifier.
        /// </summary>
        /// <value>
        /// The content identifier.
        /// </value>
        public int ContentId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [Required]
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the type of the relation.
        /// </summary>
        /// <value>
        /// The type of the relation.
        /// </value>
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public ContentUserRelationType? RelationType { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public Content Content { get; set; }

        /// <summary>
        /// Gets or sets my property.
        /// </summary>
        /// <value>
        /// My property.
        /// </value>
        public User MyProperty { get; set; }
    }
}