//-----------------------------------------------------------------------
// <copyright file="TextResourceModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api
{
    using System.ComponentModel.DataAnnotations;
    using Huellitas.Data.Entities;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Text Resource Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.BaseModel" />
    public class TextResourceModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public LanguageEnum Language { get; set; }
    }
}