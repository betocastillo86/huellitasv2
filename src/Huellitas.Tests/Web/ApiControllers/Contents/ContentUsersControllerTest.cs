//-----------------------------------------------------------------------
// <copyright file="ContentUsersControllerTest.cs" company="Dasigno">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Contents
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Entities.Enums;
    using Huellitas.Data.Infraestructure;
    using Huellitas.Web.Controllers.Api;
    using Huellitas.Web.Models.Api;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Content Users Controller Test
    /// </summary>
    /// <seealso cref="Huellitas.Tests.BaseTest" />
    [TestFixture]
    public class ContentUsersControllerTest : BaseTest
    {
        /// <summary>
        /// Test Content the users controller can add or remove user to content false.
        /// </summary>
        [Test]
        public void ContentUsersController_CanAddOrRemoveUserToContent_False()
        {
            this.Setup();
            this.SetupPublicUser(55);

            var content = this.GetContent();

            var controller = this.GetController();

            Assert.IsFalse(controller.CanAddOrRemoveUserToContent(content));
        }

        /// <summary>
        /// Test Content the users controller can add or remove user to content pet shelter user false.
        /// </summary>
        [Test]
        public void ContentUsersController_CanAddOrRemoveUserToContent_PetShelterUser_False()
        {
            this.Setup();
            this.SetupPublicUser(55);

            var userId = 55;
            var contentId = 1;
            var shelterId = 2;

            var content = this.GetContent();

            this.contentService.Setup(c => c.GetContentAttribute<int?>(contentId, ContentAttributeType.Shelter))
                .Returns(shelterId);

            this.contentService.Setup(c => c.IsUserInContent(userId, shelterId, ContentUserRelationType.Shelter))
                .Returns(false);

            var controller = this.GetController();

            Assert.IsFalse(controller.CanAddOrRemoveUserToContent(content));
        }

        /// <summary>
        /// Test Content the users controller can add or remove user to content pet shelter user true.
        /// </summary>
        [Test]
        public void ContentUsersController_CanAddOrRemoveUserToContent_PetShelterUser_True()
        {
            var userId = 55;
            var contentId = 1;
            var shelterId = 2;

            this.Setup();
            this.SetupPublicUser(userId);

            var content = this.GetContent();
            content.Type = ContentType.Pet;
            content.ContentAttributes.Add(new ContentAttribute() { AttributeType = ContentAttributeType.Shelter, Value = shelterId.ToString() });

            this.contentService.Setup(c => c.GetContentAttribute<int?>(contentId, ContentAttributeType.Shelter))
                .Returns(shelterId);

            this.contentService.Setup(c => c.IsUserInContent(userId, It.IsAny<int>(), ContentUserRelationType.Shelter))
                .Returns(true);

            var controller = this.GetController();

            Assert.IsTrue(controller.CanAddOrRemoveUserToContent(content));
        }

        /// <summary>
        /// Test Content the users controller can add or remove user to content same user true.
        /// </summary>
        [Test]
        public void ContentUsersController_CanAddOrRemoveUserToContent_SameUser_True()
        {
            this.Setup();
            this.SetupPublicUser(55);

            var content = new Content() { UserId = 55 };

            var controller = this.GetController();

            Assert.IsTrue(controller.CanAddOrRemoveUserToContent(content));
        }

        /// <summary>
        /// Test Content the users controller can add or remove user to content super admin true.
        /// </summary>
        [Test]
        public void ContentUsersController_CanAddOrRemoveUserToContent_SuperAdmin_True()
        {
            this.Setup();

            var content = this.GetContent();

            var controller = this.GetController();

            Assert.IsTrue(controller.CanAddOrRemoveUserToContent(content));
        }

        /// <summary>
        /// Test Content the users controller can add or remove user to content user in content true.
        /// </summary>
        [Test]
        public void ContentUsersController_CanAddOrRemoveUserToContent_UserInContent_True()
        {
            var userId = 55;

            this.Setup();
            this.SetupPublicUser(userId);

            var content = this.GetContent();

            this.contentService.Setup(c => c.IsUserInContent(userId, It.IsAny<int>(), It.IsAny<ContentUserRelationType?>()))
                .Returns(true);

            var controller = this.GetController();

            Assert.IsTrue(controller.CanAddOrRemoveUserToContent(content));
        }

        /// <summary>
        /// Test Content the users controller is valid model relation type null false.
        /// </summary>
        [Test]
        public void ContentUsersControllerIsValidModel_RelationTypeNull_False()
        {
            this.Setup();
            var model = this.GetModel();
            model = null;
            var controller = this.GetController();
            Assert.IsFalse(controller.IsValidModel(model));
        }

        /// <summary>
        /// Test Content the users controller is valid model true.
        /// </summary>
        [Test]
        public void ContentUsersControllerIsValidModel_True()
        {
            this.Setup();
            var model = this.GetModel();
            var controller = this.GetController();
            Assert.IsTrue(controller.IsValidModel(model));
        }

        /// <summary>
        /// Deletes the content users forbid.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task DeleteContentUsers_Forbid()
        {
            this.Setup();
            this.SetupPublicUser(55);

            int contentId = 1;
            int userId = 1;

            this.contentService.Setup(c => c.GetContentUserById(contentId, userId))
                .Returns(this.GetEntity());

            var controller = this.GetController();

            var response = await controller.Delete(contentId, userId);

            Assert.IsAssignableFrom(typeof(ForbidResult), response);
        }

        /// <summary>
        /// Deletes the content users not found.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task DeleteContentUsers_NotFound()
        {
            this.Setup();

            int contentId = 1;
            int userId = 1;

            var controller = this.GetController();

            var response = await controller.Delete(contentId, userId) as NotFoundResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        /// <summary>
        /// Deletes the content users ok.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task DeleteContentUsers_Ok()
        {
            this.Setup();

            int contentId = 1;
            int userId = 1;

            this.contentService.Setup(c => c.GetContentUserById(contentId, userId))
                .Returns(this.GetEntity());

            this.contentService.Setup(c => c.DeleteContentUser(It.IsAny<ContentUser>()))
                .Returns(Task.FromResult(0));

            var controller = this.GetController();

            var response = await controller.Delete(contentId, userId) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <returns>the content</returns>
        public Content GetContent()
        {
            return new Content()
            {
                Id = 1,
                Name = "Content"
            };
        }

        /// <summary>
        /// Gets the content users bad request.
        /// </summary>
        [Test]
        public void GetContentUsers_BadRequest()
        {
            this.Setup();

            int contentId = 1;

            var filter = new ContentUserFilterModel();

            var controller = this.GetController();

            var response = controller.Get(contentId, filter) as ObjectResult;

            Assert.AreEqual(400, response.StatusCode);
        }

        /// <summary>
        /// Gets the content users ok.
        /// </summary>
        [Test]
        public void GetContentUsers_Ok()
        {
            this.Setup();

            int contentId = 1;

            var filter = this.GetFilter();

            var contentUsers = new PagedList<ContentUser>(new List<ContentUser>().AsQueryable(), 0, 5);

            this.contentService.Setup(c => c.GetUsersByContentId(contentId, filter.RelationType, true, filter.Page, filter.PageSize))
                .Returns(contentUsers);

            var controller = this.GetController();

            var response = controller.Get(contentId, filter) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        /// <returns>the controller</returns>
        public ContentUsersController GetController()
        {
            return new ContentUsersController(this.contentService.Object, this.workContext.Object);
        }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <returns>the entity</returns>
        public ContentUser GetEntity()
        {
            return new ContentUser()
            {
                Id = 1,
                ContentId = 1,
                UserId = 1,
                Content = this.GetContent(),
                User = new User() { Id = 1, Name = "User" },
                RelationType = Data.Entities.Enums.ContentUserRelationType.Parent
            };
        }

        /// <summary>
        /// Gets the filter.
        /// </summary>
        /// <returns>the model</returns>
        public ContentUserFilterModel GetFilter()
        {
            return new ContentUserFilterModel() { RelationType = Data.Entities.Enums.ContentUserRelationType.Parent };
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <returns>the model</returns>
        public ContentUserModel GetModel()
        {
            return new ContentUserModel
            {
                UserId = 1,
                RelationType = Data.Entities.Enums.ContentUserRelationType.Parent
            };
        }

        /// <summary>
        /// Posts the content users bad request.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostContentUsers_BadRequest()
        {
            this.Setup();

            int contentId = 1;

            var model = this.GetModel();
            model = null;

            var controller = this.GetController();

            var response = await controller.Post(contentId, model) as ObjectResult;

            Assert.AreEqual(400, response.StatusCode);
        }

        /// <summary>
        /// Posts the content users forbid.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostContentUsers_Forbid()
        {
            this.Setup();
            this.SetupPublicUser(55);

            int contentId = 1;

            var model = this.GetModel();

            var content = this.GetContent();

            this.contentService.Setup(c => c.GetById(contentId, false))
                .Returns(content);

            var controller = this.GetController();

            var response = await controller.Post(contentId, model);

            Assert.IsAssignableFrom(typeof(ForbidResult), response);
        }

        /// <summary>
        /// Posts the content users not found.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostContentUsers_NotFound()
        {
            this.Setup();

            int contentId = 1;

            var model = this.GetModel();

            var controller = this.GetController();

            var response = await controller.Post(contentId, model) as NotFoundResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        /// <summary>
        /// Posts the content users ok.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostContentUsers_Ok()
        {
            this.Setup();

            int contentId = 1;

            var model = this.GetModel();

            var entity = this.GetEntity();

            var content = this.GetContent();

            this.contentService.Setup(c => c.GetById(contentId, false))
                .Returns(content);

            this.contentService.Setup(c => c.InsertUser(It.IsAny<ContentUser>()))
                .Returns(Task.FromResult(0));

            var controller = this.GetController();

            var response = await controller.Post(contentId, model) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        protected override void Setup()
        {
            this.contentService = new Mock<IContentService>();
            base.Setup();
        }
    }
}