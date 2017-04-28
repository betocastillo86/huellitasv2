//-----------------------------------------------------------------------
// <copyright file="WebSecurityHelpers.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Security
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Web Security Helpers
    /// </summary>
    public static class WebSecurityHelpers
    {
        /// <summary>
        /// Filters a string with the Cross Site Scripting validation.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>the string filtered</returns>
        public static string ToXXSFilteredString(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            var rgx = new Regex("(<|>|/|\\\"|;|:|\\)|\\()");
            return rgx.Replace(value, string.Empty);
        }
    }
}