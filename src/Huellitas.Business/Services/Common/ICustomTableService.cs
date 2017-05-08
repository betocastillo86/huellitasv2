//-----------------------------------------------------------------------
// <copyright file="ICustomTableService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System.Collections.Generic;
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
        /// <param name="orderBy">order by</param>
        /// <returns>the rows</returns>
        IList<CustomTableRow> GetRowsByTableId(int tableId, OrderByTableRow orderBy = OrderByTableRow.DisplayOrder);

        /// <summary>
        /// Gets the rows cached by table identifier.
        /// </summary>
        /// <param name="tableId">The table identifier.</param>
        /// <param name="orderBy">order by</param>
        /// <returns>the rows</returns>
        IList<CustomTableRow> GetRowsByTableIdCached(CustomTableType tableId, OrderByTableRow orderBy = OrderByTableRow.DisplayOrder);
    }
}
