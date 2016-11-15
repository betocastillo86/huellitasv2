//-----------------------------------------------------------------------
// <copyright file="ISecurityHelpers.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Security
{
    /// <summary>
    /// Interface of security helper
    /// </summary>
    public interface ISecurityHelpers
    {
        /// <summary>
        /// Converts To the MD5.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>the <c>md5</c> value</returns>
        string ToMd5(string text);

        /// <summary>
        /// To the <c>sha1</c>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>the hash</returns>
        string ToSha1(string text);

        /// <summary>
        /// To the <c>sha1</c> with a <c>salt</c> key
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>the hash</returns>
        string ToSha1(string text, string salt);
    }
}