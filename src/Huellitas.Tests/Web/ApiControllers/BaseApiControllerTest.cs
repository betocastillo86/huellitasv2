//-----------------------------------------------------------------------
// <copyright file="BaseApiControllerTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers
{
    using System.Collections.Generic;
    using Business.Exceptions;
    using Huellitas.Web.Infraestructure.WebApi;
    using Microsoft.AspNetCore.Mvc;
    using Mocks;
    using NUnit.Framework;

    /// <summary>
    /// Base <c>Api</c> Controller Tests
    /// </summary>
    [TestFixture]
    public class BaseApiControllerTest
    {
        /// <summary>
        /// Tests the Bad the state of the request with model.
        /// </summary>
        [Test]
        public void BadRequestWithModelState()
        {
            var controller = new BaseApiController();
            controller.ModelState.AddModelError("Name", "El nombre no es valido");
            controller.ModelState.AddModelError("Description", "La descripción no es valida");

            var response = controller.BadRequest(controller.ModelState);

            var error = (response.Value as BaseApiError).Error;

            Assert.AreEqual("BadArgument", error.Code);
            Assert.AreEqual(2, error.Details.Count);
            Assert.AreEqual("Name", error.Details[0].Target);
            Assert.AreEqual("Description", error.Details[1].Target);
            Assert.AreEqual("BadArgument", error.Details[1].Code, error.Details[0].Code);
        }

        /// <summary>
        /// Tests  the bad request with exception.
        /// </summary>
        [Test]
        public void BadRequestWithNestedErrors()
        {
            var errorCode = HuellitasExceptionCode.BadArgument;

            var listErrors = new List<ApiError>();
            listErrors.Add(new ApiError() { Code = "Code1", Target = "Target1", Message = "Message1" });
            listErrors.Add(new ApiError() { Code = "Code2", Target = "Target2", Message = "Message2" });

            var mainTarget = "MainTarget";

            var controller = new BaseApiControllerMock();
            var response = controller.BadRequestWithCode(errorCode, listErrors, mainTarget) as ObjectResult;

            var error = (response.Value as BaseApiError).Error;

            Assert.AreEqual(400, response.StatusCode);
            Assert.AreEqual(errorCode.ToString(), error.Code);
            Assert.AreEqual(listErrors.Count, error.Details.Count);
            Assert.AreEqual(listErrors[0].Code, error.Details[0].Code);
            Assert.AreEqual(listErrors[0].Target, error.Details[0].Target);
            Assert.AreEqual(listErrors[1].Code, error.Details[1].Code);
            Assert.AreEqual(listErrors[1].Target, error.Details[1].Target);
        }

        /// <summary>
        /// Tests the Bad request with no nested errors zero.
        /// </summary>
        [Test]
        public void BadRequestWithNestedErrorsZero()
        {
            var errorCode = HuellitasExceptionCode.BadArgument;
            var listErrors = new List<ApiError>();
            var mainTarget = "MainTarget";

            var controller = new BaseApiControllerMock();
            var response = controller.BadRequestWithCode(errorCode, listErrors, mainTarget) as ObjectResult;

            var error = (response.Value as BaseApiError).Error;

            Assert.AreEqual(400, response.StatusCode);
            Assert.AreEqual(errorCode.ToString(), error.Code);
            Assert.AreEqual(mainTarget, error.Target);
        }
    }
}