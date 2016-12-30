//-----------------------------------------------------------------------
// <copyright file="UserExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Extensions.Entities
{
    using Huellitas.Data.Entities;
    using Huellitas.Data.Entities.Enums;

    /// <summary>
    /// User Extensions
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// Determines whether this instance [can approve contents] the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can approve contents] the specified user; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanApproveContents(this User user)
        {
            ////TODO:Test
            return user.RoleEnum.Equals(RoleEnum.SuperAdmin);
        }

        /// <summary>
        /// Determines whether this instance [can edit any content] the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can edit any content] the specified user; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanEditAnyContent(this User user)
        {
            return user.RoleEnum.Equals(RoleEnum.SuperAdmin);
        }
    }
}