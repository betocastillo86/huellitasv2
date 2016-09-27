//-----------------------------------------------------------------------
// <copyright file="BaseApiController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.WebApi
{
    using System.Collections.Generic;
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Helpers;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Base for <c>Api</c> Controllers
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class BaseApiController : Controller
    {
        #region BadRequest

        /// <summary>
        /// Bad the request.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="message">The message.</param>
        /// <returns>the value</returns>
        protected IActionResult BadRequest(HuellitasException ex, string message = null)
        {
            var error = new ApiError();
            error.Code = ex.Code.ToString();
            error.Message = message ?? ex.Message;
            return this.StatusCode(400, new { Error = error });
        }

        /// <summary>
        /// Bad the request with nested errors
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="errors">The errors.</param>
        /// <param name="target">The target.</param>
        /// <returns>the value</returns>
        protected IActionResult BadRequest(HuellitasExceptionCode code, IList<ApiError> errors, string target = null)
        {
            object objResponse = null;

            if (errors == null || errors.Count == 0)
            {
                objResponse = new ApiError()
                {
                    Code = code.ToString(),
                    Message = ExceptionMessages.GetMessage(code),
                    Target = target
                };
            }
            else
            {
                objResponse = new ApiError()
                {
                    Code = code.ToString(),
                    Message = ExceptionMessages.GetMessage(code),
                    Details = errors,
                    Target = target
                };
            }

            return this.StatusCode(400, new { Error = objResponse });
        }

        #endregion BadRequest

        #region Ok

        /// <summary>
        /// Ok the specified paged list
        /// </summary>
        /// <typeparam name="T">The Entity</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="hasNextPage">if set to <c>true</c> [has next page].</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns>the value</returns>
        protected IActionResult Ok<T>(IList<T> list, bool hasNextPage, int totalCount)
        {
            this.Response.Headers.Add(ApiHeadersList.PAGINATION_HASNEXTPAGE, hasNextPage.ToString());
            this.Response.Headers.Add(ApiHeadersList.PAGINATION_TOTALCOUNT, totalCount.ToString());
            this.Response.Headers.Add(ApiHeadersList.PAGINATION_COUNT, list.Count.ToString());

            return this.StatusCode(200, list);
        }

        #endregion Ok
    }
}