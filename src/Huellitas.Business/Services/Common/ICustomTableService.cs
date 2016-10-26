//-----------------------------------------------------------------------
// <copyright file="ICustomTableService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Common
{
    using Huellitas.Data.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Interface of Custom Table Service
    /// </summary>
    public interface ICustomTableService
    {
        /// <summary>
        /// Gets the rows by table identifier.
        /// </summary>
        /// <param name="tableId">The table identifier.</param>
        /// <returns>the rows</returns>
        IList<CustomTableRow> GetRowsByTableId(int tableId);
    }
}
