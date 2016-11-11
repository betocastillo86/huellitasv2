﻿//-----------------------------------------------------------------------
// <copyright file="BaseApiController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.WebApi
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Data.Entities;
    using Huellitas.Business.Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// Base for <c>Api</c> Controllers
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class BaseApiController : Controller
    {
        /// <summary>
        /// The current user
        /// </summary>
        private User currentUser;

        /// <summary>
        /// The current user identifier
        /// </summary>
        private int currentUserId;

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public User CurrentUser
        {
            get
            {
                if (this.User.Identity.IsAuthenticated)
                {
                    if (this.currentUser == null)
                    {
                        ////TODO: Search in IUserService
                        this.currentUser = new Data.Entities.User() { Id = this.CurrentUserId, Name = "Gabriel Castillo", Email = "gabriel.castillo86@hotmail.com" };
                    }
                }

                return this.currentUser;
            }
        }

        /// <summary>
        /// Gets the current user identifier.
        /// </summary>
        /// <value>
        /// The current user identifier.
        /// </value>
        public int CurrentUserId
        {
            get
            {
                if (this.User.Identity.IsAuthenticated)
                {
                    this.currentUserId = int.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
                }

                return this.currentUserId;
            }
        }

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
            error.Message = ExceptionMessages.GetMessage(HuellitasExceptionCode.BadArgument);

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
                Message = ExceptionMessages.GetMessage(code),
                Target = target,
                Details = errors == null || errors.Count == 0 ? null : errors
            };

            return this.StatusCode(400, new BaseApiError() { Error = error });
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