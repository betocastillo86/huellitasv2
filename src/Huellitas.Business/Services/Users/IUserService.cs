//-----------------------------------------------------------------------
// <copyright file="IUserService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Users
{
    using System.Threading.Tasks;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Interface of user services
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The user</returns>
        User GetById(int id);

        /// <summary>
        /// Validates the authentication.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>The user</returns>
        Task<User> ValidateAuthentication(string email, string password);
    }
}