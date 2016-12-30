//-----------------------------------------------------------------------
// <copyright file="UserService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Users
{
    using System.Linq;
    using System.Threading.Tasks;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// User Service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.Users.IUserService" />
    public class UserService : IUserService
    {
        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IRepository<User> userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public UserService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Validates the authentication.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// The user
        /// </returns>
        public async Task<User> ValidateAuthentication(string email, string password)
        {
            return await this.userRepository.Table
                .Include(c => c.Role)
                .FirstOrDefaultAsync(c => c.Email.Equals(email) && c.Password.Equals(password) && !c.Deleted);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The user
        /// </returns>
        public User GetById(int id)
        {
            return this.userRepository.Table.FirstOrDefault(c => c.Id == id && !c.Deleted);
        }
    }
}