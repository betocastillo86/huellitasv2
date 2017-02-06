//-----------------------------------------------------------------------
// <copyright file="UserService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Users
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Entities.Enums;
    using Data.Infraestructure;
    using EventPublisher;
    using Exceptions;
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
        /// The publisher
        /// </summary>
        private readonly IPublisher publisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="publisher">The publisher.</param>
        public UserService(
            IRepository<User> userRepository,
            IPublisher publisher)
        {
            this.userRepository = userRepository;
            this.publisher = publisher;
        }

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task Delete(User user)
        {
            user.Deleted = true;

            await this.userRepository.UpdateAsync(user);

            this.publisher.EntityDeleted(user);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="role">The role.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// the list of users
        /// </returns>
        public async Task<IPagedList<User>> GetAll(
            string keyword = null,
            RoleEnum? role = null,
            int page = 0,
            int pageSize = int.MaxValue)
        {
            var query = this.userRepository.Table.Where(c => !c.Deleted);

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => c.Name.Contains(keyword) || c.Email.Contains(keyword));
            }

            if (role.HasValue)
            {
                var rolId = Convert.ToInt32(role);
                query = query.Where(c => c.RoleId == rolId);
            }

            query = query.OrderByDescending(c => c.CreatedDate);

            return await new PagedList<User>().Async(query, page, pageSize);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The user
        /// </returns>
        public async Task<User> GetById(int id)
        {
            return await this.userRepository.Table
                .FirstOrDefaultAsync(c => c.Id == id && !c.Deleted);
        }

        /// <summary>
        /// Inserts the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task Insert(User user)
        {
            user.CreatedDate = DateTime.Now;

            try
            {
                await this.userRepository.InsertAsync(user);

                this.publisher.EntityInserted(user);
            }
            catch (DbUpdateException e)
            {
                if (e.ToString().Contains("'IX_User'"))
                {
                    throw new HuellitasException(HuellitasExceptionCode.UserEmailAlreadyUsed);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task Update(User user)
        {
            try
            {
                await this.userRepository.UpdateAsync(user);

                this.publisher.EntityUpdated(user);
            }
            catch (DbUpdateException e)
            {
                if (e.ToString().Contains("'IX_User'"))
                {
                    throw new HuellitasException(HuellitasExceptionCode.UserEmailAlreadyUsed);
                }
                else
                {
                    throw;
                }
            }
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
    }
}