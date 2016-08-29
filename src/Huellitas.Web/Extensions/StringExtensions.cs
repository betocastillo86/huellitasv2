using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Web.Extensions
{
    public static class StringExtensions
    {
        public static int[] ToIntList(this string value, bool returnDefaultNull = true)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                try
                {
                    return value.Split(new char[] { ',' })
                        .Select(v => Convert.ToInt32(v))
                        .ToArray();
                }
                catch (FormatException e)
                {
                    ///TODO:Guardar la excepción
                }
            }

            return returnDefaultNull ? null : new int[0];
        }
    }
}
