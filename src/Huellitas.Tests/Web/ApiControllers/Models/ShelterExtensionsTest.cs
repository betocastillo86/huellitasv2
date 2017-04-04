//-----------------------------------------------------------------------
// <copyright file="ShelterExtensionsTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Business.Services.Contents;
    using Huellitas.Business.Services.Files;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Extensions;
    using Huellitas.Web.Models.Api.Contents;
    using Huellitas.Web.Models.Extensions.Contents;
    using Mocks;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Shelter Extensions Test
    /// </summary>
    /// <seealso cref="Huellitas.Tests.BaseTest" />
    [TestFixture]
    public class ShelterExtensionsTest : BaseTest
    {
        /// <summary>
        /// The files helper
        /// </summary>
        private Mock<IFilesHelper> filesHelper = new Mock<IFilesHelper>();

        /// <summary>
        /// To the shelter entity invalid.
        /// </summary>
        [Test]
        public void ToShelterEntity_Invalid()
        {
            var model = new ShelterModel().MockNew();
            model.Location = null;
            Assert.Throws<NullReferenceException>(() => model.ToEntity(this.contentService.Object));

            model = new ShelterModel().MockNew();
            model.Files = null;
            Assert.Throws<NullReferenceException>(() => model.ToEntity(this.contentService.Object));
        }

        /// <summary>
        /// To the shelter entity content null valid.
        /// </summary>
        [Test]
        public void ToShelterEntity_ContentNull_Valid()
        {
            var model = new ShelterModel().MockNew();

            var entity = model.ToEntity(this.contentService.Object, null, null);

            Assert.Zero(entity.Id);
            Assert.AreEqual(StatusType.Created, entity.StatusType);
            Assert.AreEqual(ContentType.Shelter, entity.Type);
            Assert.IsFalse(entity.Featured);
            Assert.AreEqual(2, entity.ContentFiles.Count);

            Assert.AreEqual(model.Name, entity.Name);
            Assert.AreEqual(model.Body, entity.Body);
            Assert.AreEqual(model.DisplayOrder, entity.DisplayOrder);
            Assert.AreEqual(model.Location.Id, entity.LocationId);
            Assert.AreEqual(model.Email, entity.Email);
            Assert.AreEqual(model.Files[0].Id, entity.FileId);
            Assert.AreEqual(model.Facebook, entity.GetAttribute<string>(ContentAttributeType.Facebook));
            Assert.AreEqual(model.Twitter, entity.GetAttribute<string>(ContentAttributeType.Twitter));
            Assert.AreEqual(model.Instagram, entity.GetAttribute<string>(ContentAttributeType.Instagram));
            Assert.AreEqual(model.Video, entity.GetAttribute<string>(ContentAttributeType.Video));
            Assert.AreEqual(model.Owner, entity.GetAttribute<string>(ContentAttributeType.Owner));
            Assert.AreEqual(model.Address, entity.GetAttribute<string>(ContentAttributeType.Address));
            Assert.AreEqual(model.Lat, entity.GetAttribute<decimal>(ContentAttributeType.Lat));
            Assert.AreEqual(model.Lng, entity.GetAttribute<decimal>(ContentAttributeType.Lng));
        }

        /// <summary>
        /// To the shelter entity users null valid.
        /// </summary>
        [Test]
        public void ToShelterEntity_Users_Null_Valid()
        {
            var model = new ShelterModel().MockNew();

            var entity = model.ToEntity(this.contentService.Object, null, null);

            Assert.Zero(entity.Users.Count);
        }

        /// <summary>
        /// To the shelter entity users not null valid.
        /// </summary>
        [Test]
        public void ToShelterEntity_Users_NotNull_Valid()
        {
            var model = new ShelterModel().MockNew();
            model.Users = new List<ContentUserModel>() { new ContentUserModel { Id = 1, UserId = 1 }, new ContentUserModel { Id = 2, UserId = 1 } };

            var entity = model.ToEntity(this.contentService.Object, null, null);

            Assert.AreEqual(2, entity.Users.Count);
        }

        /// <summary>
        /// To the shelter entity content not null valid.
        /// </summary>
        [Test]
        public void ToShelterEntity_ContentNotNull_Valid()
        {
            var model = new ShelterModel().MockNew();

            var oldContent = this.MockEntity();

            var entity = model.ToEntity(this.contentService.Object, oldContent, null);

            Assert.AreEqual(oldContent.Id, entity.Id);
            Assert.AreEqual(oldContent.StatusType, entity.StatusType);
            Assert.AreEqual(oldContent.Type, entity.Type);
            Assert.AreEqual(oldContent.Featured, entity.Featured);
            Assert.AreEqual(oldContent.ContentFiles.Count, entity.ContentFiles.Count);
            Assert.AreEqual(model.Name, entity.Name);
            Assert.AreEqual(model.Body, entity.Body);
            Assert.AreEqual(model.DisplayOrder, entity.DisplayOrder);
            Assert.AreEqual(model.Location.Id, entity.LocationId);
            Assert.AreEqual(model.Email, entity.Email);
            Assert.AreEqual(model.Files[0].Id, entity.FileId);
            Assert.AreEqual(model.Facebook, entity.GetAttribute<string>(ContentAttributeType.Facebook));
            Assert.AreEqual(model.Twitter, entity.GetAttribute<string>(ContentAttributeType.Twitter));
            Assert.AreEqual(model.Instagram, entity.GetAttribute<string>(ContentAttributeType.Instagram));
            Assert.AreEqual(model.Video, entity.GetAttribute<string>(ContentAttributeType.Video));
            Assert.AreEqual(model.Owner, entity.GetAttribute<string>(ContentAttributeType.Owner));
            Assert.AreEqual(model.Address, entity.GetAttribute<string>(ContentAttributeType.Address));
            Assert.AreEqual(model.Lat, entity.GetAttribute<decimal>(ContentAttributeType.Lat));
            Assert.AreEqual(model.Lng, entity.GetAttribute<decimal>(ContentAttributeType.Lng));
        }

        /// <summary>
        /// To the shelter model without files valid.
        /// </summary>
        [Test]
        public void ToShelterModel_WithoutFiles_Valid()
        {
            var content = this.MockEntity();

            this.filesHelper.Setup(c => c.GetFullPath(It.IsAny<File>(), null, 0, 0))
                .Returns("thefile");

            var model = content.ToShelterModel(this.contentService.Object, this.filesHelper.Object, null, false);

            Assert.AreEqual(content.Id, model.Id);
            Assert.AreEqual(content.Name, model.Name);
            Assert.AreEqual(content.Body, model.Body);
            Assert.AreEqual(content.CommentsCount, model.CommentsCount);
            Assert.AreEqual(content.DisplayOrder, model.DisplayOrder);
            Assert.AreEqual(content.StatusType, model.Status);
            Assert.AreEqual(content.Type, model.TypeId);
            Assert.AreEqual(content.Views, model.Views);
            Assert.AreEqual(content.CreatedDate, model.CreatedDate);
            Assert.AreEqual(content.Featured, model.Featured);
            Assert.AreEqual(content.Email, model.Email);
            Assert.AreEqual(content.DisplayOrder, model.DisplayOrder);
            Assert.AreEqual("thefile", model.Image.FileName);
            Assert.AreEqual(content.LocationId, model.Location.Id);
            Assert.AreEqual(content.UserId, model.User.Id);
            Assert.AreEqual(model.Facebook, content.GetAttribute<string>(ContentAttributeType.Facebook));
            Assert.AreEqual(model.Twitter, content.GetAttribute<string>(ContentAttributeType.Twitter));
            Assert.AreEqual(model.Instagram, content.GetAttribute<string>(ContentAttributeType.Instagram));
            Assert.AreEqual(model.Video, content.GetAttribute<string>(ContentAttributeType.Video));
            Assert.AreEqual(model.Owner, content.GetAttribute<string>(ContentAttributeType.Owner));
            Assert.AreEqual(model.Address, content.GetAttribute<string>(ContentAttributeType.Address));
            Assert.AreEqual(model.Lat, content.GetAttribute<decimal>(ContentAttributeType.Lat));
            Assert.AreEqual(model.Lng, content.GetAttribute<decimal>(ContentAttributeType.Lng));
            Assert.IsNull(model.Files);
        }

        /// <summary>
        /// To the shelter model with files valid.
        /// </summary>
        [Test]
        public void ToShelterModel_WithFiles_Valid()
        {
            var content = this.MockEntity();

            this.filesHelper.Setup(c => c.GetFullPath(It.IsAny<File>(), null, 0, 0))
                .Returns("thefile");

            this.contentService.Setup(c => c.GetFiles(content.Id))
                .Returns(content.ContentFiles.ToList());

            var model = content.ToShelterModel(this.contentService.Object, this.filesHelper.Object, null, true);
            Assert.AreEqual(2, model.Files.Count);
            Assert.AreEqual(content.ContentFiles.ElementAt(0).File.Name, model.Files[0].Name);
            Assert.AreEqual(content.ContentFiles.ElementAt(1).File.Name, model.Files[1].Name);
        }

        /// <summary>
        /// To the shelter model with files file helper null valid.
        /// </summary>
        [Test]
        public void ToShelterModel_WithFiles_FileHelperNull_Valid()
        {
            var content = this.MockEntity();

            this.filesHelper.Setup(c => c.GetFullPath(It.IsAny<File>(), null, 0, 0))
                .Returns("thefile");

            var model = content.ToShelterModel(this.contentService.Object, null, null, true);
            Assert.IsNull(model.Files);
        }

        /// <summary>
        /// To the shelter model no default file valid.
        /// </summary>
        [Test]
        public void ToShelterModel_NoDefaultFile_Valid()
        {
            var content = this.MockEntity();

            this.filesHelper.Setup(c => c.GetFullPath(It.IsAny<File>(), null, 0, 0))
                .Returns("thefile");
            content.File = null;

            var model = content.ToShelterModel(this.contentService.Object, this.filesHelper.Object, null, false);
            Assert.IsNull(model.Image);
        }

        /// <summary>
        /// To the shelter model no location valid.
        /// </summary>
        [Test]
        public void ToShelterModel_NoLocation_Valid()
        {
            var content = this.MockEntity();

            this.filesHelper.Setup(c => c.GetFullPath(It.IsAny<File>(), null, 0, 0))
                .Returns("thefile");
            content.Location = null;

            var model = content.ToShelterModel(this.contentService.Object, this.filesHelper.Object, null, false);
            Assert.IsNull(model.Location);
        }

        /// <summary>
        /// To the shelter model no user valid.
        /// </summary>
        [Test]
        public void ToShelterModel_NoUser_Valid()
        {
            var content = this.MockEntity();

            this.filesHelper.Setup(c => c.GetFullPath(It.IsAny<File>(), null, 0, 0))
                .Returns("thefile");
            content.User = null;

            var model = content.ToShelterModel(this.contentService.Object, this.filesHelper.Object, null, false);
            Assert.IsNull(model.User);
        }

        /// <summary>
        /// Mocks the entity.
        /// </summary>
        /// <returns>the content</returns>
        private Content MockEntity()
        {
            var content = new Content();

            content.Id = 1;
            content.Name = "shelter";
            content.Body = "body";
            content.CommentsCount = 2;
            content.DisplayOrder = 3;
            content.StatusType = StatusType.Hidden;
            content.StatusType = StatusType.Published;
            content.Type = ContentType.Shelter;
            content.Views = 4;
            content.CreatedDate = new DateTime(2016, 12, 12);
            content.Featured = true;
            content.Email = "shelter@shelter.com";
            content.FileId = 5;
            content.File = new File() { Id = 5, Name = "file", FileName = "file" };
            content.Location = new Location() { Id = 6, Name = "location" };
            content.LocationId = 6;
            content.UserId = 7;
            content.User = new User() { Id = 7, Name = "user" };
            content.ContentFiles = new List<ContentFile> { new ContentFile { FileId = 1, File = new File { Id = 1, Name = "file1", FileName = "file1" } }, new ContentFile { FileId = 2, File = new File { Id = 2, Name = "file2", FileName = "file2" } } };

            content.ContentAttributes.Add(ContentAttributeType.Phone1, ContentAttributeType.Phone1.ToString());
            content.ContentAttributes.Add(ContentAttributeType.Phone2, ContentAttributeType.Phone2.ToString());
            content.ContentAttributes.Add(ContentAttributeType.Lat, 8);
            content.ContentAttributes.Add(ContentAttributeType.Lng, 9);
            content.ContentAttributes.Add(ContentAttributeType.Owner, ContentAttributeType.Owner.ToString());
            content.ContentAttributes.Add(ContentAttributeType.Address, ContentAttributeType.Address.ToString());
            content.ContentAttributes.Add(ContentAttributeType.Facebook, ContentAttributeType.Facebook.ToString());
            content.ContentAttributes.Add(ContentAttributeType.Twitter, ContentAttributeType.Twitter.ToString());
            content.ContentAttributes.Add(ContentAttributeType.Instagram, ContentAttributeType.Instagram.ToString());
            content.ContentAttributes.Add(ContentAttributeType.Video, ContentAttributeType.Video.ToString());

            return content;
        }
    }
}