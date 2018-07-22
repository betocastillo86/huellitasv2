//-----------------------------------------------------------------------
// <copyright file="CustomTableService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Beto.Core.Caching;
    using Beto.Core.Data;
    using Caching;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Infraestructure;

    /// <summary>
    /// Custom table service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.ICustomTableService" />
    public class CustomTableService : ICustomTableService
    {
        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager cacheManager;

        /// <summary>
        /// The custom table row repository
        /// </summary>
        private readonly IRepository<CustomTableRow> customTableRowRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTableService"/> class.
        /// </summary>
        /// <param name="customTableRowRepository">The custom table row repository.</param>
        /// <param name="cacheManager">Cache manager</param>
        public CustomTableService(
            IRepository<CustomTableRow> customTableRowRepository,
            ICacheManager cacheManager)
        {
            this.customTableRowRepository = customTableRowRepository;
            this.cacheManager = cacheManager;
        }

        /// <summary>
        /// Gets the rows by table identifier.
        /// </summary>
        /// <param name="tableId">The table identifier.</param>
        /// <returns>
        /// the rows
        /// </returns>
        public IPagedList<CustomTableRow> GetRowsByTableId(int tableId, string keyword = null, OrderByTableRow orderBy = OrderByTableRow.DisplayOrder, int page = 0, int pageSize = int.MaxValue)
        {
            var query = this.customTableRowRepository.Table.Where(c => c.CustomTableId == tableId);

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => c.Value.Contains(keyword));
            }

            switch (orderBy)
            {
                case OrderByTableRow.Value:
                    query = query.OrderBy(c => c.Id);
                    break;
                default:
                case OrderByTableRow.DisplayOrder:
                    query = query.OrderByDescending(c => c.DisplayOrder);
                    break;
            }

            return new PagedList<CustomTableRow>(query, page, pageSize);
        }

        /// <summary>
        /// Gets the rows cached by table identifier.
        /// </summary>
        /// <param name="tableId">The table identifier.</param>
        /// <param name="orderBy">order by</param>
        /// <returns>
        /// the rows
        /// </returns>
        public IList<CustomTableRow> GetRowsByTableIdCached(CustomTableType tableId, OrderByTableRow orderBy = OrderByTableRow.DisplayOrder)
        {
            var key = string.Format(CacheKeys.CUSTOMTABLEROWS_BY_TABLE, tableId);
            return this.cacheManager.Get(
                key, 
                () =>
            {
                return this.GetRowsByTableId(Convert.ToInt32(tableId));
            });
        }
    }
}