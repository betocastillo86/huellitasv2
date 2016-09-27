//-----------------------------------------------------------------------
// <copyright file="HashesGenerator.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Security
{
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Generate hashes
    /// </summary>
    public static class HashesGenerator
    {
        /// <summary>
        /// To the m d5.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>the value</returns>
        public static string ToMD5(string input)
        {
            //// step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            //// step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}