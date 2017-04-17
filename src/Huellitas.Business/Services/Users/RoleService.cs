//-----------------------------------------------------------------------
// <copyright file="RoleService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Core;
    using Huellitas.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Role Service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.IRoleService" />
    public class RoleService : IRoleService
    {
        /// <summary>
        /// The role repository
        /// </summary>
        private readonly IRepository<Role> roleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService"/> class.
        /// </summary>
        /// <param name="roleRepository">The role repository.</param>
        public RoleService(IRepository<Role> roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// the list of roles
        /// </returns>
        public async Task<IList<Role>> GetAll()
        {
            return await this.roleRepository.Table.ToListAsync();
        }
    }
}