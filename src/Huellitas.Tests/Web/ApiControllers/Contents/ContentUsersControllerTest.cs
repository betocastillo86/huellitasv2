using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Huellitas.Business.Services.Contents;
using Huellitas.Data.Entities;
using Huellitas.Data.Entities.Enums;
using Huellitas.Data.Infraestructure;
using Huellitas.Web.Controllers.Api.Contents;
using Huellitas.Web.Models.Api.Contents;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Huellitas.Tests.Web.ApiControllers.Contents
{
    [TestFixture]
    public class ContentUsersControllerTest : BaseTest
    {
        private Mock<IContentService> contentService = new Mock<IContentService>();

        protected override void Setup()
        {
            this.contentService = new Mock<IContentService>();
            base.Setup();
        }

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

        [Test]
        public void ContentUsersControllerIsValidModel_True()
        {
            this.Setup();
            var model = this.GetModel();
            var controller = this.GetController();
            Assert.IsTrue(controller.IsValidModel(model));
        }

        [Test]
        public void ContentUsersControllerIsValidModel_RelationTypeNull_False()
        {
            this.Setup();
            var model = this.GetModel();
            model = null;
            var controller = this.GetController();
            Assert.IsFalse(controller.IsValidModel(model));
        }

        [Test]
        public void ContentUsersController_CanAddOrRemoveUserToContent_SameUser_True()
        {
            this.Setup();
            this.SetupPublicUser(55);

            var content = new Content() { UserId = 55 };

            var controller = this.GetController();

            Assert.IsTrue(controller.CanAddOrRemoveUserToContent(content));
        }

        [Test]
        public void ContentUsersController_CanAddOrRemoveUserToContent_SuperAdmin_True()
        {
            this.Setup();
            
            var content = this.GetContent();

            var controller = this.GetController();

            Assert.IsTrue(controller.CanAddOrRemoveUserToContent(content));
        }

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

        [Test]
        public void ContentUsersController_CanAddOrRemoveUserToContent_False()
        {
            this.Setup();
            this.SetupPublicUser(55);

            var content = this.GetContent();

            var controller = this.GetController();

            Assert.IsFalse(controller.CanAddOrRemoveUserToContent(content));
        }



        public ContentUsersController GetController()
        {
            return new ContentUsersController(this.contentService.Object, this.workContext.Object);
        }

        public ContentUserFilterModel GetFilter()
        {
            return new ContentUserFilterModel() { RelationType = Data.Entities.Enums.ContentUserRelationType.Parent };
        }

        public ContentUserModel GetModel()
        {
            return new ContentUserModel
            {
                UserId = 1,
                RelationType = Data.Entities.Enums.ContentUserRelationType.Parent
            };
        }

        public Content GetContent()
        {
            return new Content()
            {
                Id = 1,
                Name = "Content"
            };
        }

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


    }
}