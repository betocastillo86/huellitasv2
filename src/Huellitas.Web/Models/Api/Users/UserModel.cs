//-----------------------------------------------------------------------
// <copyright file="UserModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.Users
{
    /// <summary>
    /// User Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.Users.BaseUserModel" />
    public class UserModel : BaseUserModel
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
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the second phone.
        /// </summary>
        /// <value>
        /// The second phone.
        /// </value>
        public string Phone2 { get; set; }
    }
}