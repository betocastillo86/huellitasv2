﻿//-----------------------------------------------------------------------
// <copyright file="ContentExtensionsTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Beto.Core.Data;
    using Beto.Core.Data.Files;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Extensions;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Content Extensions Test
    /// </summary>
    [TestFixture]
    public class ContentExtensionsTest : BaseTest
    {
        /// <summary>
        /// The mock of cache
        /// </summary>
        private Mock<IFilesHelper> mockCacheManager = new Mock<IFilesHelper>();

        /// <summary>
        /// Determines whether this instance [can user edit pet super admin true].
        /// </summary>
        [Test]
        public void CanUserEditPet_SuperAdmin_True()
        {
            var user = new User() { Id = 1, Name = "Name", RoleEnum = RoleEnum.SuperAdmin };
            var content = new Content { };

            var response = ContentExtensions.CanUserEditPet(user, content, this.contentService.Object);
            Assert.IsTrue(response);
        }

        /// <summary>
        /// Determines whether this instance [can user edit pet user content true].
        /// </summary>
        [Test]
        public void CanUserEditPet_UserContent_True()
        {
            var user = new User() { Id = 1, Name = "Name", RoleEnum = RoleEnum.Public };
            var content = new Content { UserId = 1 };

            var response = ContentExtensions.CanUserEditPet(user, content, this.contentService.Object);
            Assert.IsTrue(response);
        }

        /// <summary>
        /// Determines whether this instance [can user edit pet user content false].
        /// </summary>
        [Test]
        public void CanUserEditPet_UserContent_False()
        {
            var user = new User() { Id = 1, Name = "Name", RoleEnum = RoleEnum.Public };
            var content = new Content { UserId = 2 };

            var response = ContentExtensions.CanUserEditPet(user, content, this.contentService.Object);
            Assert.IsFalse(response);
        }

        /// <summary>
        /// Determines whether this instance [can user edit pet shelter owner true].
        /// </summary>
        [Test]
        public void CanUserEditPet_ShelterOwner_True()
        {
            var user = new User() { Id = 2, Name = "Name", RoleEnum = RoleEnum.Public };
            var content = new Content { UserId = 1, ContentAttributes = new List<ContentAttribute> { new ContentAttribute { AttributeType = ContentAttributeType.Shelter, Value = "1" } } };

            var contentUsers = new PagedList<ContentUser>((new List<ContentUser> { new ContentUser() { UserId = 2 } }).AsQueryable(), 0, 5);
            this.contentService.Setup(c => c.GetUsersByContentId(
                It.IsAny<int>(),
                ContentUserRelationType.Shelter,
                false,
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(contentUsers);

            var response = ContentExtensions.CanUserEditPet(user, content, this.contentService.Object);
            Assert.IsTrue(response);
        }

        /// <summary>
        /// Determines whether this instance [can user edit pet shelter owner false].
        /// </summary>
        [Test]
        public void CanUserEditPet_ShelterOwner_False()
        {
            var user = new User() { Id = 1, Name = "Name", RoleEnum = RoleEnum.Public };
            var content = new Content { UserId = 2 };

            var contentUsers = new PagedList<ContentUser>((new List<ContentUser> { new ContentUser() { UserId = 3 } }).AsQueryable(), 0, 5);
            this.contentService.Setup(c => c.GetUsersByContentId(It.IsAny<int>(), ContentUserRelationType.Shelter, true, It.IsAny<int>(), It.IsAny<int>()))
                .Returns(contentUsers);

            var response = ContentExtensions.CanUserEditPet(user, content, this.contentService.Object);
            Assert.IsFalse(response);
        }

        /// <summary>
        /// Determines whether this instance [can user edit pet user content false].
        /// </summary>
        [Test]
        public void CanUserEditShelter_OwnerContent_False()
        {
            var user = new User() { Id = 1, Name = "Name", RoleEnum = RoleEnum.Public };
            var content = new Content { UserId = 2 };

            var response = ContentExtensions.CanUserEditShelter(user, content, this.contentService.Object);
            Assert.IsFalse(response);
        }

        /// <summary>
        /// Determines whether this instance [can user edit shelter owner content true].
        /// </summary>
        [Test]
        public void CanUserEditShelter_OwnerContent_True()
        {
            var user = new User() { Id = 1, Name = "Name", RoleEnum = RoleEnum.Public };
            var content = new Content { UserId = 1 };

            var response = ContentExtensions.CanUserEditShelter(user, content, this.contentService.Object);
            Assert.IsTrue(response);
        }

        /// <summary>
        /// Converts to content model test without file
        /// </summary>
        [Test]
        public void ToContentModel_NoFile()
        {
            var content = this.GetContent();

            var model = content.ToModel(this.mockCacheManager.Object);

            Assert.AreEqual(content.Id, model.Id);
            Assert.AreEqual(content.Name, model.Name);
            Assert.AreEqual(content.Body, model.Body);
            Assert.AreEqual(content.CommentsCount, model.CommentsCount);
            Assert.AreEqual(content.DisplayOrder, model.DisplayOrder);
            Assert.AreEqual(content.StatusType, model.Status);
            Assert.AreEqual(content.Views, model.Views);
            Assert.AreEqual(content.CreatedDate, model.CreatedDate);
            Assert.AreEqual(content.LocationId, model.Location.Id);
            Assert.IsNull(model.Image);
            Assert.AreEqual(content.UserId, model.User.Id);
            Assert.AreEqual(content.ContentAttributes.ElementAt(0).Value, model.Attributes.ElementAt(0).Value);
            Assert.AreEqual(content.ContentAttributes.ElementAt(1).Value, model.Attributes.ElementAt(1).Value);
            Assert.AreEqual(content.ContentAttributes.ElementAt(2).Value, model.Attributes.ElementAt(2).Value);
            Assert.AreEqual(content.ContentAttributes.ElementAt(3).Value, model.Attributes.ElementAt(3).Value);
        }

        /// <summary>
        /// Converts to content model test with file
        /// </summary>
        [Test]
        public void ToContentModel_WithFile()
        {
            var content = this.GetContent();
            content.FileId = 1;
            content.File = new File { Id = 1, Name = "nombre", FileName = "nombrearchivo" };

            var model = content.ToModel(this.mockCacheManager.Object);

            Assert.AreEqual(content.Id, model.Id);
            Assert.AreEqual(content.Name, model.Name);
            Assert.AreEqual(content.Body, model.Body);
            Assert.AreEqual(content.CommentsCount, model.CommentsCount);
            Assert.AreEqual(content.DisplayOrder, model.DisplayOrder);
            Assert.AreEqual(content.StatusType, model.Status);
            Assert.AreEqual(content.Views, model.Views);
            Assert.AreEqual(content.CreatedDate, model.CreatedDate);
            Assert.AreEqual(content.LocationId, model.Location.Id);
            Assert.AreEqual(content.UserId, model.User.Id);
            Assert.AreEqual(content.File.Name, model.Image.Name);
            Assert.AreEqual(content.ContentAttributes.ElementAt(0).Value, model.Attributes.ElementAt(0).Value);
            Assert.AreEqual(content.ContentAttributes.ElementAt(1).Value, model.Attributes.ElementAt(1).Value);
            Assert.AreEqual(content.ContentAttributes.ElementAt(2).Value, model.Attributes.ElementAt(2).Value);
            Assert.AreEqual(content.ContentAttributes.ElementAt(3).Value, model.Attributes.ElementAt(3).Value);
        }

        /// <summary>
        /// gets the content
        /// </summary>
        /// <returns>the content</returns>
        private Content GetContent()
        {
            var entity = new Content();
            entity.Id = 1;
            entity.Name = "Name";
            entity.Body = "Body";
            entity.CommentsCount = 5;
            entity.DisplayOrder = 1;
            entity.StatusType = StatusType.Closed;
            entity.Views = 6;
            entity.CreatedDate = DateTime.UtcNow;
            entity.LocationId = 7;
            entity.Location = new Location() { Id = 7, Name = "Location" };
            entity.UserId = 1;
            entity.User = new User() { Id = 1, Name = "User" };

            entity.ContentAttributes.Add(new ContentAttribute() { AttributeType = ContentAttributeType.Subtype, Value = "1" });
            entity.ContentAttributes.Add(new ContentAttribute() { AttributeType = ContentAttributeType.Genre, Value = "1" });
            entity.ContentAttributes.Add(new ContentAttribute() { AttributeType = ContentAttributeType.Age, Value = "10" });
            entity.ContentAttributes.Add(new ContentAttribute() { AttributeType = ContentAttributeType.Shelter, Value = "1" });

            return entity;
        }
    }
}