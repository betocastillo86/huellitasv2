//-----------------------------------------------------------------------
// <copyright file="IBannerService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Huellitas.Data.Entities;
    

    /// <summary>
    /// Interface of banner service
    /// </summary>
    public interface IBannerService
    {
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="banner">The identifier.</param>
        /// <returns>the task</returns>
        Task Delete(Banner banner);

        /// <summary>
        /// Gets all banners
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="active">The active.</param>
        /// <param name="keyword">searches by name and body with the keyword</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="orderby">the order</param>
        /// <returns>the list of banners</returns>
        IPagedList<Banner> GetAll(
            BannerSection? section = null,
            bool? active = null,
            string keyword = null,
            int page = 0,
            int pageSize = int.MaxValue,
            OrderByBanner orderby = OrderByBanner.DisplayOrder);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeFile">if set to <c>true</c> [include file].</param>
        /// <returns>the banner</returns>
        Banner GetById(int id, bool includeFile = true);

        /// <summary>
        /// Inserts the specified banner.
        /// </summary>
        /// <param name="banner">The banner.</param>
        /// <returns>the task</returns>
        Task Insert(Banner banner);

        /// <summary>
        /// Updates the specified banner.
        /// </summary>
        /// <param name="banner">The banner.</param>
        /// <returns>the task</returns>
        Task Update(Banner banner);
    }
}