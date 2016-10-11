//-----------------------------------------------------------------------
// <copyright file="BaseApiError.cs" company="Dasigno">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.WebApi
{
    /// <summary>
    /// Base Api Error
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