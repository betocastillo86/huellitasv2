//-----------------------------------------------------------------------
// <copyright file="AuthenticationControllerTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Users
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Data.Entities;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services;
    using Huellitas.Web.Controllers.Api;
    using Huellitas.Web.Infraestructure.Security;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api;
    using Microsoft.AspNetCore.Mvc;
    using Mocks;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Authentication Controller Test
    /// </summary>
    [TestFixture]
    public class AuthenticationControllerTest : BaseTest
    {
        /// <summary>
        /// The authentication token generator
        /// </summary>
        private Mock<IAuthenticationTokenGenerator> authenticationTokenGenerator;

        /// <summary>
        /// The string helpers
        /// </summary>
        private Mock<INotificationService> notificationService = new Mock<INotificationService>();

        /// <summary>
        /// The security helpers
        /// </summary>
        private Mock<ISecurityHelpers> securityHelpers;

        /// <summary>
        /// The user service
        /// </summary>
        private Mock<IUserService> userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationControllerTest"/> class.
        /// </summary>
        public AuthenticationControllerTest()
        {
            this.authenticationTokenGenerator = new Mock<IAuthenticationTokenGenerator>();
            this.userService = new Mock<IUserService>();
            this.securityHelpers = new Mock<ISecurityHelpers>();
            this.notificationService = new Mock<INotificationService>();
        }

        /// <summary>
        /// Gets the authentication ok.
        /// </summary>
        [Test]
        public void GetAuthentication_Ok()
        {
            this.Setup();
            this.notificationService.Setup(c => c.CountUnseenNotificationsByUserId(1))
                .Returns(5);

            var controller = this.GetController();

            var response = controller.Get() as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        /// <summary>
        /// Mocks the authentication controller.
        /// </summary>
        /// <returns>the mock</returns>
        public AuthenticationController GetController()
        {
            return new AuthenticationController(
                this.authenticationTokenGenerator.Object,
                this.userService.Object,
                this.workContext.Object,
                this.notificationService.Object);
        }

        /// <summary>
        /// Posts the authentication invalid model bad password.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostAuthentication_InvalidModel_BadPassword()
        {
            var controller = this.GetController();
            AuthenticationUserModel model = new AuthenticationUserModel();
            model.Email = "aa@aa.com";

            var response = await controller.Post(model) as ObjectResult;
            Assert.IsFalse(controller.IsValidModelState(model));
        }

        /// <summary>
        /// Posts the authentication invalid model null.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostAuthentication_InvalidModel_Null()
        {
            var controller = this.GetController();

            AuthenticationUserModel model = null;
            var response = await controller.Post(model) as ObjectResult;
            var error = (response.Value as BaseApiError).Error;

            Assert.AreEqual(400, response.StatusCode);
            Assert.AreEqual("BadArgument", error.Code);
        }

        /// <summary>
        /// Posts the authentication ok.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostAuthentication_Ok()
        {
            var token = new GeneratedAuthenticationToken { AccessToken = "abc", Expires = 30 };
            var model = new AuthenticationUserModel { Email = "aa@aa.com", Password = "123" };
            var userAuthenticated = new User { Id = 1, Name = "Name", Role = new Role() { Name = "Role", Id = 1 }, Email = model.Email };

            this.authenticationTokenGenerator
                .Setup(c => c.GenerateToken(It.IsAny<GenericIdentity>(), It.IsAny<IList<Claim>>(), It.IsAny<DateTimeOffset>()))
                .Returns(token);

            this.securityHelpers
                .Setup(c => c.ToSha1(It.IsAny<string>(), It.IsAny<string>()))
                .Returns("123");

            this.userService
                .Setup(c => c.ValidateAuthentication(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(userAuthenticated);

            var controller = this.GetController();

            var response = await controller.Post(model) as ObjectResult;
            var user = response.Value as AuthenticatedUserModel;

            Assert.AreEqual(userAuthenticated.Id, user.Id);
            Assert.AreEqual(token.AccessToken, user.Token.AccessToken);
            Assert.AreEqual(200, response.StatusCode);
        }

        /// <summary>
        /// Posts the authentication unauthorized wrong password.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostAuthentication_Unauthorized_WrongPassword()
        {
            var model = new AuthenticationUserModel { Email = "aa@aa.com", Password = "123" };

            this.securityHelpers
                .Setup(c => c.ToSha1(It.IsAny<string>(), It.IsAny<string>()))
                .Returns("123");

            this.userService
                .Setup(c => c.ValidateAuthentication(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var controller = this.GetController();

            var response = await controller.Post(model) as UnauthorizedResult;

            Assert.AreEqual(401, response.StatusCode);
        }
    }
}