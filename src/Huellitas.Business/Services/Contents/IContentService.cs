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
        /// Gets the users by content identifier.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="relation">The relation.</param>
        /// <returns>the list of users</returns>
        IList<ContentUser> GetUsersByContentId(int contentId, ContentUserRelationType? relation = null);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>the task</returns>
        Task InsertAsync(Content content);

        /// <summary>
        /// Determines whether [is user in content] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="relation">The relation.</param>
        /// <returns>
        ///   <c>true</c> if [is user in content] [the specified user identifier]; otherwise, <c>false</c>.
        /// </returns>
        bool IsUserInContent(int userId, int contentId, ContentUserRelationType? relation = null);

        /// <summary>
        /// Searches the specified keyword.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="attributesFilter">The attributes filter.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="page">The page.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="status">The status.</param>
        /// <returns>the list</returns>
        IPagedList<Content> Search(
            string keyword = null,
            ContentType? contentType = null,
            IList<FilterAttribute> attributesFilter = null,
            int pageSize = int.MaxValue,
            int page = 0,
            ContentOrderBy orderBy = ContentOrderBy.DisplayOrder,
            int? locationId = null,
            StatusType? status = null);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>the task</returns>
        Task UpdateAsync(Content content);

        /// <summary>
        /// Gets the content attribute.
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns>the value</returns>
        T GetContentAttribute<T>(int contentId, ContentAttributeType attribute);
    }
}