//-----------------------------------------------------------------------
// <copyright file="ContentAttributeExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Extensions
{
    using System;
    using System.Collections.Generic;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Content Attribute Extensions
    /// </summary>
    public static class ContentAttributeExtensions
    {
        /// <summary>
        /// Adds the specified attribute.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <param name="attribute">The attribute.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="ArgumentNullException">the value</exception>
        public static void Add(this ICollection<ContentAttribute> attributes, ContentAttributeType attribute, object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            attributes.Add(new ContentAttribute()
            {
                AttributeType = attribute,
                Value = value.ToString()
            });
        }
    }
}