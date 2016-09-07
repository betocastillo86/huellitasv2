using Huellitas.Business.Services.Contents;
using Huellitas.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Business.Extensions.Services
{
    public static class FilterAttributeExtensions
    {
        public static void Add(this IList<FilterAttribute> list, ContentAttributeType attribute, object value, FilterAttributeType type = FilterAttributeType.Equals, object valueTo = null)
        {
            if (value == null)
                return;

            list.Add(new FilterAttribute() {
                  Attribute = attribute,
                  FilterType = type,
                  Value = value,
                  ValueTo = valueTo
            });
        }


        public static void AddRangeAttribute(this IList<FilterAttribute> list, ContentAttributeType attribute, string value)
        {
            if (value == null)
                return;

            var valueParts = value.ToString().Split(new char[] { '-' });
            string valueTo = null;
            //Si no viene en rango no lo agrega
            if (valueParts.Length == 2)
            {
                value = valueParts[0];
                valueTo = valueParts[1];
            }
            else
            {
                return;
            }

            list.Add(attribute, value, FilterAttributeType.Range, valueTo);
        }
    }
}
