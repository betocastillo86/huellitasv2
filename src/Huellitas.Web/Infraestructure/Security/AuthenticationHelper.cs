//-----------------------------------------------------------------------
// <copyright file="AuthenticationHelper.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Security
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Security.Principal;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Authentication Helper
    /// </summary>
    public static class AuthenticationHelper
    {
        /// <summary>
        /// Gets the identity.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="claims">The claims.</param>
        /// <returns>the identity</returns>
        public static GenericIdentity GetIdentity(User user, out IList<Claim> claims)
        {
            var genericIdentity = new GenericIdentity(user.Id.ToString(), "Token");
            claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            claims.Add(new Claim(ClaimTypes.Role, user.RoleEnum.ToString()));
            return genericIdentity;
        }
    }
}