//-----------------------------------------------------------------------
// <copyright file="ContentAttributeModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.Contents
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Content Attribute Model
    /// </summary>
    /// <typeparam name="T">Any type</typeparam>
    public class ContentAttributeModel<T>
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [Required]
        public T Value { get; set; }
    }
}