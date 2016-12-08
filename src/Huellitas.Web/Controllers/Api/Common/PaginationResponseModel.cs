//-----------------------------------------------------------------------
// <copyright file="PaginationResponseModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.Common
{
    using System.Collections.Generic;

    /// <summary>
    /// Information about a request with pagination
    /// </summary>
    /// <typeparam name="T">A class</typeparam>
    public class PaginationResponseModel<T> where T : class
    {
        /// <summary>
        /// Gets or sets the meta.
        /// </summary>
        /// <value>
        /// The meta.
        /// </value>
        public PaginationInformationModel Meta { get; set; }

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>
        /// The results.
        /// </value>
        public IList<T> Results { get; set; }
    }
}