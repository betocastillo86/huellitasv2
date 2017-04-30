//-----------------------------------------------------------------------
// <copyright file="CommentExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Extensions
{
    using Huellitas.Data.Entities;

    /// <summary>
    /// Comment Extensions
    /// </summary>
    public static class CommentExtensions
    {
        /// <summary>
        /// Determines whether this instance [can user delete comment] the specified user.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can user delete comment] the specified user; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanUserDeleteComment(this Comment comment, User user)
        {
            if (user == null || comment == null)
            {
                return false;
            }

            if (comment.ParentCommentId.HasValue)
            {
                ////El usuario puede eliminar el cotenido si es administrador, si es el dueño del comentario padre (idea) o si es dueño del mismo comentario
                return user.IsSuperAdmin() || comment.UserId == user.Id;
            }
            else
            {
                ////El usuario puede eliminar si es admin o si es el dueño del comentario (idea)
                return user.IsSuperAdmin() || comment.UserId == user.Id;
            }
        }

        /// <summary>
        /// Determines whether this instance [can user edit comment] the specified user.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can user edit comment] the specified user; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanUserEditComment(this Comment comment, User user)
        {
            if (user == null || comment == null)
            {
                return false;
            }

            return user.IsSuperAdmin() || comment.UserId == user.Id;
        }
    }
}