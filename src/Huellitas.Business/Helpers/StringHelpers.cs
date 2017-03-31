//-----------------------------------------------------------------------
// <copyright file="NotificationFilterModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Helpers
{
    using System;
    using System.Linq;

    /// <summary>
    /// String Helpers
    /// </summary>
    /// <seealso cref="Huellitas.Business.Helpers.IStringHelpers" />
    public class StringHelpers : IStringHelpers
    {
        /// <summary>
        /// The random
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// Gets the random string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns>
        /// the string
        /// </returns>
        public string GetRandomString(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._/{}%&()!#-*¡?¿";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}