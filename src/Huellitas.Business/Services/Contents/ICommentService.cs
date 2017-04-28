//-----------------------------------------------------------------------
// <copyright file="ICommentService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using Huellitas.Data.Entities;
    using Huellitas.Data.Infraestructure;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICommentService
    {
        /// <summary>
        /// Deletes the specified comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>the task</returns>
        Task Delete(Comment comment);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="getUser">if set to <c>true</c> [get user].</param>
        /// <param name="getParent">if set to <c>true</c> [get parent].</param>
        /// <param name="getContent">if set to <c>true</c> [get content].</param>
        /// <returns>the comment</returns>
        Comment GetById(int id, bool getUser = true, bool getParent = true, bool getContent = false);


        /// <summary>
        /// Inserts the specified comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>the task</returns>
        Task Insert(Comment comment);

        /// <summary>
        /// Searches the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="parentCommentId">The parent comment identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>the comments</returns>
        IPagedList<Comment> Search(
            string keyword = null,
            OrderByComment orderBy = OrderByComment.Recent,
            int? parentCommentId = default(int?),
            int? userId = null,
            int? contentId = null,
            int page = 0,
            int pageSize = int.MaxValue);

        /// <summary>
        /// Updates the specified comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>the task</returns>
        Task Update(Comment comment);
    }
}