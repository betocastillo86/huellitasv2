using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Web.Extensions
{
    public static class StringExtensions
    {
        public static string[] ToStringList(this string value, bool returnDefaultNull = true)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value.Split(new char[] { ',' });
            }

            return returnDefaultNull ? null : new string[0];
        }

        public static int[] ToIntList(this string value, bool returnDefaultNull = true)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                try
                {

                    return value.Split(new char[] { ',' }).Select(c => Convert.ToInt32(c)).ToArray();
                }
                catch (FormatException)
                {
                    throw;
                }
            }

            return returnDefaultNull ? null : new int[0];
        }
    }
}
