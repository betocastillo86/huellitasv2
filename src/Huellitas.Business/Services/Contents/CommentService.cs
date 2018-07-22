//-----------------------------------------------------------------------
// <copyright file="CommentService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using Beto.Core.Data;
    using Huellitas.Business.Configuration;
    using Beto.Core.EventPublisher;
    using Huellitas.Business.Exceptions;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Infraestructure;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Helpers;

    public class CommentService : ICommentService
    {
        /// <summary>
        /// The comment repository
        /// </summary>
        private readonly IRepository<Comment> commentRepository;

        /// <summary>
        /// The content repository
        /// </summary>
        private readonly IRepository<Content> contentRepository;

        /// <summary>
        /// The general settings
        /// </summary>
        private readonly IGeneralSettings generalSettings;

        /// <summary>
        /// The HTTP context helpers
        /// </summary>
        private readonly IHttpContextHelper httpContextHelpers;

        /// <summary>
        /// The publisher
        /// </summary>
        private readonly IPublisher publisher;

        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IRepository<User> userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentService"/> class.
        /// </summary>
        /// <param name="commentRepository">The comment repository.</param>
        /// <param name="contentRepository">The content repository.</param>
        /// <param name="generalSettings">The general settings.</param>
        /// <param name="httpContextHelpers">The HTTP context helpers.</param>
        /// <param name="publisher">The publisher.</param>
        /// <param name="userRepository">The user repository.</param>
        public CommentService(
            IRepository<Comment> commentRepository,
            IRepository<Content> contentRepository,
            IGeneralSettings generalSettings,
            IHttpContextHelper httpContextHelpers,
            IPublisher publisher,
            IRepository<User> userRepository)
        {
            this.commentRepository = commentRepository;
            this.contentRepository = contentRepository;
            this.generalSettings = generalSettings;
            this.httpContextHelpers = httpContextHelpers;
            this.publisher = publisher;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Deletes the specified comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task Delete(Comment comment)
        {
            comment.Deleted = true;
            comment.ModifiedDate = DateTime.Now;

            if (comment.ParentCommentId.HasValue)
            {
                comment.ParentComment.NumComments = this.commentRepository.Table.Count(c => c.ParentCommentId == comment.ParentCommentId && !c.Deleted) - 1;
            }

            await this.commentRepository.UpdateAsync(comment);

            await this.publisher.EntityDeleted(comment);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="getUser">if set to <c>true</c> [get user].</param>
        /// <param name="getParent">if set to <c>true</c> [get parent].</param>
        /// <param name="getContent">if set to <c>true</c> [get content].</param>
        /// <returns>
        /// the comment
        /// </returns>
        public Comment GetById(int id, bool getUser = true, bool getParent = true, bool getContent = false)
        {
            var query = this.commentRepository.Table;

            if (getUser)
            {
                query = query.Include(c => c.User);
            }

            if (getContent)
            {
                query = query.Include(c => c.Content);
            }

            if (getParent)
            {
                query = query.Include(c => c.ParentComment);
            }

            return query.FirstOrDefault(c => c.Id == id && !c.Deleted);
        }

        /// <summary>
        /// Inserts the specified comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task Insert(Comment comment)
        {
            comment.CreationDate = DateTime.Now;

            comment.IpAddress = this.httpContextHelpers.GetCurrentIpAddress();

            if (comment.ParentCommentId.HasValue)
            {
                var parentComment = this.commentRepository.Table.FirstOrDefault(c => c.Id == comment.ParentCommentId);
                if (parentComment != null)
                {
                    parentComment.NumComments = this.commentRepository.Table.Count(c => c.ParentCommentId == parentComment.Id) + 1;
                }
                else
                {
                    throw new HuellitasException("ParentComment", HuellitasExceptionCode.InvalidForeignKey);
                }
            }

            try
            {
                ////Se guarda y se actualizan los comentarios padre
                await this.commentRepository.InsertAsync(comment);
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException is System.Data.SqlClient.SqlException)
                {
                    var inner = (System.Data.SqlClient.SqlException)e.InnerException;

                    if (inner.Number == 547)
                    {
                        string target = string.Empty;

                        if (inner.Message.IndexOf("FK_Comment_User") != -1)
                        {
                            target = "User";
                        }
                        else if (inner.Message.IndexOf("FK_Comment_ParentComment") != -1)
                        {
                            target = "ParentComment";
                        }
                        else if (inner.Message.IndexOf("FK_Comment_Content") != -1)
                        {
                            target = "Content";
                        }
                        else
                        {
                            throw;
                        }

                        throw new HuellitasException(target, HuellitasExceptionCode.InvalidForeignKey);
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ////Notifica el evento de inserción del comentario
            await this.publisher.EntityInserted(comment);
        }

        /// <summary>
        /// Searches the specified key.
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="parentCommentId">The parent comment identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// the comments
        /// </returns>
        public IPagedList<Comment> Search(
            string keyword = null,
            OrderByComment orderBy = OrderByComment.Recent,
            int? parentCommentId = default(int?),
            int? userId = null,
            int? contentId = null,
            int page = 0,
            int pageSize = int.MaxValue)
        {
            var query = this.commentRepository.Table
                .Include(c => c.User)
                .Include(c => c.ParentComment)
                .Where(c => !c.Deleted);

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => c.Value.Contains(keyword));
            }

            ////filters by parent comment
            if (parentCommentId.HasValue)
            {
                query = query.Where(c => c.ParentCommentId == parentCommentId.Value);
            }

            if (userId.HasValue)
            {
                query = query.Where(c => c.UserId == userId.Value);
            }

            if (contentId.HasValue)
            {
                query = query.Where(c => c.ContentId == contentId.Value);
            }

            switch (orderBy)
            {
                case OrderByComment.Recent:
                default:
                    query = query.OrderByDescending(c => c.CreationDate);
                    break;

                case OrderByComment.MostCommented:
                    query = query.OrderByDescending(c => c.NumComments);
                    break;
            }

            return new PagedList<Comment>(query, page, pageSize);
        }

        /// <summary>
        /// Updates the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>the task</returns>
        public async Task Update(Comment comment)
        {
            comment.ModifiedDate = DateTime.Now;

            try
            {
                await this.commentRepository.UpdateAsync(comment);
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException is System.Data.SqlClient.SqlException)
                {
                    var inner = (System.Data.SqlClient.SqlException)e.InnerException;

                    if (inner.Number == 547)
                    {
                        string target = string.Empty;

                        if (inner.Message.IndexOf("FK_Comment_User") != -1)
                        {
                            target = "User";
                        }
                        else if (inner.Message.IndexOf("FK_Comment_ParentComment") != -1)
                        {
                            target = "ParentComment";
                        }
                        else if (inner.Message.IndexOf("FK_Comment_Content") != -1)
                        {
                            target = "Content";
                        }
                        else
                        {
                            throw;
                        }

                        throw new HuellitasException(target, HuellitasExceptionCode.InvalidForeignKey);
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            await this.publisher.EntityUpdated(comment);
        }
    }
}