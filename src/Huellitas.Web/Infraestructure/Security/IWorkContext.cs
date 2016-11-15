//-----------------------------------------------------------------------
// <copyright file="IWorkContext.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Security
{
    using Huellitas.Data.Entities;

    /// <summary>
    /// Interface that contains the work user information
    /// </summary>
    public interface IWorkContext
    {
        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        User CurrentUser { get; }

        /// <summary>
        /// Gets the current user identifier.
        /// </summary>
        /// <value>
        /// The current user identifier.
        /// </value>
        int CurrentUserId { get; }
    }
}