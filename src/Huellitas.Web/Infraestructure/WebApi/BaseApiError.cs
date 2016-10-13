//-----------------------------------------------------------------------
// <copyright file="BaseApiError.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.WebApi
{
    /// <summary>
    /// Base <c>Api</c> Error
    /// </summary>
    public class BaseApiError
    {
        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public ApiError Error { get; set; }
    }
}