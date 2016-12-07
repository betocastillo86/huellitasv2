//-----------------------------------------------------------------------
// <copyright file="IContentService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Contents
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities.Enums;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Infraestructure;

    /// <summary>
    /// Interface of Content Service
    /// </summary>
    public interface IContentService
    {
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeLocation">Includes the location in the query</param>
        /// <returns>the value</returns>
        Content GetById(int id, bool includeLocation = false);

        /// <summary>
        /// Gets the files of a content
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <returns>the files</returns>
        IList<ContentFile> GetFiles(int contentId);

        /// <summary>
        /// Gets the related contents by type optional
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="relation">The relation.</param>
        /// <param name="page">the page</param>
        /// <param name="pageSize">the page size</param>
        /// <returns>List of related contents by type</returns>
        IPagedList<Content> GetRelated(
            int id,
            RelationType? relation = null,
            int page = 0,
            int pageSize = int.MaxValue);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>the task</returns>
        Task InsertAsync(Content content);

        /// <summary>
        /// Searches the specified keyword.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="attributesFilter">The attributes filter.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="page">The page.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>the value</returns>
        IPagedList<Content> Search(
            string keyword = null,
            ContentType? contentType = null,
            IList<FilterAttribute> attributesFilter = null,
            int pageSize = int.MaxValue,
            int page = 0,
            ContentOrderBy orderBy = ContentOrderBy.DisplayOrder);
    }
}