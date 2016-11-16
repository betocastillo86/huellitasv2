//-----------------------------------------------------------------------
// <copyright file="CustomTableServiceExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Extensions.Services
{
    using System.Linq;
    using Huellitas.Business.Services.Common;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Custom table service extensions
    /// </summary>
    public static class CustomTableServiceExtensions
    {
        /// <summary>
        /// Gets the value of a custom table row by table and identifier.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="table">The table.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>the value column</returns>
        public static string GetValueByCustomTableAndId(this ICustomTableService service, CustomTableType table, int id)
        {
            ////TODO:Test
            var row = service.GetRowsByTableIdCached(table).FirstOrDefault(c => c.Id == id);
            if (row != null)
            {
                return row.Value;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}