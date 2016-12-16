//-----------------------------------------------------------------------
// <copyright file="ContentAttributeExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        /// <param name="replace">replace the attribute</param>
        /// <exception cref="ArgumentNullException">the value</exception>
        public static void Add(this ICollection<ContentAttribute> attributes, ContentAttributeType attribute, object value, bool replace = false)
        {
            ////TODO:Test
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            //if the attribute can be replace, it will search it and replaces the value
            if (replace)
            {
                var existentAttribute = attributes.FirstOrDefault(c => c.AttributeType == attribute);
                if (existentAttribute != null)
                {
                    existentAttribute.Value = value.ToString();
                }
                else
                {
                    attributes.Add(new ContentAttribute()
                    {
                        AttributeType = attribute,
                        Value = value.ToString()
                    });
                }
            }
            else
            {
                attributes.Add(new ContentAttribute()
                {
                    AttributeType = attribute,
                    Value = value.ToString()
                });
            }
        }
    }
}