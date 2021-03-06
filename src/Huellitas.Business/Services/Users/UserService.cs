﻿//-----------------------------------------------------------------------
// <copyright file="UserService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Beto.Core.EventPublisher;
    using Beto.Core.Helpers;
    using Data.Entities;
    using Exceptions;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// User Service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.IUserService" />
    public class UserService : IUserService
    {
        /// <summary>
        /// The HTTP context helpers
        /// </summary>
        private readonly IHttpContextHelper httpContextHelpers;

        /// <summary>
        /// The publisher
        /// </summary>
        private readonly IPublisher publisher;

        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IRepository<User> userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="publisher">The publisher.</param>
        /// <param name="httpContextHelpers">The HTTP context helpers.</param>
        public UserService(
            IRepository<User> userRepository,
            IPublisher publisher,
            IHttpContextHelper httpContextHelpers)
        {
            this.userRepository = userRepository;
            this.publisher = publisher;
            this.httpContextHelpers = httpContextHelpers;
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

            await this.publisher.EntityDeleted(user);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="role">The role.</param>
        /// <param name="email">filter by email</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// the list of users
        /// </returns>
        public async Task<IPagedList<User>> GetAll(
            string keyword = null,
            RoleEnum? role = null,
            string email = null,
            int page = 0,
            int pageSize = int.MaxValue)
        {
            var query = this.userRepository.Table.Where(c => !c.Deleted);

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => c.Name.Contains(keyword) || c.Email.Contains(keyword));
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(c => c.Email.Equals(email));
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
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the user</returns>
        public User GetById(int id)
        {
            return this.userRepository.Table
                .Include(c => c.Location)
                .FirstOrDefault(c => c.Id == id && !c.Deleted);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The user
        /// </returns>
        public async Task<User> GetByIdAsync(int id)
        {
            return await this.userRepository.Table
                .Include(c => c.Location)
                .FirstOrDefaultAsync(c => c.Id == id && !c.Deleted);
        }

        /// <summary>
        /// Gets the user by password token.
        /// </summary>
        /// <param name="passwordToken">The password token.</param>
        /// <returns>
        /// the user
        /// </returns>
        public async Task<User> GetByPasswordToken(string passwordToken)
        {
            return await this.userRepository.Table.FirstOrDefaultAsync(c => c.PasswordRecoveryToken == passwordToken);
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
            user.CreatedDate = DateTime.UtcNow;
            user.IpAddress = this.httpContextHelpers.GetCurrentIpAddress();

            try
            {
                await this.userRepository.InsertAsync(user);

                await this.publisher.EntityInserted(user);
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

                await this.publisher.EntityUpdated(user);
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
            var user = await this.userRepository.Table
                .Include(c => c.Role)
                .Include(c => c.Location)
                .FirstOrDefaultAsync(c => c.Email.Equals(email) && !c.Deleted);

            if (user != null)
            {
                return StringHelpers.ToSha1(password, user.Salt).Equals(user.Password) ? user : null;
            }
            else
            {
                return null;
            }
        }
    }
}