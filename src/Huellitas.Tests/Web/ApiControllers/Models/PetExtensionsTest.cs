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
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Services.Contents;
    using Huellitas.Web.Models.Api.Contents;
    using Huellitas.Web.Models.Api.Files;
    using Huellitas.Web.Models.Extensions;
    using Mocks;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Pet Extensions Test
    /// </summary>
    [TestFixture]
    public class PetExtensionsTest
    {
        /// <summary>
        /// Determines whether [is valid false].
        /// </summary>
        [Test]
        public void IsValid_False()
        {
            var model = new PetModel();
            model.Shelter = new ShelterModel();
            model.Files = new List<FileModel>();
            var modelState = new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary();
            Assert.IsFalse(model.IsValid(modelState));
            Assert.IsNotNull(modelState["Images"]);
            Assert.IsNull(modelState["Location"]);

            model = new PetModel();
            model.Files = new List<FileModel>() { new FileModel() { Id = 1 } };
            modelState = new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary();
            Assert.IsFalse(model.IsValid(modelState));
            Assert.IsNotNull(modelState["Location"]);
            Assert.IsNull(modelState["Images"]);
        }

        /// <summary>
        /// Determines whether [is valid true].
        /// </summary>
        [Test]
        public void IsValid_True()
        {
            var model = new PetModel();
            model.Files = new List<FileModel>() { new FileModel() };
            model.Shelter = new ShelterModel();
            Assert.IsTrue(model.IsValid(new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary()));

            model.Shelter = null;
            model.Location = new Huellitas.Web.Models.Api.Common.LocationModel();
            Assert.IsTrue(model.IsValid(new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary()));
        }

        /// <summary>
        /// To the pet entity invalid.
        /// </summary>
        [Test]
        public void ToPetEntityInvalid()
        {
            var mockContentService = new Mock<IContentService>();
            mockContentService.Setup(c => c.GetById(It.IsAny<int>(), false)).Returns((Content)null);

            var model = new PetModel().MockNew();
            model.Shelter = null;
            model.Location = null;
            Assert.Throws<NullReferenceException>(() => model.ToEntity(mockContentService.Object));

            model = new PetModel().MockNew();
            model.Shelter = new ShelterModel() { Id = 1 };
            var ex = Assert.Throws<HuellitasException>(() => model.ToEntity(mockContentService.Object));
            Assert.AreEqual(HuellitasExceptionCode.ShelterNotFound, ex.Code);

            model = new PetModel().MockNew();
            model.Subtype = null;
            Assert.Throws<NullReferenceException>(() => model.ToEntity(mockContentService.Object));

            model = new PetModel().MockNew();
            model.Genre = null;
            Assert.Throws<NullReferenceException>(() => model.ToEntity(mockContentService.Object));

            model = new PetModel().MockNew();
            model.Size = null;
            Assert.Throws<NullReferenceException>(() => model.ToEntity(mockContentService.Object));
        }

        /// <summary>
        /// To the pet entity valid.
        /// </summary>
        [Test]
        public void ToPetEntityValid()
        {
            var locationId = 5;
            var shelterId = 2;

            var mockContentService = new Mock<IContentService>();
            mockContentService.Setup(c => c.GetById(It.IsAny<int>(), false)).Returns(new Content() { Id = shelterId, LocationId = locationId });

            var model = new PetModel().MockNew();
            model.Shelter = new ShelterModel() { Id = 1 };
            var entity = model.ToEntity(mockContentService.Object);

            Assert.AreEqual(locationId, entity.LocationId);

            var x = entity.ContentAttributes.FirstOrDefault(c => c.AttributeType == ContentAttributeType.Shelter);
            var y = entity.ContentAttributes.Where(c => c.AttributeType == ContentAttributeType.Shelter).ToList();

            Assert.AreEqual(shelterId.ToString(), entity.ContentAttributes.FirstOrDefault(c => c.AttributeType == ContentAttributeType.Shelter).Value);
            Assert.AreEqual(model.AutoReply.ToString(), entity.ContentAttributes.FirstOrDefault(c => c.AttributeType == ContentAttributeType.AutoReply).Value);
            Assert.AreEqual(model.Moths.ToString(), entity.ContentAttributes.FirstOrDefault(c => c.AttributeType == ContentAttributeType.Age).Value);
            Assert.AreEqual(model.Subtype.Value.ToString(), entity.ContentAttributes.FirstOrDefault(c => c.AttributeType == ContentAttributeType.Subtype).Value);
            Assert.AreEqual(model.Genre.Value.ToString(), entity.ContentAttributes.FirstOrDefault(c => c.AttributeType == ContentAttributeType.Genre).Value);
            Assert.AreEqual(model.Size.Value.ToString(), entity.ContentAttributes.FirstOrDefault(c => c.AttributeType == ContentAttributeType.Size).Value);
            Assert.AreEqual(model.Castrated.ToString(), entity.ContentAttributes.FirstOrDefault(c => c.AttributeType == ContentAttributeType.Castrated).Value);
        }
    }
}