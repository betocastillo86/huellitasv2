//-----------------------------------------------------------------------
// <copyright file="LocationService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Common
{
    using System;
    using System.Linq;
    using Data.Core;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Infraestructure;

    /// <summary>
    /// Location Service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.Common.ILocationService" />
    public class LocationService : ILocationService
    {
        /// <summary>
        /// The location repository
        /// </summary>
        private readonly IRepository<Location> locationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationService"/> class.
        /// </summary>
        /// <param name="locationRepository">The location repository.</param>
        public LocationService(IRepository<Location> locationRepository)
        {
            this.locationRepository = locationRepository;
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
    }
}