//-----------------------------------------------------------------------
// <copyright file="CommentExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Extensions
{
    using System;
    using System.Collections.Generic;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Infraestructure.Security;
    using Huellitas.Web.Models.Api;

    /// <summary>
    /// Comment Extensions
    /// </summary>
    public static class CommentExtensions
    {
        /// <summary>
        /// Converts the model to the entity .
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the entity</returns>
        public static Comment ToEntity(this CommentModel model)
        {
            var entity = new Comment()
            {
                Id = model.Id,
                Value = model.Value.TrimEnd().ToXXSFilteredString(),
                ParentCommentId = model.ParentCommentId,
                ContentId = model.ContentId,
                UserId = model.User != null ? model.User.Id : 0
            };

            return entity;
        }

        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="currentUser">The current user.</param>
        /// <param name="commentService">The comment service.</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="loadFirstComments">if set to <c>true</c> [load first comments].</param>
        /// <returns>the model</returns>
        public static CommentModel ToModel(
            this Comment entity,
            User currentUser,
            ICommentService commentService,
            Func<string, string> contentUrlFunction = null,
            bool loadFirstComments = false)
        {
            var model = new CommentModel()
            {
                Id = entity.Id,
                Value = entity.Value,
                CountComments = entity.NumComments,
                CreationDate = entity.CreationDate,
                User = entity.User.ToBaseUserModel(),
                ParentCommentId = entity.ParentCommentId,
                CanDelete = entity.CanUserDeleteComment(currentUser)
            };

            ////Carga los dos primeros comentarios asociados a ese comentario
            if (loadFirstComments && entity.NumComments > 0)
            {
                ////var commentService = new CommentService();
                model.FirstComments = commentService.Search(parentCommentId: entity.Id, pageSize: 2)
                    .ToModels(currentUser, commentService, contentUrlFunction);
            }

            return model;
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="currentUser">The current user.</param>
        /// <param name="commentService">The comment service.</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="loadFirstComments">if set to <c>true</c> [load first comments].</param>
        /// <returns>the return</returns>
        public static IList<CommentModel> ToModels(
            this IList<Comment> entities,
            User currentUser,
            ICommentService commentService,
            Func<string, string> contentUrlFunction = null,
            bool loadFirstComments = false)
        {
            var models = new List<CommentModel>();
            foreach (var entity in entities)
            {
                models.Add(entity.ToModel(
                    currentUser,
                    commentService,
                    contentUrlFunction,
                    loadFirstComments));
            }

            return models;
        }
    }
}