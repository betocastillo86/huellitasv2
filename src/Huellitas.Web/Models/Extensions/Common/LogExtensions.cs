//-----------------------------------------------------------------------
// <copyright file="LogExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api;

    /// <summary>
    /// Log Extensions
    /// </summary>
    public static class LogExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the model</returns>
        public static LogModel ToModel(this Log entity)
        {
            return new LogModel
            {
                CreationDate = entity.CreationDate,
                FullMessage = entity.FullMessage,
                Id = entity.Id,
                IpAddress = entity.IpAddress,
                PageUrl = entity.PageUrl,
                ShortMessage = entity.ShortMessage,
                UserModel = entity.User != null ? entity.User.ToBaseUserModel() : null
            };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>the list</returns>
        public static IList<LogModel> ToModels(this IEnumerable<Log> entities)
        {
            return entities.Select(ToModel).ToList();
        }
    }
}