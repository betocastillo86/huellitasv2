//-----------------------------------------------------------------------
// <copyright file="BaseApiController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.WebApi
{
    using System.Collections.Generic;
    using Controllers.Api;
    using Huellitas.Business.Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// Base for <c>Api</c> Controllers
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class BaseApiController : Controller
    {
        #region BadRequest

        /// <summary>
        /// Creates an <see cref="T:Microsoft.AspNetCore.Mvc.BadRequestObjectResult" /> that produces a Bad Request (400) response.
        /// </summary>
        /// <param name="modelState">the model state</param>
        /// <returns>
        /// The created <see cref="T:Microsoft.AspNetCore.Mvc.BadRequestObjectResult" /> for the response.
        /// </returns>
        public override BadRequestObjectResult BadRequest(ModelStateDictionary modelState)
        {
            var error = new ApiError();
            error.Code = HuellitasExceptionCode.BadArgument.ToString();
            error.Message = MessageExceptionFinder.GetErrorMessage(HuellitasExceptionCode.BadArgument);

            foreach (var key in modelState.Keys)
            {
                var errorState = modelState[key];

                foreach (var detailError in errorState.Errors)
                {
                    error.Details.Add(new ApiError()
                    {
                        Code = HuellitasExceptionCode.BadArgument.ToString(),
                        Message = detailError.ErrorMessage,
                        Target = key
                    });
                }
            }

            return base.BadRequest(new BaseApiError() { Error = error });
        }

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
            error.Target = ex.Target;
            return this.StatusCode(400, new BaseApiError() { Error = error });
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
            var error = new ApiError()
            {
                Code = code.ToString(),
                Message = MessageExceptionFinder.GetErrorMessage(code),
                Target = target,
                Details = errors == null || errors.Count == 0 ? null : errors
            };

            return this.StatusCode(400, new BaseApiError() { Error = error });
        }

        /// <summary>
        /// Sends a Bad the request.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="error">The error.</param>
        /// <returns>the action</returns>
        protected IActionResult BadRequest(HuellitasExceptionCode code, string error)
        {
            var apiError = new ApiError()
            {
                Code = code.ToString(),
                Message = MessageExceptionFinder.GetErrorMessage(code),
                Target = null
            };

            return this.StatusCode(400, new BaseApiError() { Error = apiError });
        }

        /// <summary>
        /// Sends a Bad request.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <param name="target">The target.</param>
        /// <returns>The action</returns>
        protected IActionResult BadRequest(IList<ApiError> errors, string target = null)
        {
            return this.BadRequest(HuellitasExceptionCode.BadArgument, errors, target);
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
        protected IActionResult Ok<T>(IList<T> list, bool hasNextPage, int totalCount) where T : class
        {
            var model = new PaginationResponseModel<T>()
            {
                Meta = new PaginationInformationModel { Count = list.Count, HasNextPage = hasNextPage, TotalCount = totalCount },
                Results = list
            };

            return this.StatusCode(200, model);
        }

        #endregion Ok
    }
}