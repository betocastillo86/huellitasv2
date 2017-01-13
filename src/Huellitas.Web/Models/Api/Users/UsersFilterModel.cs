//-----------------------------------------------------------------------
// <copyright file="UsersFilterModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.Users
{
    /// <summary>
    /// Users Filter Model
    /// </summary>
    public class UsersFilterModel
    {
        /// <summary>
        /// Gets or sets the keyword.
        /// </summary>
        /// <value>
        /// The keyword.
        /// </value>
        public string Keyword { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [full object].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [full object]; otherwise, <c>false</c>.
        /// </value>
        public bool FullObject { get; set; }
    }
}