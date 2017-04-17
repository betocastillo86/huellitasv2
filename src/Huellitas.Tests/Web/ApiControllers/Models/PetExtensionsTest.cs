//-----------------------------------------------------------------------
// <copyright file="PetExtensionsTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Entities;
    using Data.Entities.Enums;
    using Data.Infraestructure;
    using Huellitas.Business.Caching;
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Services;
    using Huellitas.Web.Models.Api;
    using Huellitas.Web.Models.Extensions;
    using Mocks;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Pet Extensions Test
    /// </summary>
    [TestFixture]
    public class PetExtensionsTest : BaseTest
    {
        protected override void Setup()
        {
            base.Setup();
        }

        /// <summary>
        /// To the pet entity invalid.
        /// </summary>
        [Test]
        public void ToPetEntityInvalid()
        {
            this.Setup();
            this.contentService.Setup(c => c.GetById(It.IsAny<int>(), false)).Returns((Content)null);

            var model = new PetModel().MockNew();
            model.Shelter = null;
            model.Location = null;
            Assert.Throws<NullReferenceException>(() => model.ToEntity(this.contentSettings.Object, this.contentService.Object, true));

            model = new PetModel().MockNew();
            model.Shelter = new ShelterModel() { Id = 1 };
            var ex = Assert.Throws<HuellitasException>(() => model.ToEntity(this.contentSettings.Object, this.contentService.Object, true));
            Assert.AreEqual(HuellitasExceptionCode.ShelterNotFound, ex.Code);

            model = new PetModel().MockNew();
            model.Subtype = null;
            Assert.Throws<NullReferenceException>(() => model.ToEntity(this.contentSettings.Object, this.contentService.Object, true));

            model = new PetModel().MockNew();
            model.Genre = null;
            Assert.Throws<NullReferenceException>(() => model.ToEntity(this.contentSettings.Object, this.contentService.Object, true));

            model = new PetModel().MockNew();
            model.Size = null;
            Assert.Throws<NullReferenceException>(() => model.ToEntity(this.contentSettings.Object, this.contentService.Object, true));
        }

        /// <summary>
        /// To the pet entity valid.
        /// </summary>
        [Test]
        public void ToPetEntityValid()
        {
            this.Setup();
            var locationId = 5;
            var shelterId = 2;

            this.contentService.Setup(c => c.GetById(It.IsAny<int>(), false)).Returns(new Content() { Id = shelterId, LocationId = locationId });

            var model = new PetModel().MockNew();
            model.Shelter = new ShelterModel() { Id = 1 };
            
            var entity = model.ToEntity(this.contentSettings.Object, this.contentService.Object, true);

            Assert.AreEqual(locationId, entity.LocationId);

            var x = entity.ContentAttributes.FirstOrDefault(c => c.AttributeType == ContentAttributeType.Shelter);
            var y = entity.ContentAttributes.Where(c => c.AttributeType == ContentAttributeType.Shelter).ToList();

            Assert.AreEqual(shelterId.ToString(), entity.ContentAttributes.FirstOrDefault(c => c.AttributeType == ContentAttributeType.Shelter).Value);
            Assert.AreEqual(model.AutoReply.ToString(), entity.ContentAttributes.FirstOrDefault(c => c.AttributeType == ContentAttributeType.AutoReply).Value);
            Assert.AreEqual(model.Months.ToString(), entity.ContentAttributes.FirstOrDefault(c => c.AttributeType == ContentAttributeType.Age).Value);
            Assert.AreEqual(model.Subtype.Value.ToString(), entity.ContentAttributes.FirstOrDefault(c => c.AttributeType == ContentAttributeType.Subtype).Value);
            Assert.AreEqual(model.Genre.Value.ToString(), entity.ContentAttributes.FirstOrDefault(c => c.AttributeType == ContentAttributeType.Genre).Value);
            Assert.AreEqual(model.Size.Value.ToString(), entity.ContentAttributes.FirstOrDefault(c => c.AttributeType == ContentAttributeType.Size).Value);
            Assert.AreEqual(model.Castrated.ToString(), entity.ContentAttributes.FirstOrDefault(c => c.AttributeType == ContentAttributeType.Castrated).Value);
            Assert.Zero(entity.Users.Count);
        }

        [Test]
        public void ToPetEntity_ClosingDate_Config()
        {
            this.Setup();
            var model = new PetModel().MockNew();

            var timeToClose = 5;
            this.contentSettings.SetupGet(c => c.DaysToAutoClosingPet)
                .Returns(timeToClose);

            var dateTimeClose = DateTime.Now.AddDays(timeToClose);

            var date = DateTime.Now;

            model.ClosingDate = date;

            var entity = model.ToEntity(this.contentSettings.Object, this.contentService.Object, false);

            Assert.AreEqual(dateTimeClose.Date, entity.ClosingDate.Value.Date);
        }

        [Test]
        public void ToPetEntity_ClosingDate_NoConfig_Admin()
        {
            this.Setup();
            var model = new PetModel().MockNew();

            var date = DateTime.Now;

            model.ClosingDate = date;

            var entity = model.ToEntity(this.contentSettings.Object, this.contentService.Object, true);

            Assert.AreEqual(date, entity.ClosingDate);
        }

        [Test]
        public void ToPetEntity_ClosingDate_NoConfig_NoAdmin()
        {
            this.Setup();
            var model = new PetModel().MockNew();
            model.Shelter = new ContentBaseModel { Id = 1, Name = "Name" };

            this.contentService.Setup(c => c.GetById(It.IsAny<int>(), false)).Returns(new Content() { Id = 1, LocationId = 2 });

            var date = DateTime.Now;

            model.ClosingDate = date;

            var entity = model.ToEntity(this.contentSettings.Object, this.contentService.Object, true);

            Assert.AreEqual(date, entity.ClosingDate);
        }



        /// <summary>
        /// To the pet entity valid parents.
        /// </summary>
        [Test]
        public void ToPetEntityValid_Parents()
        {
            var locationId = 5;
            var shelterId = 2;

            this.Setup();
            this.contentService.Setup(c => c.GetById(It.IsAny<int>(), false)).Returns(new Content() { Id = shelterId, LocationId = locationId });

            var model = new PetModel().MockNew();
            model.Parents = new List<ContentUserModel>() { new ContentUserModel { Id = 1, UserId = 1 }, new ContentUserModel { Id = 2, UserId = 2 } };
            model.Shelter = new ShelterModel() { Id = 1 };
            var entity = model.ToEntity(this.contentSettings.Object, this.contentService.Object, true);

            Assert.AreEqual(model.Parents.Count, entity.Users.Count);
        }

        /// <summary>
        /// To the pet model not existent shelter.
        /// </summary>
        [Test]
        public void ToPetModel_NotExistentShelter()
        {
            this.Setup();
            var mockCustomTableService = this.MockCustomTableService();
            this.MockContentService();

            var mockCacheManager = new Mock<ICacheManager>();

            var entity = this.MockEntity();
            entity.ContentAttributes.FirstOrDefault(c => c.AttributeType == ContentAttributeType.Shelter).Value = "55";
            var model = entity.ToPetModel(this.contentService.Object, mockCustomTableService.Object, mockCacheManager.Object, null, null, true);

            Assert.IsEmpty(model.Shelter.Name);
            Assert.AreEqual(55, model.Shelter.Id);
        }

        /// <summary>
        /// To the pet model null file helpers.
        /// </summary>
        [Test]
        public void ToPetModel_NullFileHelpers()
        {
            this.Setup();
            var mockCustomTableService = this.MockCustomTableService();
            this.MockContentService();

            var mockCacheManager = new Mock<ICacheManager>();

            var entity = this.MockEntity();
            var model = entity.ToPetModel(this.contentService.Object, mockCustomTableService.Object, mockCacheManager.Object, null, null, true);

            Assert.IsNull(model.Files);
        }

        /// <summary>
        /// To the pet model with related pets.
        /// </summary>
        [Test]
        public void ToPetModel_WithRelatedPets()
        {
            this.Setup();
            var mockCustomTableService = this.MockCustomTableService();
            this.MockContentService();

            var mockCacheManager = new Mock<ICacheManager>();

            var entity = this.MockEntity();
            var model = entity.ToPetModel(this.contentService.Object, mockCustomTableService.Object, mockCacheManager.Object, null, null, false, true);

            Assert.AreEqual(2, model.RelatedPets.Count);
        }

        /// <summary>
        /// To the pet model valid.
        /// </summary>
        [Test]
        public void ToPetModelValid()
        {
            this.Setup();
            var mockCustomTableService = this.MockCustomTableService();
            this.MockContentService();

            var mockFilesHelper = new Mock<IFilesHelper>();
            mockFilesHelper.Setup(c => c.GetFullPath(It.IsAny<File>(), null, 0, 0)).Returns("filename.jpg");

            var mockCacheManager = new Mock<ICacheManager>();

            var entity = this.MockEntity();
            var model = entity.ToPetModel(this.contentService.Object, mockCustomTableService.Object, mockCacheManager.Object, mockFilesHelper.Object, null, true, withRelated: false);

            Assert.AreEqual(entity.Id, model.Id);
            Assert.AreEqual(entity.Name, model.Name);
            Assert.AreEqual(entity.Body, model.Body);
            Assert.AreEqual(entity.CommentsCount, model.CommentsCount);
            Assert.AreEqual(entity.DisplayOrder, model.DisplayOrder);
            Assert.AreEqual(entity.StatusType, model.Status);
            Assert.AreEqual(entity.Views, model.Views);
            Assert.AreEqual(entity.CreatedDate, model.CreatedDate);
            Assert.AreEqual(entity.LocationId, model.Location.Id);
            Assert.AreEqual(entity.Location.Name, model.Location.Name);
            Assert.AreEqual(entity.User.Id, model.User.Id);
            Assert.AreEqual("TableRowName", model.Subtype.Text);
            Assert.AreEqual("TableRowName", model.Genre.Text);
            Assert.AreEqual(10, model.Months);
            Assert.Zero(model.RelatedPets.Count);
            Assert.AreEqual("ContentName", model.Shelter.Name);
            Assert.AreEqual(2, model.Files.Count);
            Assert.AreEqual("File1", model.Files[0].Name);
            Assert.AreEqual("filename.jpg", model.Files[0].FileName);
        }

        /// <summary>
        /// Mocks the content service.
        /// </summary>
        /// <returns>the mock</returns>
        private void MockContentService()
        {
            this.contentService.Setup(c => c.GetFiles(It.IsAny<int>())).Returns(new List<ContentFile>() { new ContentFile { File = new File { Id = 1, Name = "File1" } }, new ContentFile { File = new File { Id = 2, Name = "File2" } } });
            this.contentService.Setup(c => c.Search(null, It.IsAny<ContentType>(), null, int.MaxValue, 0, ContentOrderBy.DisplayOrder, null, null, null, null)).Returns(new PagedList<Content> { new Content { Id = 1, Name = "ContentName" } });
            this.contentService.Setup(c => c.GetRelated(It.IsAny<int>(), It.IsAny<RelationType>(), 0, int.MaxValue)).Returns(new PagedList<Content> { new Content { Id = 1, Name = "Content1" }, new Content { Id = 1, Name = "Content2" } });
        }

        /// <summary>
        /// Mocks the custom table service.
        /// </summary>
        /// <returns>the mock</returns>
        private Mock<ICustomTableService> MockCustomTableService()
        {
            var mockCustomTableService = new Mock<ICustomTableService>();
            mockCustomTableService.Setup(c => c.GetRowsByTableIdCached(It.IsAny<CustomTableType>())).Returns(new List<CustomTableRow> { new CustomTableRow { Id = 1, Value = "TableRowName" } });
            return mockCustomTableService;
        }

        /// <summary>
        /// Mocks the entity.
        /// </summary>
        /// <returns>the mock</returns>
        private Content MockEntity()
        {
            var entity = new Content();
            entity.Id = 1;
            entity.Name = "Name";
            entity.Body = "Body";
            entity.CommentsCount = 5;
            entity.DisplayOrder = 1;
            entity.StatusType = StatusType.Closed;
            entity.Views = 6;
            entity.CreatedDate = DateTime.Now;
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