//-----------------------------------------------------------------------
// <copyright file="AuthenticatedUserModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api
{
    using Huellitas.Web.Models.Api.Users;
    using Infraestructure.Security;

    /// <summary>
    /// Authenticated User Modal
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.Users.BaseUserModel" />
    public class AuthenticatedUserModel : BaseUserModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public GeneratedAuthenticationToken Token { get; set; }
    }
}
