//-----------------------------------------------------------------------
// <copyright file="IExternalAuthenticationService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System;
    using System.Threading.Tasks;
    using Beto.Core.Data.Users;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Interface of external authentication service
    /// </summary>
    public interface IExternalAuthenticationService
    {
        /// <summary>
        /// Gets the facebook user.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>the facebook user</returns>
        Task<FacebookUserModel> GetFacebookUser(string token);

        /// <summary>
        /// Tries the authenticate.
        /// </summary>
        /// <param name="socialNetwork">The social network.</param>
        /// <param name="token">The token.</param>
        /// <param name="token2">The token2.</param>
        /// <returns>the user authenticated</returns>
        Task<Tuple<bool, User>> TryAuthenticate(SocialLoginType socialNetwork, string token, string token2);
    }
}