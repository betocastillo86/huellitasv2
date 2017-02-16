using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Huellitas.Business.Models;
using Huellitas.Business.Services.AdoptionForms;
using Huellitas.Business.Services.Common;
using Huellitas.Business.Services.Contents;
using Huellitas.Business.Services.Files;
using Huellitas.Data.Entities;
using Huellitas.Data.Infraestructure;
using Huellitas.Tests.Web.Mocks;
using Huellitas.Web.Controllers.Api.AdoptionForms;
using Huellitas.Web.Controllers.Api.Common;
using Huellitas.Web.Infraestructure.WebApi;
using Huellitas.Web.Models.Api.AdoptionForms;
using Huellitas.Web.Models.Api.Contents;
using Huellitas.Web.Models.Extensions.AdoptionForms;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Huellitas.Tests.Web.ApiControllers.AdoptionForms
{
    [TestFixture]
    public class AdoptionFormsControllerTest : BaseTest
    {

        /// <summary>
        /// The content service
        /// </summary>
        private Mock<IContentService> contentService = new Mock<IContentService>();

        /// <summary>
        /// The adoption form service
        /// </summary>
        private Mock<IAdoptionFormService> adoptionFormService = new Mock<IAdoptionFormService>();

        /// <summary>
        /// The files helper
        /// </summary>
        private Mock<IFilesHelper> filesHelper = new Mock<IFilesHelper>();

        /// <summary>
        /// The custom table service
        /// </summary>
        private Mock<ICustomTableService> customTableService = new Mock<ICustomTableService>();

        /// <summary>
        /// Setups this instance.
        /// </summary>
        protected override void Setup()
        {
            this.adoptionFormService = new Mock<IAdoptionFormService>();
            this.filesHelper = new Mock<IFilesHelper>();
            this.contentService = new Mock<IContentService>();
            this.customTableService = new Mock<ICustomTableService>();
            base.Setup();
        }

        /// <summary>
        /// Gets the adoption forms ok.
        /// </summary>
        [Test]
        public void GetAdoptionForms_Ok()
        {
            this.Setup();

            this.adoptionFormService.Setup(c => c.GetAll(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), null, It.IsAny<AdoptionFormOrderBy>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new PagedList<AdoptionForm>(new List<AdoptionForm>().AsQueryable(), 0, 1));

            var controller = this.GetController();
            controller.AddUrl();

            var filter = new AdoptionFormFilterModel();

            var response = controller.Get(filter) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
            Assert.IsAssignableFrom(typeof(PaginationResponseModel<AdoptionFormModel>), response.Value);
        }

        /// <summary>
        /// Gets the adoption forms bad request invalid user.
        /// </summary>
        [Test]
        public void GetAdoptionForms_BadRequest_InvalidUser()
        {
            this.Setup();
            this.SetupNotAuthenticated();

            this.adoptionFormService.Setup(c => c.GetAll(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), null, It.IsAny<AdoptionFormOrderBy>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new PagedList<AdoptionForm>(new List<AdoptionForm>().AsQueryable(), 0, 1));

            var controller = this.GetController();
            controller.AddUrl();

            var filter = new AdoptionFormFilterModel();

            var response = controller.Get(filter) as ObjectResult;
            var error = response.Value as BaseApiError;

            Assert.AreEqual(400, response.StatusCode);
            Assert.AreEqual("FormUserId", error.Error.Details[0].Target);
        }

        [Test]
        public void GetAdoptionFormsById_Ok()
        {
            this.Setup();

            var form = this.GetEntity();

            this.adoptionFormService.Setup(c => c.GetById(It.IsAny<int>()))
                .Returns(form);

            this.adoptionFormService.Setup(c => c.GetAnswers(It.IsAny<int>()))
                .Returns(new List<AdoptionFormAnswer>());
            this.adoptionFormService.Setup(c => c.GetAttributes(It.IsAny<int>()))
                .Returns(new List<AdoptionFormAttribute>());

            var controller = this.GetController();
            controller.AddUrl();

            var response = controller.Get(1) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        [Test]
        public void GetAdoptionFormsById_NotFound()
        {
            this.Setup();

            this.adoptionFormService.Setup(c => c.GetById(It.IsAny<int>()))
                .Returns((AdoptionForm)null);

            var controller = this.GetController();
            controller.AddUrl();

            var response = controller.Get(1) as NotFoundResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        [Test]
        public void GetAdoptionFormsById_Forbid()
        {
            this.Setup();
            this.SetupPublicUser(55);

            this.adoptionFormService.Setup(c => c.GetById(It.IsAny<int>()))
                .Returns(this.GetEntity());

            var controller = this.GetController();
            controller.AddUrl();

            var response = controller.Get(1);

            Assert.IsAssignableFrom(typeof(ForbidResult), response);
        }

        [Test]
        public void AdoptionFormsController_ValidateQuestions_Ok()
        {
            this.Setup();

            this.customTableService.Setup(c => c.GetRowsByTableId(Convert.ToInt32(CustomTableType.QuestionAdoptionForm)))
                .Returns(this.GetQuestions());

            var attributes = this.GetAttributes();
            var controller = this.GetController();

            controller.ValidateQuestions(attributes);

            Assert.IsTrue(controller.ModelState.IsValid);
        }

        /// <summary>
        /// Adoptions the forms controller validate questions invalid required.
        /// </summary>
        [Test]
        public void AdoptionFormsController_ValidateQuestions_InvalidRequired()
        {
            this.Setup();

            this.customTableService.Setup(c => c.GetRowsByTableId(Convert.ToInt32(CustomTableType.QuestionAdoptionForm)))
                .Returns(this.GetQuestions());

            var attributes = this.GetAttributes();
            //var question = attributes.ElementAt(attributes.Count - 1).Question;
            attributes.RemoveAt(attributes.Count - 1);

            var controller = this.GetController();

            controller.ValidateQuestions(attributes);

            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.IsNotNull(controller.ModelState["Attributes"]);
        }

        [Test]
        public void AdoptionFormsController_ValidateQuestions_WithoutQuestion_NoRequired_True()
        {
            this.Setup();

            var questions = this.GetQuestions();
            questions.Add(new CustomTableRow { Id = 5, Value = "question5", AdditionalInfo = "Single|a,b,c|False" });

            this.customTableService.Setup(c => c.GetRowsByTableId(Convert.ToInt32(CustomTableType.QuestionAdoptionForm)))
                .Returns(questions);

            var attributes = this.GetAttributes();

            var controller = this.GetController();

            controller.ValidateQuestions(attributes);

            Assert.IsTrue(controller.ModelState.IsValid);
        }

        [Test]
        public void AdoptionFormsController_IsValidModel_True()
        {
            this.Setup();

            this.customTableService.Setup(c => c.GetRowsByTableId(Convert.ToInt32(CustomTableType.QuestionAdoptionForm)))
                .Returns(this.GetQuestions());

            var model = this.GetModel();
            var controller = this.GetController();

            Assert.IsTrue(controller.IsValidModel(model));
        }

        [Test]
        public void AdoptionFormsController_IsValidModel_False_NoAttributes()
        {
            this.Setup();

            this.customTableService.Setup(c => c.GetRowsByTableId(Convert.ToInt32(CustomTableType.QuestionAdoptionForm)))
                .Returns(this.GetQuestions());

            var model = this.GetModel();
            model.Attributes = null;
            var controller = this.GetController();

            Assert.IsFalse(controller.IsValidModel(model));
            Assert.IsNotNull(controller.ModelState["Attributes"]);
        }

        [Test]
        public void AdoptionFormsController_IsValidModel_False_FamilyMembersAge()
        {
            this.Setup();

            this.customTableService.Setup(c => c.GetRowsByTableId(Convert.ToInt32(CustomTableType.QuestionAdoptionForm)))
                .Returns(this.GetQuestions());

            var model = this.GetModel();
            model.FamilyMembers = 5;

            var controller = this.GetController();

            Assert.IsFalse(controller.IsValidModel(model));
            Assert.IsNotNull(controller.ModelState["FamilyMembersAge"]);
        }

        [Test]
        public void CanSeeForm_True_Admin()
        {
            this.Setup();

            var form = this.GetEntity();
            var controller = this.GetController();

            Assert.IsTrue(controller.CanSeeForm(form));
        }

        [Test]
        public void CanSeeForm_True_FormUser()
        {
            this.Setup();
            this.SetupPublicUser(55);

            var form = this.GetEntity();
            form.UserId = 55;
            var controller = this.GetController();

            Assert.IsTrue(controller.CanSeeForm(form));
        }

        [Test]
        public void CanSeeForm_True_ContentUser()
        {
            this.Setup();
            this.SetupPublicUser(55);

            var form = this.GetEntity();
            form.Content.UserId = 55;
            var controller = this.GetController();

            Assert.IsTrue(controller.CanSeeForm(form));
        }

        [Test]
        public void CanSeeForm_True_ShelterUser()
        {
            this.Setup();
            this.SetupPublicUser(55);

            var form = this.GetEntity();
            this.contentService.Setup(c => c.GetContentAttribute<int?>(form.ContentId, ContentAttributeType.Shelter))
                .Returns(5);

            this.contentService.Setup(c => c.IsUserInContent(It.IsAny<int>(), It.IsAny<int>(), Data.Entities.Enums.ContentUserRelationType.Shelter))
                .Returns(true);

            var controller = this.GetController();

            Assert.IsTrue(controller.CanSeeForm(form));
        }

        [Test]
        public void CanSeeForm_False_ShelterNoUser()
        {
            this.Setup();
            this.SetupPublicUser(55);

            var form = this.GetEntity();
            this.contentService.Setup(c => c.GetContentAttribute<int?>(form.ContentId, ContentAttributeType.Shelter))
                .Returns(5);

            this.contentService.Setup(c => c.IsUserInContent(It.IsAny<int>(), It.IsAny<int>(), Data.Entities.Enums.ContentUserRelationType.Shelter))
                .Returns(false);

            var controller = this.GetController();

            Assert.IsFalse(controller.CanSeeForm(form));
        }

        [Test]
        public void CanSeeForm_False_NoShelter()
        {
            this.Setup();
            this.SetupPublicUser(55);

            var form = this.GetEntity();
            this.contentService.Setup(c => c.GetContentAttribute<int?>(form.ContentId, ContentAttributeType.Shelter))
                .Returns((int?)null);

            var controller = this.GetController();

            Assert.IsFalse(controller.CanSeeForm(form));
        }

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

        //private IList<AdoptionFormQuestionModel> GetQuestions()
        //{
        //    var questions = new List<AdoptionFormQuestionModel>();

        //    questions.Add(new AdoptionFormQuestionModel { Id = 1, Question = "question1", Options = new string[] { "a", "b", "c" }, Required = true });
        //    questions.Add(new AdoptionFormQuestionModel { Id = 2, Question = "question2", Options = new string[] { "a", "b", "c" }, Required = true });
        //    questions.Add(new AdoptionFormQuestionModel { Id = 3, Question = "question3", Options = new string[] { "a", "b", "c" }, Required = true });
        //    questions.Add(new AdoptionFormQuestionModel { Id = 4, Question = "question4", Options = new string[] { "a", "b", "c" }, Required = true });
        //    questions.Add(new AdoptionFormQuestionModel { Id = 5, Question = "question5", Options = new string[] { "a", "b", "c" }, Required = true });

        //    return questions;
        //}
        private IList<CustomTableRow> GetQuestions()
        {
            var questions = new List<CustomTableRow>();

            questions.Add(new CustomTableRow { Id = 1, Value = "question1", AdditionalInfo = "Single|a,b,c|True" });
            questions.Add(new CustomTableRow { Id = 2, Value = "question2", AdditionalInfo = "Single|a,b,c|True" });
            questions.Add(new CustomTableRow { Id = 3, Value = "question3", AdditionalInfo = "Single|a,b,c|True" });
            questions.Add(new CustomTableRow { Id = 4, Value = "question4", AdditionalInfo = "Single|a,b,c|True" });
            questions.Add(new CustomTableRow { Id = 5, Value = "question5", AdditionalInfo = "Single|a,b,c|True" });

            return questions;
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
                LastStatusEnum = AdoptionFormAnswerStatus.None,
                LocationId = 1,
                Name = "Username",
                Town = "Barrio",
                UserId = 1,
                Content = new Content() { UserId = 1 }
            };
        }

        /// <summary>
        /// Gets the model
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
                Job = new ContentAttributeModel<int> { Text = "Job", Value = 1  },
                Status = AdoptionFormAnswerStatus.None,
                Location = new Huellitas.Web.Models.Api.Common.LocationModel { Id = 1, Name = "location" },
                Name = "Username del usuario",
                Town = "Barrio",
                User = new Huellitas.Web.Models.Api.Users.BaseUserModel { Id =1, Name = "user" },
                PhoneNumber = "3669223"
            };
        }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        /// <returns>the controller</returns>
        private AdoptionFormsController GetController()
        {
            return new AdoptionFormsController(
                this.adoptionFormService.Object,
                this.workContext.Object,
                this.contentService.Object,
                this.filesHelper.Object,
                this.customTableService.Object);
        }
    }
}
