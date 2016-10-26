//-----------------------------------------------------------------------
// <copyright file="CustomTableService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Custom table service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.Common.ICustomTableService" />
    public class CustomTableService : ICustomTableService
    {
        /// <summary>
        /// The custom table row repository
        /// </summary>
        private readonly IRepository<CustomTableRow> customTableRowRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTableService"/> class.
        /// </summary>
        /// <param name="customTableRowRepository">The custom table row repository.</param>
        public CustomTableService(IRepository<CustomTableRow> customTableRowRepository)
        {
            this.customTableRowRepository = customTableRowRepository;
        }

        /// <summary>
        /// Gets the rows by table identifier.
        /// </summary>
        /// <param name="tableId">The table identifier.</param>
        /// <returns>
        /// the rows
        /// </returns>
        public IList<CustomTableRow> GetRowsByTableId(int tableId)
        {
            return this.customTableRowRepository.TableNoTracking.Where(c => c.CustomTableId == tableId).ToList();
        }
    }
}