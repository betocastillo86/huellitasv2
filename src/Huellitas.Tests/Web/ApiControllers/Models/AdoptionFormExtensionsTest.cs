//-----------------------------------------------------------------------
// <copyright file="AdoptionFormExtensionsTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Models
{
    using System;
    using System.Collections.Generic;
    using Data.Entities;
    using Huellitas.Business.Services.Files;
    using Huellitas.Web.Models.Api.AdoptionForms;
    using Huellitas.Web.Models.Api.Contents;
    using Huellitas.Web.Models.Extensions.AdoptionForms;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Adoption Form Extension Test
    /// </summary>
    [TestFixture]
    public class AdoptionFormExtensionsTest : BaseTest
    {
        private Mock<IFilesHelper> filesHelper = new Mock<IFilesHelper>();

        /// <summary>
        /// Adoptions the form to entity.
        /// </summary>
        [Test]
        public void AdoptionForm_ToEntity()
        {
            this.Setup();

            var model = this.GetModel();

            var entity = model.ToEntity();

            Assert.AreEqual(model.Id, entity.Id);
            Assert.AreEqual(model.Name, entity.Name);
            Assert.AreEqual(model.Address, entity.Address);
            Assert.AreEqual(model.Attributes.Count, entity.Attributes.Count);
            Assert.AreEqual(model.BirthDate, entity.BirthDate);
            Assert.AreEqual(model.ContentId, entity.ContentId);
            Assert.AreEqual(model.CreationDate, entity.CreationDate);
            Assert.AreEqual(model.Email, entity.Email);
            Assert.AreEqual(model.FamilyMembers, entity.FamilyMembers);
            Assert.AreEqual(model.FamilyMembersAge, entity.FamilyMembersAge);
            Assert.AreEqual(model.Job.Value, entity.JobId);
            Assert.AreEqual(model.Status, entity.LastStatusEnum);
            Assert.AreEqual(model.Location.Id, entity.LocationId);
            Assert.AreEqual(model.PhoneNumber, entity.PhoneNumber);
            Assert.AreEqual(model.Town, entity.Town);
        }

        /// <summary>
        /// Adoptions the form to model.
        /// </summary>
        [Test]
        public void AdoptionForm_ToModel()
        {
            this.Setup();

            var entity = this.GetEntity();

            var model = entity.ToModel(this.filesHelper.Object, null);

            Assert.AreEqual(entity.Id, model.Id);
            Assert.AreEqual(entity.Name, model.Name);
            Assert.AreEqual(entity.Address, model.Address);
            Assert.AreEqual(entity.BirthDate, model.BirthDate);
            Assert.AreEqual(entity.Content.Name, model.Content.Name);
            Assert.AreEqual(entity.ContentId, model.ContentId);
            Assert.AreEqual(entity.CreationDate, model.CreationDate);
            Assert.AreEqual(entity.Email, model.Email);
            Assert.AreEqual(entity.FamilyMembers, model.FamilyMembers);
            Assert.AreEqual(entity.FamilyMembersAge, model.FamilyMembersAge);
            Assert.AreEqual(entity.JobId, model.Job.Value);
            Assert.AreEqual(entity.Job.Value, model.Job.Text);
            Assert.AreEqual(entity.LastStatusEnum, model.Status);
            Assert.AreEqual(entity.Location.Name, model.Location.Name);
            Assert.AreEqual(entity.LocationId, model.Location.Id);
            Assert.AreEqual(entity.PhoneNumber, model.PhoneNumber);
            Assert.AreEqual(entity.Town, model.Town);
            Assert.AreEqual(entity.User.Name, model.User.Name);
            Assert.AreEqual(entity.UserId, model.User.Id);
        }

        protected override void Setup()
        {
            this.filesHelper = new Mock<IFilesHelper>();
            base.Setup();
        }
        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <returns>the list</returns>
        private IList<AdoptionFormAttributeModel> GetAttributes()
        {
            var attributes = new List<AdoptionFormAttributeModel>();

            attributes.Add(new AdoptionFormAttributeModel { AttributeId = 1, Question = "question1", Value = "answer1" });
            attributes.Add(new AdoptionFormAttributeModel { AttributeId = 2, Question = "question2", Value = "answer2" });
            attributes.Add(new AdoptionFormAttributeModel { AttributeId = 3, Question = "question3", Value = "answer3" });
            attributes.Add(new AdoptionFormAttributeModel { AttributeId = 4, Question = "question4", Value = "answer4" });
            attributes.Add(new AdoptionFormAttributeModel { AttributeId = 5, Question = "question5", Value = "answer5" });

            return attributes;
        }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <returns>the entity</returns>
        private AdoptionForm GetEntity()
        {
            return new AdoptionForm()
            {
                Id = 1,
                Address = "cr 10 10 10",
                Attributes = this.GetAttributes().ToEntities(),
                BirthDate = DateTime.Now,
                ContentId = 1,
                CreationDate = DateTime.Now,
                Email = "email@email.com",
                FamilyMembers = 1,
                FamilyMembersAge = "20",
                JobId = 1,
                Job = new CustomTableRow() { Id = 1, Value = "job" },
                LastStatusEnum = AdoptionFormAnswerStatus.None,
                LocationId = 1,
                Name = "Username",
                Town = "Barrio",
                UserId = 1,
                Content = new Content() { UserId = 1, Name = "content" },
                Location = new Location { Id = 1, Name = "location" },
                User = new User { Id= 1, Name = "gabriel" }
            };
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <returns>the model</returns>
        private AdoptionFormModel GetModel()
        {
            return new AdoptionFormModel()
            {
                Id = 1,
                Address = "cr 10 10 10",
                Attributes = this.GetAttributes(),
                BirthDate = DateTime.Now,
                ContentId = 1,
                CreationDate = DateTime.Now,
                Email = "email@email.com",
                FamilyMembers = 1,
                FamilyMembersAge = "20",
                Job = new ContentAttributeModel<int> { Text = "Job", Value = 1 },
                Status = AdoptionFormAnswerStatus.None,
                Location = new Huellitas.Web.Models.Api.Common.LocationModel { Id = 1, Name = "location" },
                Name = "Username del usuario",
                Town = "Barrio",
                User = new Huellitas.Web.Models.Api.Users.BaseUserModel { Id = 1, Name = "user" },
                PhoneNumber = "3669223",
                Answers = new List<AdoptionFormAnswerModel>() { new AdoptionFormAnswerModel() { Id =1, AdditionalInfo = "a", CreationDate = DateTime.Now, Notes = "b", Status = AdoptionFormAnswerStatus.Approved, User = new Huellitas.Web.Models.Api.Users.BaseUserModel { Id = 1, Name = "b" } } }
            };
        }
    }
}