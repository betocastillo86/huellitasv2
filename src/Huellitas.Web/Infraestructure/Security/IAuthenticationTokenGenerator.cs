//-----------------------------------------------------------------------
// <copyright file="IAuthenticationTokenGenerator.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Security
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Security.Principal;

    /// <summary>
    /// Interface for generating a token for authentication
    /// </summary>
    public interface IAuthenticationTokenGenerator
    {
        /// <summary>
        /// Generates the token for authenticate a user
        /// </summary>
        /// <param name="genericIdentity">The generic identity.</param>
        /// <param name="claims">The claims.</param>
        /// <returns>The generated token for authentication</returns>
        GeneratedAuthenticationToken GenerateToken(GenericIdentity genericIdentity, IList<Claim> claims);
    }
}