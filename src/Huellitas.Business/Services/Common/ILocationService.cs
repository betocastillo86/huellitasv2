//-----------------------------------------------------------------------
// <copyright file="ILocationService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Common
{
    using Huellitas.Data.Entities;
    using Huellitas.Data.Infraestructure;

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
    }
}