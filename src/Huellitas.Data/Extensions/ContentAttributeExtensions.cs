//-----------------------------------------------------------------------
// <copyright file="ContentAttributeExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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

            ////if the attribute can be replace, it will search it and replaces the value
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

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns>the attribute</returns>
        public static ContentAttribute GetAttribute(this Content content, ContentAttributeType attribute)
        {
            if (content.ContentAttributes != null)
            {
                return content.ContentAttributes.FirstOrDefault(c => c.Attribute == attribute.ToString());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the attribute value
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns>the value</returns>
        public static T GetAttribute<T>(this Content content, ContentAttributeType attribute) 
        {
            var contentAttribute = content.GetAttribute(attribute);

            if (contentAttribute != null)
            {
                var destinationConverter = TypeDescriptor.GetConverter(typeof(T));
                return (T)destinationConverter.ConvertFrom(null, System.Globalization.CultureInfo.InvariantCulture, contentAttribute.Value);
            }
            else
            {
                return default(T);
            }
        }
    }
}