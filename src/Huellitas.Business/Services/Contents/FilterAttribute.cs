//-----------------------------------------------------------------------
// <copyright file="FilterAttribute.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Contents
{
    using Huellitas.Data.Entities;

    /// <summary>
    /// Attribute Filter
    /// </summary>
    public class FilterAttribute
    {
        /// <summary>
        /// Gets or sets the attribute.
        /// </summary>
        /// <value>
        /// The attribute.
        /// </value>
        public ContentAttributeType Attribute { get; set; }

        /// <summary>
        /// Gets or sets the type of the filter.
        /// </summary>
        /// <value>
        /// The type of the filter.
        /// </value>
        public FilterAttributeType FilterType { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the value to.
        /// </summary>
        /// <value>
        /// The value to.
        /// </value>
        public object ValueTo { get; set; }
    }
}