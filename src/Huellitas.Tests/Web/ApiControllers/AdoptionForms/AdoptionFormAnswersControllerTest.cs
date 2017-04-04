//-----------------------------------------------------------------------
// <copyright file="AdoptionFormAnswersControllerTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.AdoptionForms
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Services.AdoptionForms;
    using Huellitas.Business.Services.Contents;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Entities.Enums;
    using Huellitas.Tests.Web.Mocks;
    using Huellitas.Web.Controllers.Api.AdoptionForms;
    using Huellitas.Web.Infraestructure.WebApi;
    using Huellitas.Web.Models.Api.AdoptionForms;
    using Huellitas.Web.Models.Api.Common;
    using Huellitas.Web.Models.Extensions.AdoptionForms;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Adoption Form Answers Controller Test
    /// </summary>
    /// <seealso cref="Huellitas.Tests.BaseTest" />
    [TestFixture]
    public class AdoptionFormAnswersControllerTest : BaseTest
    {
        /// <summary>
        /// The adoption form service
        /// </summary>
        private Mock<IAdoptionFormService> adoptionFormService = new Mock<IAdoptionFormService>();

        /// <summary>
        /// Determines whether this instance [can user answer form false].
        /// </summary>
        [Test]
        public void CanUserAnswerForm_False()
        {
            this.Setup();
            this.SetupPublicUser(55);

            this.contentService.Setup(c => c.IsUserInContent(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ContentUserRelationType?>()))
                .Returns(false);

            var controller = this.GetController();
            var form = this.GetFormEntity();
            form.Content.UserId = 56;

            Assert.IsFalse(controller.CanUserAnswerForm(form));
        }

        /// <summary>
        /// Determines whether this instance [can user answer form true admin].
        /// </summary>
        [Test]
        public void CanUserAnswerForm_True_Admin()
        {
            this.Setup();
            var controller = this.GetController();
            var form = this.GetFormEntity();
            Assert.IsTrue(controller.CanUserAnswerForm(form));
        }

        /// <summary>
        /// Determines whether this instance [can user answer form true content user].
        /// </summary>
        [Test]
        public void CanUserAnswerForm_True_ContentUser()
        {
            this.Setup();
            this.SetupPublicUser(55);
            var controller = this.GetController();
            var form = this.GetFormEntity();
            form.Content.UserId = 55;
            Assert.IsTrue(controller.CanUserAnswerForm(form));
        }

        /// <summary>
        /// Determines whether this instance [can user answer form true shelter user].
        /// </summary>
        [Test]
        public void CanUserAnswerForm_True_ShelterUser()
        {
            this.Setup();
            this.SetupPublicUser(55);

            this.contentService.Setup(c => c.IsUserInContent(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ContentUserRelationType?>()))
                .Returns(true);

            var controller = this.GetController();
            var form = this.GetFormEntity();
            form.Content.UserId = 56;

            Assert.IsTrue(controller.CanUserAnswerForm(form));
        }

        /// <summary>
        /// Gets the adoption form answer by identifier not found.
        /// </summary>
        [Test]
        public void GetAdoptionFormAnswerById_NotFound()
        {
            this.Setup();

            this.adoptionFormService.Setup(c => c.GetById(It.IsAny<int>()))
                .Returns((AdoptionForm)null);

            var controller = this.GetController();
            var model = this.GetModel();

            var result = controller.Get(1) as NotFoundResult;

            Assert.AreEqual(404, result.StatusCode);
        }

        /// <summary>
        /// Gets the adoption form answer by identifier ok.
        /// </summary>
        [Test]
        public void GetAdoptionFormAnswerById_Ok()
        {
            this.Setup();

            this.adoptionFormService.Setup(c => c.GetById(It.IsAny<int>()))
                .Returns(this.GetFormEntity());

            this.adoptionFormService.Setup(c => c.GetAnswers(It.IsAny<int>()))
                .Returns(this.GetEntities());

            var controller = this.GetController();
            var model = this.GetModel();

            var result = controller.Get(1) as ObjectResult;

            Assert.AreEqual(200, result.StatusCode);
        }

        /// <summary>
        /// Gets the by adoption form answer by identifier forbid.
        /// </summary>
        [Test]
        public void GetByAdoptionFormAnswerById_Forbid()
        {
            this.Setup();
            this.SetupPublicUser(55);

            this.adoptionFormService.Setup(c => c.GetById(It.IsAny<int>()))
                 .Returns(this.GetFormEntity());

            var controller = this.GetController();
            var model = this.GetModel();

            var result = controller.Get(1);

            Assert.IsAssignableFrom(typeof(ForbidResult), result);
        }

        /// <summary>
        /// Posts the adoption form answers bad request.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostAdoptionFormAnswers_BadRequest()
        {
            this.Setup();

            AdoptionFormAnswerModel model = null;
            var controller = this.GetController();

            var result = await controller.Post(1, model) as ObjectResult;

            Assert.AreEqual(400, result.StatusCode);
        }

        /// <summary>
        /// Posts the adoption form answers bad request no adoption form.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostAdoptionFormAnswers_BadRequest_NoAdoptionForm()
        {
            this.Setup();

            this.adoptionFormService.Setup(c => c.InsertAnswer(It.IsAny<AdoptionFormAnswer>()))
                .Throws(new HuellitasException("Content", HuellitasExceptionCode.InvalidForeignKey));

            this.adoptionFormService.Setup(c => c.GetById(It.IsAny<int>()))
                 .Returns(this.GetFormEntity());

            var model = this.GetModel();
            var controller = this.GetController();

            var result = await controller.Post(1, model) as ObjectResult;
            var error = result.Value as BaseApiError;

            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual(HuellitasExceptionCode.InvalidForeignKey.ToString(), error.Error.Code);
        }

        /// <summary>
        /// Posts the adoption form answers forbid.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostAdoptionFormAnswers_Forbid()
        {
            this.Setup();
            this.SetupPublicUser(55);

            this.adoptionFormService.Setup(c => c.GetById(It.IsAny<int>()))
                 .Returns(this.GetFormEntity());

            var model = this.GetModel();
            var controller = this.GetController();

            var result = await controller.Post(1, model);

            Assert.IsAssignableFrom(typeof(ForbidResult), result);
        }

        /// <summary>
        /// Posts the adoption form answers not found.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostAdoptionFormAnswers_NotFound()
        {
            this.Setup();

            this.adoptionFormService.Setup(c => c.GetById(It.IsAny<int>()))
                 .Returns((AdoptionForm)null);

            var model = this.GetModel();
            var controller = this.GetController();

            var result = await controller.Post(1, model) as NotFoundResult;

            Assert.AreEqual(404, result.StatusCode);
        }

        /// <summary>
        /// Posts the adoption form answers ok.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostAdoptionFormAnswers_Ok()
        {
            this.Setup();

            this.adoptionFormService.Setup(c => c.Insert(It.IsAny<AdoptionForm>()))
                .Callback((AdoptionForm c) =>
                {
                    c.Id = 2;
                })
                .Returns(Task.FromResult(0));

            this.adoptionFormService.Setup(c => c.GetById(It.IsAny<int>()))
                 .Returns(this.GetFormEntity());

            var model = this.GetModel();
            var controller = this.GetController();
            controller.AddUrl(true);
            controller.AddResponse();

            var result = await controller.Post(1, model) as ObjectResult;

            Assert.AreEqual(200, result.StatusCode);
            Assert.IsAssignableFrom(typeof(BaseModel), result.Value);
        }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        protected override void Setup()
        {
            this.adoptionFormService = new Mock<IAdoptionFormService>();
            this.contentService = new Mock<IContentService>();
            base.Setup();
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <returns>the attributes</returns>
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
        /// Gets the controller.
        /// </summary>
        /// <returns>the controller</returns>
        private AdoptionFormAnswersController GetController()
        {
            return new AdoptionFormAnswersController(
                this.adoptionFormService.Object,
                this.workContext.Object,
                this.contentService.Object);
        }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <returns>the entities</returns>
        private IList<AdoptionFormAnswer> GetEntities()
        {
            var list = new List<AdoptionFormAnswer>();
            list.Add(this.GetEntity());
            list.Add(this.GetEntity());
            list.Add(this.GetEntity());
            list.Add(this.GetEntity());
            return list;
        }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <returns>the entity</returns>
        private AdoptionFormAnswer GetEntity()
        {
            return new AdoptionFormAnswer()
            {
                Id = 1,
                AdditionalInfo = "additionalinfo",
                AdoptionFormId = 1,
                CreationDate = DateTime.Now,
                Notes = "notes notes notes notes notes",
                StatusEnum = AdoptionFormAnswerStatus.Approved,
                UserId = 1,
                User = new User() { Id = 1, Name = "name", CreatedDate = DateTime.Now }
            };
        }

        /// <summary>
        /// Gets the form entity.
        /// </summary>
        /// <returns>the form</returns>
        private AdoptionForm GetFormEntity()
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
                LastStatusEnum = AdoptionFormAnswerStatus.None,
                LocationId = 1,
                Name = "Username",
                Town = "Barrio",
                UserId = 1,
                Content = new Content() { UserId = 1 }
            };
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <returns>the model</returns>
        private AdoptionFormAnswerModel GetModel()
        {
            return new AdoptionFormAnswerModel()
            {
                Id = 1,
                AdditionalInfo = "additionalinfo",
                AdoptionFormId = 1,
                CreationDate = DateTime.Now,
                Notes = "notes notes notes notes notes",
                Status = AdoptionFormAnswerStatus.Approved,
                User = new Huellitas.Web.Models.Api.Users.BaseUserModel() { Id = 1, Name = "name" }
            };
        }
    }
}