//-----------------------------------------------------------------------
// <copyright file="IContentService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Data.Entities;

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
        /// Get by friendly name
        /// </summary>
        /// <param name="friendlyName">friendly name</param>
        /// <param name="includeLocation">includes location</param>
        /// <returns>the content</returns>
        Content GetByFriendlyName(string friendlyName, bool includeLocation = false);

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
        /// <param name="includeUser">include user</param>
        /// <param name="page">the page</param>
        /// <param name="pageSize">the page size</param>
        /// <returns>the list of users</returns>
        IPagedList<ContentUser> GetUsersByContentId(int contentId, ContentUserRelationType? relation = null, bool includeUser = false, int page = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Gets the contents by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="relation">The relation.</param>
        /// <param name="includeContent">if set to <c>true</c> [include content].</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>the contents</returns>
        IPagedList<ContentUser> GetContentsByUserId(int userId, ContentUserRelationType? relation = null, bool includeContent = false, int page = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>the task</returns>
        Task InsertAsync(Content content);

        /// <summary>
        /// Gets the content user by user identifier and content identifier.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>the content user relationship</returns>
        ContentUser GetContentUserById(int contentId, int userId);

        /// <summary>
        /// Deletes the content user.
        /// </summary>
        /// <param name="contentUser">The content user.</param>
        /// <returns>the task</returns>
        Task DeleteContentUser(ContentUser contentUser);

        /// <summary>
        /// Inserts the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>the task</returns>
        Task InsertUser(ContentUser user);

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
        /// <param name="closingDateFrom">The closing date from.</param>
        /// <param name="closingDateTo">The closing date to.</param>
        /// <param name="startingDateFrom">The starting date from.</param>
        /// <param name="startingDateTo">The starting date to.</param>
        /// <param name="belongsToUserId">The belongs to user identifier.</param>
        /// <param name="excludeContentId">The exclude content identifier.</param>
        /// <param name="onlyFeatured">The only featured.</param>
        /// <param name="onlyRescuers">only with rescuers</param>
        /// <returns>the list</returns>
        IPagedList<Content> Search(
            string keyword = null,
            ContentType? contentType = null,
            IList<FilterAttribute> attributesFilter = null,
            int pageSize = int.MaxValue,
            int page = 0,
            ContentOrderBy orderBy = ContentOrderBy.DisplayOrder,
            int? locationId = null,
            StatusType? status = null,
            DateTime? closingDateFrom = null,
            DateTime? closingDateTo = null,
            DateTime? startingDateFrom = null,
            DateTime? startingDateTo = null,
            int? belongsToUserId = null,
            int? excludeContentId = null,
            bool? onlyFeatured = null,
            bool? onlyRescuers = null);

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

        /// <summary>
        /// Sorts the files.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="fileIdFrom">The file identifier from.</param>
        /// <param name="fileIdTo">The file identifier to.</param>
        /// <returns>the task</returns>
        Task SortFiles(int contentId, int fileIdFrom, int fileIdTo);
    }
}