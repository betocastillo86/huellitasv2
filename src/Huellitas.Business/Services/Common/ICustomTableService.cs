//-----------------------------------------------------------------------
// <copyright file="ICustomTableService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System.Collections.Generic;
    using Beto.Core.Data;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Interface of Custom Table Service
    /// </summary>
    public interface ICustomTableService
    {
        /// <summary>
        /// Gets the rows by table identifier.
        /// </summary>
        /// <param name="tableId">The table identifier.</param>
        /// <param name="keyword">The keyword.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>the list</returns>
        IPagedList<CustomTableRow> GetRowsByTableId(int tableId, string keyword = null, OrderByTableRow orderBy = OrderByTableRow.DisplayOrder, int page = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Gets the rows by table identifier cached.
        /// </summary>
        /// <param name="tableId">The table identifier.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>the list</returns>
        IList<CustomTableRow> GetRowsByTableIdCached(CustomTableType tableId, OrderByTableRow orderBy = OrderByTableRow.DisplayOrder);
    }
}