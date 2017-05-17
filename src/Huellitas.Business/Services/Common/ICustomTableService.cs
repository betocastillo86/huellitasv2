//-----------------------------------------------------------------------
// <copyright file="ICustomTableService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System.Collections.Generic;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Infraestructure;

    /// <summary>
    /// Interface of Custom Table Service
    /// </summary>
    public interface ICustomTableService
    {
        /// <summary>
        /// Gets the rows by table identifier.
        /// </summary>
        /// <param name="tableId">The table identifier.</param>
        /// <param name="orderBy">order by</param>
        /// <returns>the rows</returns>
        IPagedList<CustomTableRow> GetRowsByTableId(int tableId, string keyword = null, OrderByTableRow orderBy = OrderByTableRow.DisplayOrder, int page = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Gets the rows cached by table identifier.
        /// </summary>
        /// <param name="tableId">The table identifier.</param>
        /// <param name="orderBy">order by</param>
        /// <returns>the rows</returns>
        IList<CustomTableRow> GetRowsByTableIdCached(CustomTableType tableId, OrderByTableRow orderBy = OrderByTableRow.DisplayOrder);
    }
}
