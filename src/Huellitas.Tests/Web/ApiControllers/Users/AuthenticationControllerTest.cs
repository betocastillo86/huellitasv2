//-----------------------------------------------------------------------
// <copyright file="AuthenticationControllerTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Users
{
    using Huellitas.Business.Security;
    using Huellitas.Business.Services.Users;
    using Huellitas.Web.Controllers.Api.Users;
    using Huellitas.Web.Infraestructure.Security;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api.Users;
    using Microsoft.AspNetCore.Mvc;
    using Mocks;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Authentication Controller Test
    /// </summary>
    [TestFixture]
    public class AuthenticationControllerTest
    {
        /// <summary>
        /// Mocks the authentication controller.
        /// </summary>
        /// <param name="authenticationTokenGenerator">The authentication token generator.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="securityHelpers">The security helpers.</param>
        /// <returns>the mock</returns>
        public AuthenticationController MockAuthenticationController(
            IAuthenticationTokenGenerator authenticationTokenGenerator = null,
            IUserService userService = null,
            ISecurityHelpers securityHelpers = null)
        {
            if (authenticationTokenGenerator == null)
            {
                authenticationTokenGenerator = new Mock<IAuthenticationTokenGenerator>().Object;
            }

            if (userService == null)
            {
                userService = new Mock<IUserService>().Object;
            }

            if (securityHelpers == null)
            {
                securityHelpers = new Mock<ISecurityHelpers>().Object;
            }

            return new AuthenticationController(authenticationTokenGenerator, userService, securityHelpers);
        }

        /// <summary>
        /// Posts the authentication invalid model null.
        /// </summary>
        [Test]
        public void PostAuthentication_InvalidModel_Null()
        {
            var controller = this.MockAuthenticationController();
            AuthenticationUserModel model = null;
            var response = controller.Post(model) as ObjectResult;
            var error = (response.Value as BaseApiError).Error;

            Assert.AreEqual(400, response.StatusCode);
            Assert.AreEqual("BadArgument", error.Code);
        }

        /// <summary>
        /// Posts the authentication invalid model bad password.
        /// </summary>
        [Test]
        public void PostAuthentication_InvalidModel_BadPassword()
        {
            var controller = this.MockAuthenticationController();
            AuthenticationUserModel model = new AuthenticationUserModel();
            model.Email = "aa@aa.com";

            var response = controller.Post(model) as ObjectResult;
            Assert.IsFalse(controller.IsValidModelState(model));
        }
    }
}