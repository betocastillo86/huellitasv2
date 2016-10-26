//-----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Huellitas.Business.Utilities.Extensions
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Extensions for strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Determines whether this instance is number.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is number; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNumber(this string value)
        {
            if (value == null)
            {
                return false;
            }

            var number = 0;
            return int.TryParse(value, out number);
        }

        /// <summary>
        /// Determines whether [is valid integer list].
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///   <c>true</c> if [is validate integer list] [the specified text]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidIntList(this string text)
        {
            return Regex.IsMatch(text, @"^(\d+)(,\d+)*$");
        }

        /// <summary>
        /// To the integer list.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="returnDefaultNull">if set to <c>true</c> [return default null].</param>
        /// <returns>the value</returns>
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

        /// <summary>
        /// To the string list.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="returnDefaultNull">if set to <c>true</c> [return default null].</param>
        /// <returns>the value</returns>
        public static string[] ToStringList(this string value, bool returnDefaultNull = true)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value.Split(new char[] { ',' });
            }

            return returnDefaultNull ? null : new string[0];
        }
    }
}