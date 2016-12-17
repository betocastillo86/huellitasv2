using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Huellitas.Data.Entities;
using Huellitas.Data.Entities.Enums;

namespace Huellitas.Business.Extensions.Entities
{
    /// <summary>
    /// User Extensions
    /// </summary>
    public static class UserExtensions
    {
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
