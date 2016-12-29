//-----------------------------------------------------------------------
// <copyright file="ContentExtensionsTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Models
{
    using System.Collections.Generic;
    using Huellitas.Business.Services.Contents;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Extensions;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Content Extensions Test
    /// </summary>
    [TestFixture]
    public class ContentExtensionsTest
    {
        /// <summary>
        /// The content service
        /// </summary>
        private Mock<IContentService> contentService = new Mock<IContentService>();

        /// <summary>
        /// Determines whether this instance [can user edit pet super admin true].
        /// </summary>
        [Test]
        public void CanUserEditPet_SuperAdmin_True()
        {
            var user = new User() { Id = 1, Name = "Name", RoleEnum = Data.Entities.Enums.RoleEnum.SuperAdmin };
            var content = new Content { };

            var response = ContentExtensions.CanUserEditPet(user, content, contentService.Object);
            Assert.IsTrue(response);
        }

        /// <summary>
        /// Determines whether this instance [can user edit pet user content true].
        /// </summary>
        [Test]
        public void CanUserEditPet_UserContent_True()
        {
            var user = new User() { Id = 1, Name = "Name", RoleEnum = Data.Entities.Enums.RoleEnum.Public };
            var content = new Content { UserId = 1 };

            var response = ContentExtensions.CanUserEditPet(user, content, contentService.Object);
            Assert.IsTrue(response);
        }

        /// <summary>
        /// Determines whether this instance [can user edit pet user content false].
        /// </summary>
        [Test]
        public void CanUserEditPet_UserContent_False()
        {
            var user = new User() { Id = 1, Name = "Name", RoleEnum = Data.Entities.Enums.RoleEnum.Public };
            var content = new Content { UserId = 2 };

            var response = ContentExtensions.CanUserEditPet(user, content, contentService.Object);
            Assert.IsFalse(response);
        }

        /// <summary>
        /// Determines whether this instance [can user edit pet shelter owner true].
        /// </summary>
        [Test]
        public void CanUserEditPet_ShelterOwner_True()
        {
            var user = new User() { Id = 2, Name = "Name", RoleEnum = Data.Entities.Enums.RoleEnum.Public };
            var content = new Content { UserId = 1, ContentAttributes = new List<ContentAttribute> { new ContentAttribute { AttributeType = ContentAttributeType.Shelter, Value = "1" } } };

            var contentUsers = new List<ContentUser>() { new ContentUser() { UserId = 2 } };
            this.contentService.Setup(c => c.GetUsersByContentId(It.IsAny<int>(), Data.Entities.Enums.ContentUserRelationType.Shelter))
                .Returns(contentUsers);

            var response = ContentExtensions.CanUserEditPet(user, content, contentService.Object);
            Assert.IsTrue(response);
        }

        /// <summary>
        /// Determines whether this instance [can user edit pet shelter owner false].
        /// </summary>
        [Test]
        public void CanUserEditPet_ShelterOwner_False()
        {
            var user = new User() { Id = 1, Name = "Name", RoleEnum = Data.Entities.Enums.RoleEnum.Public };
            var content = new Content { UserId = 2 };

            var contentUsers = new List<ContentUser>() { new ContentUser() { UserId = 3 } };
            this.contentService.Setup(c => c.GetUsersByContentId(It.IsAny<int>(), Data.Entities.Enums.ContentUserRelationType.Shelter))
                .Returns(contentUsers);

            var response = ContentExtensions.CanUserEditPet(user, content, contentService.Object);
            Assert.IsFalse(response);
        }

        /// <summary>
        /// Determines whether this instance [can user edit pet user content false].
        /// </summary>
        [Test]
        public void CanUserEditShelter_OwnerContent_False()
        {
            var user = new User() { Id = 1, Name = "Name", RoleEnum = Data.Entities.Enums.RoleEnum.Public };
            var content = new Content { UserId = 2 };

            var response = ContentExtensions.CanUserEditPet(user, content, contentService.Object);
            Assert.IsFalse(response);
        }

        /// <summary>
        /// Determines whether this instance [can user edit shelter owner content true].
        /// </summary>
        [Test]
        public void CanUserEditShelter_OwnerContent_True()
        {
            var user = new User() { Id = 1, Name = "Name", RoleEnum = Data.Entities.Enums.RoleEnum.Public };
            var content = new Content { UserId = 1 };

            var response = ContentExtensions.CanUserEditPet(user, content, contentService.Object);
            Assert.IsTrue(response);
        }


    }
}