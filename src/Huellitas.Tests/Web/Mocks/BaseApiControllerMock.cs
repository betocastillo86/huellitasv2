//-----------------------------------------------------------------------
// <copyright file="BaseApiControllerMock.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.Mocks
{
    using System.Collections.Generic;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api;
    using Beto.Core.Web.Api.Controllers;
    using Huellitas.Business.Exceptions;
    using Huellitas.Web.Infraestructure.WebApi;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Base <c>Api</c> Controller Mock
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    public class BaseApiControllerMock : BaseApiController
    {
        public BaseApiControllerMock(IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
        }

        /// <summary>
        /// Method for testing Bad request with code
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="errors">The errors.</param>
        /// <param name="target">The target.</param>
        /// <returns>action result</returns>
        public IActionResult BadRequestWithCode(HuellitasExceptionCode code, IList<ApiErrorModel> errors, string target = null)
        {
            return this.BadRequest(new HuellitasException(code), errors, target);
        }
    }
}
