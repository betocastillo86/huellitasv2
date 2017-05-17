//-----------------------------------------------------------------------
// <copyright file="UserMappingExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.Entities.Enums;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api;

    /// <summary>
    /// User Mapping Extensions
    /// </summary>
    public static class UserMappingExtensions
    {
        /// <summary>
        /// To the base user model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the model</returns>
        public static BaseUserModel ToBaseUserModel(this User entity)
        {
            return new BaseUserModel
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        /// <summary>
        /// To the base user models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>the list</returns>
        public static IList<BaseUserModel> ToBaseUserModels(this IEnumerable<User> entities)
        {
            return entities.Select(ToBaseUserModel).ToList();
        }

        /// <summary>
        /// To the entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the entity</returns>
        public static User ToEntity(this UserModel model)
        {
            return new User
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                RoleEnum = model.Role.HasValue ? model.Role.Value : RoleEnum.Public,
                PhoneNumber = model.Phone,
                PhoneNumber2 = model.Phone2
            };
        }

        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="canSeeSensitiveInfo">can see sensitive info</param>
        /// <returns>the model</returns>
        public static UserModel ToModel(this User entity, bool canSeeSensitiveInfo)
        {
            return new UserModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Phone = entity.PhoneNumber,
                Phone2 = entity.PhoneNumber2,
                Role = canSeeSensitiveInfo ? entity.RoleEnum : (RoleEnum?)null,
                Location = entity.Location != null ? entity.Location.ToModel() : null,
                CreatedDate = entity.CreatedDate
            };
        }

        /// <summary>
        /// To the base user models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="canSeeSensitiveInfo">can see sensitive info</param>
        /// <returns>the list</returns>
        public static IList<UserModel> ToModels(this IEnumerable<User> entities, bool canSeeSensitiveInfo)
        {
            return entities.Select(c => c.ToModel(canSeeSensitiveInfo)).ToList();
        }
    }
}