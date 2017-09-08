//-----------------------------------------------------------------------
// <copyright file="LocationService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Caching;
    using Data.Core;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Infraestructure;

    /// <summary>
    /// Location Service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.ILocationService" />
    public class LocationService : ILocationService
    {
        /// <summary>
        /// The location repository
        /// </summary>
        private readonly IRepository<Location> locationRepository;

        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager cacheManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationService"/> class.
        /// </summary>
        /// <param name="locationRepository">The location repository.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public LocationService(
            IRepository<Location> locationRepository,
            ICacheManager cacheManager)
        {
            this.locationRepository = locationRepository;
            this.cacheManager = cacheManager;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="parentId">The parent identifier.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// the list of locations
        /// </returns>
        public IPagedList<Location> GetAll(string name = null, int? parentId = default(int?), int page = 0, int pageSize = int.MaxValue)
        {
            var query = this.locationRepository.Table.Where(c => !c.Deleted);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.Contains(name));
            }

            if (parentId.HasValue)
            {
                query = query.Where(c => c.ParentLocationId == parentId.Value);
            }

            return new PagedList<Location>(query, page, pageSize);
        }

        /// <summary>
        /// Gets the cached locations.
        /// </summary>
        /// <returns>
        /// the locations
        /// </returns>
        public IList<Location> GetCachedLocations()
        {
            return this.cacheManager.Get(
                CacheKeys.LOCATIONS_ALL, 
                () => 
                {
                return this.GetAll();
            });
        }

        /// <summary>
        /// Gets the cached location by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// the location
        /// </returns>
        public Location GetCachedLocationById(int id)
        {
            return this.GetCachedLocations().FirstOrDefault(c => c.Id == id);
        }
    }
}