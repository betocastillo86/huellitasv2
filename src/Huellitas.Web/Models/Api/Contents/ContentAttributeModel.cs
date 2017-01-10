//-----------------------------------------------------------------------
// <copyright file="ContentAttributeModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models
{
    using Huellitas.Data.Entities;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Content Attribute Model Not Typed
    /// </summary>
    public class ContentAttributeModel
    {
        /// <summary>
        /// Gets or sets the attribute.
        /// </summary>
        /// <value>
        /// The attribute.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public ContentAttributeType Attribute { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }
    }
}