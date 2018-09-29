//-----------------------------------------------------------------------
// <copyright file="UserExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Extensions
{
    using Huellitas.Data.Entities;

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
            if (user == null)
            {
                return false;
            }

            return user.RoleEnum.Equals(RoleEnum.SuperAdmin);
        }

        /// <summary>
        /// Determines whether [is super admin].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <c>true</c> if [is super admin] [the specified user]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSuperAdmin(this User user)
        {
            if (user == null)
            {
                return false;
            }

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
            if (user == null)
            {
                return false;
            }

            return user.RoleEnum.Equals(RoleEnum.SuperAdmin);
        }

        /// <summary>
        /// Determines whether this instance [can edit any user] the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can edit any user] the specified user; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanEditAnyUser(this User user)
        {
            if (user == null)
            {
                return false;
            }

            return user.RoleEnum.Equals(RoleEnum.SuperAdmin);
        }

        /// <summary>
        /// Determines whether this instance [can see sensitive user information] the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can see sensitive user information] the specified user; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanSeeSensitiveUserInfo(this User user)
        {
            if (user == null)
            {
                return false;
            }

            return user.RoleEnum.Equals(RoleEnum.SuperAdmin);
        }

        /// <summary>
        /// Gets the cellphone number by user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>the phone</returns>
        public static string GetCellphoneNumber(this User user)
        {
            if (user.PhoneNumber?.Trim().Length == 10 && user.PhoneNumber.Trim().StartsWith("3"))
            {
                return user.PhoneNumber.Trim();
            }

            if (user.PhoneNumber2?.Trim().Length == 10 && user.PhoneNumber2.Trim().StartsWith("3"))
            {
                return user.PhoneNumber2.Trim();
            }

            return null;
        }
    }
}