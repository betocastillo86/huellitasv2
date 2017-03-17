//-----------------------------------------------------------------------
// <copyright file="UsersControllerTest.cs" company="Huellitas Sin Hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Users
{
    using System.Threading.Tasks;
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services.Users;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Entities.Enums;
    using Huellitas.Data.Infraestructure;
    using Huellitas.Tests.Web.Mocks;
    using Huellitas.Web.Controllers.Api.Common;
    using Huellitas.Web.Controllers.Api.Users;
    using Huellitas.Web.Infraestructure.Security;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api;
    using Huellitas.Web.Models.Api.Common;
    using Huellitas.Web.Models.Api.Users;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Users Controller Test
    /// </summary>
    /// <seealso cref="Huellitas.Tests.BaseTest" />
    [TestFixture]
    public class UsersControllerTest : BaseTest
    {
        /// <summary>
        /// The authentication token generator
        /// </summary>
        private Mock<IAuthenticationTokenGenerator> authenticationTokenGenerator = new Mock<IAuthenticationTokenGenerator>();

        /// <summary>
        /// The security helpers
        /// </summary>
        private Mock<ISecurityHelpers> securityHelpers = new Mock<ISecurityHelpers>();

        /// <summary>
        /// The user service
        /// </summary>
        private Mock<IUserService> userService = new Mock<IUserService>();

        /// <summary>
        /// Deletes the user forbid.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task DeleteUser_Forbid()
        {
            this.Setup();

            this.workContext.SetupGet(c => c.CurrentUser)
                .Returns(new User { RoleEnum = RoleEnum.Public });

            var id = 1;

            var controller = this.GetController();

            var response = await controller.Delete(id);

            Assert.IsAssignableFrom(typeof(ForbidResult), response);
        }

        /// <summary>
        /// Deletes the user not found.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task DeleteUser_NotFound()
        {
            this.Setup();

            var id = 1;

            this.userService.Setup(c => c.GetByIdAsync(id))
                .ReturnsAsync(null);

            var controller = this.GetController();

            var response = await controller.Delete(id) as NotFoundResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        /// <summary>
        /// Deletes the user ok.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task DeleteUser_Ok()
        {
            this.Setup();

            var id = 1;

            this.userService.Setup(c => c.GetByIdAsync(id))
                .ReturnsAsync(new User() { Id = id, Email = "email", Name = "name", Password = "123", RoleEnum = RoleEnum.Public });

            var controller = this.GetController();

            var response = await controller.Delete(id) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        /// <summary>
        /// Gets the user by identifier forbid different user.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task GetUserById_Forbid_DifferentUser()
        {
            this.Setup();

            this.workContext.SetupGet(c => c.CurrentUser)
                .Returns(new User() { Id = 2, Name = "public", RoleEnum = RoleEnum.Public });

            this.workContext.SetupGet(c => c.CurrentUserId)
                .Returns(2);

            var id = 1;

            this.userService.Setup(c => c.GetByIdAsync(id))
                .ReturnsAsync(new User() { Id = 3, Name = "public", RoleEnum = RoleEnum.Public });

            var controller = this.GetController();
            var response = await controller.Get(id);

            Assert.IsAssignableFrom(typeof(ForbidResult), response);
        }

        /// <summary>
        /// Gets the user by identifier not found.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task GetUserById_NotFound()
        {
            this.Setup();

            var id = 1;

            this.userService.Setup(c => c.GetByIdAsync(id))
                .ReturnsAsync(null);

            var controller = this.GetController();
            var response = await controller.Get(id) as NotFoundResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        /// <summary>
        /// Gets the user by identifier ok.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task GetUserById_Ok()
        {
            this.Setup();

            var id = 1;

            this.userService.Setup(c => c.GetByIdAsync(id))
                .ReturnsAsync(new User() { Id = id, Email = "email", Name = "name", Password = "123", RoleEnum = RoleEnum.Public });

            var controller = this.GetController();
            var response = await controller.Get(id) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
            Assert.IsAssignableFrom(typeof(UserModel), response.Value);
        }

        /// <summary>
        /// Gets the user by identifier ok different user.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task GetUserById_Ok_DifferentUser()
        {
            this.Setup();

            var id = 2;

            this.userService.Setup(c => c.GetByIdAsync(id))
                .ReturnsAsync(new User() { Id = 2, Name = "public", RoleEnum = RoleEnum.Public });

            var controller = this.GetController();
            var response = await controller.Get(id) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        /// <summary>
        /// Gets the users bad request.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task GetUsers_BadRequest()
        {
            this.Setup();

            this.workContext.SetupGet(c => c.CurrentUser)
                .Returns(new User() { Id = 1, Name = "public", RoleEnum = RoleEnum.Public });

            var filter = new UsersFilterModel();
            filter.Role = RoleEnum.SuperAdmin;

            var controller = this.GetController();

            var response = await controller.Get(filter) as ObjectResult;

            Assert.AreEqual(400, response.StatusCode);
        }

        /// <summary>
        /// Gets the users ok.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task GetUsers_Ok()
        {
            this.Setup();

            var filter = new UsersFilterModel();

            this.userService
                .Setup(c => c.GetAll(It.IsAny<string>(), It.IsAny<RoleEnum?>(), filter.Page, filter.PageSize))
                .ReturnsAsync(new PagedList<User>());

            var controller = this.GetController();

            var response = await controller.Get(filter) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
            Assert.IsAssignableFrom(typeof(PaginationResponseModel<UserModel>), response.Value);
        }

        /// <summary>
        /// Posts the user bad request existent email.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostUser_BadRequest_ExistentEmail()
        {
            this.Setup();

            var user = new User() { Id = 2 };

            this.userService.Setup(c => c.Insert(It.IsAny<User>()))
                .Throws(new HuellitasException(HuellitasExceptionCode.UserEmailAlreadyUsed));

            var controller = this.GetController();

            var model = this.GetModel();
            var response = await controller.Post(model) as ObjectResult;

            Assert.AreEqual(400, response.StatusCode);
            Assert.AreEqual(HuellitasExceptionCode.UserEmailAlreadyUsed.ToString(), (response.Value as BaseApiError).Error.Code);
        }

        /// <summary>
        /// Posts the user bad request model.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostUser_BadRequest_Model()
        {
            this.Setup();
            var controller = this.GetController();
            var model = this.GetModel();
            model.Password = null;

            var response = await controller.Post(model) as ObjectResult;

            Assert.AreEqual(400, response.StatusCode);
            Assert.IsNotNull(controller.ModelState["Password"]);
            Assert.IsNull(controller.ModelState["Email"]);
        }

        /// <summary>
        /// Posts the user ok not authenticated.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostUser_Ok_NotAuthenticated()
        {
            this.Setup();
            this.SetupNotAuthenticated();

            var user = new User() { Id = 2 };

            this.userService.Setup(c => c.Insert(It.IsAny<User>()))
                .Callback((User user1) => { user1.Id = user.Id; })
                .Returns(Task.FromResult(0));

            var controller = this.GetController();
            controller.AddUrl(true);
            controller.AddResponse();

            var model = this.GetModel();
            model.Role = RoleEnum.Public;
            var response = await controller.Post(model) as ObjectResult;

            Assert.AreEqual(201, response.StatusCode);
            Assert.AreEqual(user.Id, (response.Value as BaseModel).Id);
            Assert.IsAssignableFrom(typeof(AuthenticatedUserModel), response.Value);
        }

        /// <summary>
        /// Posts the user ok previous authenticated.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostUser_Ok_PreviousAuthenticated()
        {
            this.Setup();

            var user = new User() { Id = 2 };

            this.userService.Setup(c => c.Insert(It.IsAny<User>()))
                .Callback((User user1) => { user1.Id = user.Id; })
                .Returns(Task.FromResult(0));

            var controller = this.GetController();
            controller.AddUrl(true);
            controller.AddResponse();

            var model = this.GetModel();
            var response = await controller.Post(model) as ObjectResult;

            Assert.AreEqual(201, response.StatusCode);
            Assert.AreEqual(user.Id, (response.Value as BaseModel).Id);
            Assert.IsAssignableFrom(typeof(BaseModel), response.Value);
        }

        /// <summary>
        /// Puts the user bad request existent email.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PutUser_BadRequest_ExistentEmail()
        {
            var id = 2;
            this.Setup();

            this.userService.Setup(c => c.GetByIdAsync(id))
                .ReturnsAsync(new User { Id = id, Name = "Gabriel" });

            this.userService.Setup(c => c.Update(It.IsAny<User>()))
                .Throws(new HuellitasException(HuellitasExceptionCode.UserEmailAlreadyUsed));

            var controller = this.GetController();
            var model = this.GetModel();
            model.Id = id;
            model.Role = RoleEnum.Public;

            var response = await controller.Put(id, model) as ObjectResult;

            Assert.AreEqual(400, response.StatusCode);
            Assert.AreEqual(HuellitasExceptionCode.UserEmailAlreadyUsed.ToString(), (response.Value as BaseApiError).Error.Code);
        }

        /// <summary>
        /// Puts the user bad request model.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PutUser_BadRequest_Model()
        {
            this.Setup();

            var controller = this.GetController();
            var model = this.GetModel();
            model.Name = null;

            var response = await controller.Put(1, model) as ObjectResult;

            Assert.AreEqual(400, response.StatusCode);
            Assert.IsNotNull(controller.ModelState["Name"]);
        }

        /// <summary>
        /// Puts the user forbid different user.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PutUser_Forbid_DifferentUser()
        {
            int id = 2;
            this.Setup();
            this.SetupPublicUser(3);

            this.userService.Setup(c => c.GetByIdAsync(id))
                .ReturnsAsync(new User { Id = id, Name = "Gabriel" });

            var controller = this.GetController();
            var model = this.GetModel();
            model.Role = RoleEnum.Public;

            var response = await controller.Put(id, model);

            Assert.IsAssignableFrom(typeof(ForbidResult), response);
        }

        /// <summary>
        /// Puts the user not found.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PutUser_NotFound()
        {
            int id = 2;
            this.Setup();
            this.SetupPublicUser(3);

            var controller = this.GetController();
            var model = this.GetModel();
            model.Role = RoleEnum.Public;

            var response = await controller.Put(id, model) as NotFoundResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        /// <summary>
        /// Puts the user ok admin user.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PutUser_Ok_AdminUser()
        {
            var id = 2;
            this.Setup();

            this.userService.Setup(c => c.GetByIdAsync(id))
                .ReturnsAsync(new User { Id = id, Name = "Gabriel" });

            var controller = this.GetController();
            var model = this.GetModel();
            model.Id = id;
            model.Role = RoleEnum.Public;

            var response = await controller.Put(id, model) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        /// <summary>
        /// Puts the user ok same user.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PutUser_Ok_SameUser()
        {
            var id = 2;
            this.Setup();
            this.SetupPublicUser(id);

            this.userService.Setup(c => c.GetByIdAsync(id))
                .ReturnsAsync(new User { Id = id, Name = "Gabriel" });

            var controller = this.GetController();
            var model = this.GetModel();
            model.Id = id;
            model.Role = RoleEnum.Public;

            var response = await controller.Put(id, model) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        /// <summary>
        /// User the controller is valid model is new is admin false.
        /// </summary>
        [Test]
        public void UsersController_IsValidModel_IsNew_IsAdmin_False()
        {
            this.Setup();

            var model = this.GetModel();

            this.workContext.SetupGet(c => c.IsAuthenticated).Returns(true);

            var controller = this.GetController();
            model.Password = null;
            Assert.IsFalse(controller.IsValidModel(model, true, true));
            Assert.IsNotNull(controller.ModelState["Password"]);
            Assert.IsNull(controller.ModelState["Role"]);
        }

        /// <summary>
        /// User the controller is valid model is new is admin true.
        /// </summary>
        [Test]
        public void UsersController_IsValidModel_IsNew_IsAdmin_True()
        {
            this.Setup();

            var model = this.GetModel();

            this.workContext.SetupGet(c => c.IsAuthenticated).Returns(true);

            var controller = this.GetController();
            Assert.IsTrue(controller.IsValidModel(model, true, true));
        }

        /// <summary>
        /// User the controller is valid model is new no admin false.
        /// </summary>
        [Test]
        public void UsersController_IsValidModel_IsNew_NoAdmin_False()
        {
            this.Setup();

            var model = this.GetModel();

            this.workContext.SetupGet(c => c.IsAuthenticated).Returns(true);

            var controller = this.GetController();

            model.Role = RoleEnum.SuperAdmin;
            model.Password = null;
            model.Name = null;
            Assert.IsFalse(controller.IsValidModel(model, true, false));
            Assert.IsNotNull(controller.ModelState["Password"]);
            Assert.IsNotNull(controller.ModelState["Role"]);
            Assert.IsNotNull(controller.ModelState["Name"]);
        }

        /// <summary>
        /// User the controller is valid model is new no admin true.
        /// </summary>
        [Test]
        public void UsersController_IsValidModel_IsNew_NoAdmin_True()
        {
            this.Setup();
            this.SetupNotAuthenticated();

            var model = this.GetModel();

            var controller = this.GetController();
            model.Role = Data.Entities.Enums.RoleEnum.Public;
            Assert.IsTrue(controller.IsValidModel(model, true, false));

            model = this.GetModel();
            model.Role = null;
            Assert.IsTrue(controller.IsValidModel(model, true, false));
            Assert.AreEqual(RoleEnum.Public, model.Role);
        }

        /// <summary>
        /// User the controller is valid model not new is admin false.
        /// </summary>
        [Test]
        public void UsersController_IsValidModel_NotNew_IsAdmin_False()
        {
            this.Setup();

            var model = this.GetModel();

            this.workContext.SetupGet(c => c.IsAuthenticated).Returns(true);

            var controller = this.GetController();
            model.Name = null;
            Assert.IsFalse(controller.IsValidModel(model, false, true));
            Assert.IsNotNull(controller.ModelState["Name"]);
            Assert.IsNull(controller.ModelState["Role"]);
            Assert.IsNull(controller.ModelState["Password"]);

            controller = this.GetController();
            model = null;
            Assert.IsFalse(controller.IsValidModel(model, false, true));
            Assert.IsNull(controller.ModelState["Name"]);
            Assert.IsNull(controller.ModelState["Role"]);
            Assert.IsNull(controller.ModelState["Password"]);
        }

        /// <summary>
        /// User the controller is valid model not new is admin true.
        /// </summary>
        public void UsersController_IsValidModel_NotNew_IsAdmin_True()
        {
            this.Setup();

            var model = this.GetModel();

            this.workContext.SetupGet(c => c.IsAuthenticated).Returns(true);

            var controller = this.GetController();
            Assert.IsTrue(controller.IsValidModel(model, false, true));
        }

        /// <summary>
        /// User controller is valid model not new no admin authenticated false.
        /// </summary>
        [Test]
        public void UsersController_IsValidModel_NotNew_NoAdmin_Authenticated_False()
        {
            this.Setup();

            var model = this.GetModel();

            var controller = this.GetController();
            model.Role = Data.Entities.Enums.RoleEnum.SuperAdmin;
            model.Password = null;
            Assert.IsFalse(controller.IsValidModel(model, false, false));
        }

        /// <summary>
        /// User the controller is valid model not new no admin false.
        /// </summary>
        [Test]
        public void UsersController_IsValidModel_NotNew_NoAdmin_False()
        {
            this.Setup();

            var model = this.GetModel();

            var controller = this.GetController();
            model.Role = RoleEnum.SuperAdmin;
            model.Password = null;
            model.Name = null;
            Assert.IsFalse(controller.IsValidModel(model, false, false));
            Assert.IsNull(controller.ModelState["Password"]);
            Assert.IsNotNull(controller.ModelState["Role"]);
            Assert.IsNotNull(controller.ModelState["Name"]);
        }

        /// <summary>
        /// User the controller is valid model not new no admin no authenticated true.
        /// </summary>
        [Test]
        public void UsersController_IsValidModel_NotNew_NoAdmin_NoAuthenticated_True()
        {
            this.Setup();

            var model = this.GetModel();

            this.workContext.SetupGet(c => c.IsAuthenticated).Returns(false);

            var controller = this.GetController();
            model.Role = Data.Entities.Enums.RoleEnum.Public;
            model.Password = null;
            Assert.IsTrue(controller.IsValidModel(model, false, false));

            model = this.GetModel();
            model.Role = null;
            model.Password = null;
            Assert.IsTrue(controller.IsValidModel(model, false, false));
            Assert.IsNull(model.Role);
        }

        /// <summary>
        /// Setup the content
        /// </summary>
        protected override void Setup()
        {
            this.authenticationTokenGenerator = new Mock<IAuthenticationTokenGenerator>();
            this.securityHelpers = new Mock<ISecurityHelpers>();
            this.userService = new Mock<IUserService>();
            base.Setup();
        }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        /// <returns>the controller</returns>
        private UsersController GetController()
        {
            return new UsersController(
                this.workContext.Object,
                this.userService.Object,
                this.authenticationTokenGenerator.Object,
                this.securityHelpers.Object);
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <returns>the model</returns>
        private UserModel GetModel()
        {
            return new UserModel
            {
                Id = 1,
                Name = "nombre",
                Email = "email",
                Password = "123",
                Phone = "456",
                Phone2 = "789",
                Role = Data.Entities.Enums.RoleEnum.SuperAdmin
            };
        }
    }
}