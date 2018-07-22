//-----------------------------------------------------------------------
// <copyright file="ILocationService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System.Collections.Generic;
    using Beto.Core.Data;
    using Huellitas.Data.Entities;
    

    /// <summary>
    /// Interface of location service
    /// </summary>
    public interface ILocationService
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="parentId">The parent identifier.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>the list of locations</returns>
        IPagedList<Location> GetAll(
            string name = null,
            int? parentId = null,
            int page = 0,
            int pageSize = int.MaxValue);

        /// <summary>
        /// Gets the cached locations.
        /// </summary>
        /// <returns>the locations</returns>
        IList<Location> GetCachedLocations();

        /// <summary>
        /// Gets the cached location by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the location</returns>
        Location GetCachedLocationById(int id);
    }
}