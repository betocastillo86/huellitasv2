//-----------------------------------------------------------------------
// <copyright file="AuthenticatedUserModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api
{
    using Huellitas.Web.Models.Api;
    using Infraestructure.Security;

    /// <summary>
    /// Authenticated User Modal
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.BaseUserModel" />
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
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        public string Phone1 { get; set; }

        /// <summary>
        /// Gets or sets the second phone.
        /// </summary>
        /// <value>
        /// The phone2.
        /// </value>
        public string Phone2 { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public LocationModel Location { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public GeneratedAuthenticationToken Token { get; set; }
    }
}
