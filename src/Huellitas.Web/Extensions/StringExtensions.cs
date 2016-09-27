﻿//-----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace Huellitas.Web.Extensions
{
    using System;
    using System.Linq;

    /// <summary>
    /// Extensions for strings
    /// </summary>
    public static class StringExtensions
    {
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
    }
}