//-----------------------------------------------------------------------
// <copyright file="SecurityHelpers.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Security
{
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Security Helpers
    /// </summary>
    /// <seealso cref="Huellitas.Business.Security.ISecurityHelpers" />
    public class SecurityHelpers : ISecurityHelpers
    {
        /// <summary>
        /// Converts To the MD5.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// the <c>md5</c> value
        /// </returns>
        public string ToMd5(string text)
        {
            //// step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(text);
            byte[] hash = md5.ComputeHash(inputBytes);

            //// step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString().ToLower();
        }

        /// <summary>
        /// To the <c>sha1</c>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// the hash
        /// </returns>
        public string ToSha1(string text)
        {
            using (var sha1 = SHA1.Create())
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] stream = null;
                StringBuilder sb = new StringBuilder();
                stream = sha1.ComputeHash(encoding.GetBytes(text));
                for (int i = 0; i < stream.Length; i++)
                {
                    sb.AppendFormat("{0:x2}", stream[i]);
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// To the <c>sha1</c> with a <c>salt</c> key
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>
        /// the hash
        /// </returns>
        public string ToSha1(string text, string salt)
        {
            return this.ToSha1($"{text}.{salt}");
        }
    }
}