//-----------------------------------------------------------------------
// <copyright file="IRoleService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Interface of role service
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>the list of roles</returns>
        Task<IList<Role>> GetAll();
    }
}