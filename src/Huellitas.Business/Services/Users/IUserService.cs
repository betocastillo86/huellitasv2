//-----------------------------------------------------------------------
// <copyright file="IUserService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Users
{
    using System.Threading.Tasks;
    using Data.Entities.Enums;
    using Data.Infraestructure;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Interface of user services
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>the task</returns>
        Task Delete(User user);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="role">The role.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>the list of users</returns>
        Task<IPagedList<User>> GetAll(
            string keyword = null,
            RoleEnum? role = null,
            int page = 0,
            int pageSize = int.MaxValue);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The user</returns>
        Task<User> GetByIdAsync(int id);

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the user</returns>
        User GetById(int id);

        /// <summary>
        /// Inserts the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>the task</returns>
        Task Insert(User user);

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>the task</returns>
        Task Update(User user);

        /// <summary>
        /// Validates the authentication.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>The user</returns>
        Task<User> ValidateAuthentication(string email, string password);
    }
}