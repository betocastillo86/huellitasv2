//-----------------------------------------------------------------------
// <copyright file="IStringHelpers.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Helpers
{
    /// <summary>
    /// Interface of string helpers
    /// </summary>
    public interface IStringHelpers
    {
        /// <summary>
        /// Gets the random string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns>the string</returns>
        string GetRandomString(int length = 6);
    }
}