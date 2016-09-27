//-----------------------------------------------------------------------
// <copyright file="FilterAttributeExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Extensions.Services
{
    using System.Collections.Generic;
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Services.Contents;
    using Huellitas.Data.Entities;
    
    /// <summary>
    /// Attribute Extensions for filters
    /// </summary>
    public static class FilterAttributeExtensions
    {
        /// <summary>
        /// Adds the specified attribute.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="attribute">The attribute.</param>
        /// <param name="value">The value.</param>
        /// <param name="type">The type.</param>
        /// <param name="valueTo">The value to.</param>
        public static void Add(this IList<FilterAttribute> list, ContentAttributeType attribute, object value, FilterAttributeType type = FilterAttributeType.Equals, object valueTo = null)
        {
            if (value == null)
            {
                return;
            }

            list.Add(new FilterAttribute()
            {
                Attribute = attribute,
                FilterType = type,
                Value = value,
                ValueTo = valueTo
            });
        }

        /// <summary>
        /// Adds the range attribute.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="attribute">The attribute.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="Huellitas.Business.Exceptions.HuellitasException">
        /// <![CDATA[Los valores indicados en el rango no son numericos]]>
        /// or
        /// <![CDATA[No se tiene un formato valido para los rangos deben ser: NumeroMenor-NumeroMayor]]>
        /// </exception>
        public static void AddRangeAttribute(this IList<FilterAttribute> list, ContentAttributeType attribute, string value)
        {
            if (value == null)
            {
                return;
            }

            var valueParts = value.ToString().Split(new char[] { '-' });

            ////Si no viene en rango no lo agrega
            if (valueParts.Length == 2)
            {
                int intValueFrom = 0;
                int intValueTo = 0;
                if (int.TryParse(valueParts[0], out intValueFrom) && int.TryParse(valueParts[1], out intValueTo))
                {
                    list.Add(attribute, intValueFrom, FilterAttributeType.Range, intValueTo);
                }
                else
                {
                    throw new HuellitasException(HuellitasExceptionCode.BadArgument, "Los valores indicados en el rango no son numericos");
                }
            }
            else
            {
                throw new HuellitasException(HuellitasExceptionCode.BadArgument, "No se tiene un formato valido para los rangos deben ser: NumeroMenor-NumeroMayor");
            }
        }
    }
}