//-----------------------------------------------------------------------
// <copyright file="BaseApiControllerMock.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.Mocks
{
    using System.Collections.Generic;
    using Huellitas.Business.Exceptions;
    using Huellitas.Web.Infraestructure.WebApi;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Base <c>Api</c> Controller Mock
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    public class BaseApiControllerMock : BaseApiController
    {
        /// <summary>
        /// Method for testing Bad request with code
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="errors">The errors.</param>
        /// <param name="target">The target.</param>
        /// <returns>action result</returns>
        public IActionResult BadRequestWithCode(HuellitasExceptionCode code, IList<ApiError> errors, string target = null)
        {
            return this.BadRequest(code, errors, target);
        }

        /// <summary>
        /// Oks the header.
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="hasNextPage">if set to <c>true</c> [has next page].</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns>Test result</returns>
        public IActionResult OkHeader<T>(IList<T> list, bool hasNextPage, int totalCount)
        {
            return this.Ok(list, hasNextPage, totalCount);
        }
    }
}
