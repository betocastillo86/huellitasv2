//-----------------------------------------------------------------------
// <copyright file="AdoptionFormsControllerTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.AdoptionForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Huellitas.Business.Exceptions;
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
    using Huellitas.Web.Models.Api.Common;
    using Huellitas.Web.Models.Api.Contents;
    using Huellitas.Web.Models.Extensions.AdoptionForms;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Adoption Forms Controller Test
    /// </summary>
    /// <seealso cref="Huellitas.Tests.BaseTest" />
    [TestFixture]
    public class AdoptionFormsControllerTest : BaseTest
    {
        /// <summary>
        /// The adoption form service
        /// </summary>
        private Mock<IAdoptionFormService> adoptionFormService = new Mock<IAdoptionFormService>();

        /// <summary>
        /// The content service
        /// </summary>
        private Mock<IContentService> contentService = new Mock<IContentService>();

        /// <summary>
        /// The custom table service
        /// </summary>
        private Mock<ICustomTableService> customTableService = new Mock<ICustomTableService>();

        /// <summary>
        /// The files helper
        /// </summary>
        private Mock<IFilesHelper> filesHelper = new Mock<IFilesHelper>();

        /// <summary>
        /// Adoptions the forms controller is valid model false family members age.
        /// </summary>
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

        /// <summary>
        /// Adoptions the forms controller is valid model false no attributes.
        /// </summary>
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

        /// <summary>
        /// Adoptions the forms controller is valid model true.
        /// </summary>
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

            attributes.RemoveAt(attributes.Count - 1);

            var controller = this.GetController();

            controller.ValidateQuestions(attributes);

            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.IsNotNull(controller.ModelState["Attributes"]);
        }

        /// <summary>
        /// Adoptions the forms controller validate questions ok.
        /// </summary>
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
        /// Adoptions the forms controller validate questions without question no required true.
        /// </summary>
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

        /// <summary>
        /// Determines whether this instance [can see form false no shelter].
        /// </summary>
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

        /// <summary>
        /// Determines whether this instance [can see form false shelter no user].
        /// </summary>
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

        /// <summary>
        /// Determines whether this instance [can see form true admin].
        /// </summary>
        [Test]
        public void CanSeeForm_True_Admin()
        {
            this.Setup();

            var form = this.GetEntity();
            var controller = this.GetController();

            Assert.IsTrue(controller.CanSeeForm(form));
        }

        /// <summary>
        /// Determines whether this instance [can see form true content user].
        /// </summary>
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

        /// <summary>
        /// Determines whether this instance [can see form true user in adoption form].
        /// </summary>
        [Test]
        public void CanSeeForm_True_UserInAdoptionForm()
        {
            var userId = 55;

            this.Setup();
            this.SetupPublicUser(userId);

            var form = this.GetEntity();

            this.adoptionFormService.Setup(c => c.IsUserInAdoptionForm(userId, form.Id))
                .Returns(true);
            
            var controller = this.GetController();

            Assert.IsTrue(controller.CanSeeForm(form));
        }

        /// <summary>
        /// Determines whether this instance [can see form true user is parent].
        /// </summary>
        [Test]
        public void CanSeeForm_True_UserIsParent()
        {
            var userId = 55;

            this.Setup();
            this.SetupPublicUser(userId);

            var form = this.GetEntity();

            this.contentService.Setup(c => c.IsUserInContent(userId, form.ContentId, Data.Entities.Enums.ContentUserRelationType.Parent))
                .Returns(true);

            var controller = this.GetController();

            Assert.IsTrue(controller.CanSeeForm(form));
        }

        /// <summary>
        /// Determines whether this instance [can see form true form user].
        /// </summary>
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

        /// <summary>
        /// Determines whether this instance [can see form true shelter user].
        /// </summary>
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
        public void CanSeeForm_True_AdoptionFormUser()
        {
            this.Setup();
            this.SetupPublicUser(55);

            var form = this.GetEntity();

            this.adoptionFormService.Setup(c => c.IsUserInAdoptionForm(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            var controller = this.GetController();

            Assert.IsTrue(controller.CanSeeForm(form));
        }

        /// <summary>
        /// Gets the adoption forms bad request invalid user.
        /// </summary>
        [Test]
        public void GetAdoptionForms_BadRequest_InvalidUser()
        {
            this.Setup();
            this.SetupNotAuthenticated();

            this.adoptionFormService.Setup(c => c.GetAll(
                It.IsAny<string>(), 
                It.IsAny<int?>(), 
                It.IsAny<int?>(), 
                It.IsAny<int?>(), 
                It.IsAny<int?>(), 
                It.IsAny<int?>(),
                It.IsAny<int?>(),
                It.IsAny<int?>(),
                It.IsAny<int?>(),
                It.IsAny<AdoptionFormAnswerStatus>(),
                It.IsAny<AdoptionFormOrderBy>(), 
                It.IsAny<int>(), 
                It.IsAny<int>()))
                .Returns(new PagedList<AdoptionForm>(new List<AdoptionForm>().AsQueryable(), 0, 1));

            var controller = this.GetController();
            controller.AddUrl();

            var filter = new AdoptionFormFilterModel();

            var response = controller.Get(filter) as ObjectResult;
            var error = response.Value as BaseApiError;

            Assert.AreEqual(400, response.StatusCode);
            Assert.AreEqual("FormUserId", error.Error.Details[0].Target);
        }

        /// <summary>
        /// Gets the adoption forms ok.
        /// </summary>
        [Test]
        public void GetAdoptionForms_Ok()
        {
            this.Setup();

            this.adoptionFormService.Setup(c => c.GetAll(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<AdoptionFormAnswerStatus?>(), It.IsAny<AdoptionFormOrderBy>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new PagedList<AdoptionForm>(new List<AdoptionForm>().AsQueryable(), 0, 1));

            var controller = this.GetController();
            controller.AddUrl();

            var filter = new AdoptionFormFilterModel();

            var response = controller.Get(filter) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
            Assert.IsAssignableFrom(typeof(PaginationResponseModel<AdoptionFormModel>), response.Value);
        }

        /// <summary>
        /// Gets the adoption forms by identifier forbid.
        /// </summary>
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

        /// <summary>
        /// Gets the adoption forms by identifier not found.
        /// </summary>
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

        /// <summary>
        /// Gets the adoption forms by identifier ok.
        /// </summary>
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

        /// <summary>
        /// Posts the adoption form bad request invalid model.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostAdoptionForm_BadRequest_InvalidModel()
        {
            this.Setup();

            var model = this.GetModel();
            model.Attributes = null;

            this.adoptionFormService.Setup(c => c.Insert(It.IsAny<AdoptionForm>()))
                .Callback((AdoptionForm c) =>
                {
                    c.Id = 2;
                });

            var controller = this.GetController();
            controller.AddUrl();
            controller.AddResponse();

            var result = await controller.Post(model) as ObjectResult;

            Assert.AreEqual(400, result.StatusCode);
        }

        /// <summary>
        /// Posts the adoption form bad request no location.
        /// </summary>
        /// <returns>the action</returns>
        [Test]
        public async Task PostAdoptionForm_BadRequest_NoLocation()
        {
            this.Setup();

            var model = this.GetModel();

            this.customTableService.Setup(c => c.GetRowsByTableId(Convert.ToInt32(CustomTableType.QuestionAdoptionForm)))
                .Returns(this.GetQuestions());

            this.adoptionFormService.Setup(c => c.Insert(It.IsAny<AdoptionForm>()))
                .Throws(new HuellitasException("Location", HuellitasExceptionCode.InvalidForeignKey));

            var controller = this.GetController();
            controller.AddUrl();
            controller.AddResponse();

            var result = await controller.Post(model) as ObjectResult;
            var error = result.Value as BaseApiError;

            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual(HuellitasExceptionCode.InvalidForeignKey.ToString(), error.Error.Code);
            Assert.AreEqual("Location", error.Error.Target);
        }

        /// <summary>
        /// Posts the adoption form ok.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PostAdoptionForm_Ok()
        {
            this.Setup();

            var model = this.GetModel();

            this.customTableService.Setup(c => c.GetRowsByTableId(Convert.ToInt32(CustomTableType.QuestionAdoptionForm)))
                .Returns(this.GetQuestions());

            this.adoptionFormService.Setup(c => c.Insert(It.IsAny<AdoptionForm>()))
                .Callback((AdoptionForm c) =>
                {
                    c.Id = 2;
                })
                .Returns(Task.FromResult(0));

            var controller = this.GetController();
            controller.AddUrl(true);
            controller.AddResponse();

            var result = await controller.Post(model) as ObjectResult;

            Assert.AreEqual(201, result.StatusCode);
            Assert.IsAssignableFrom(typeof(BaseModel), result.Value);
        }

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
                Job = new ContentAttributeModel<int> { Text = "Job", Value = 1 },
                Status = AdoptionFormAnswerStatus.None,
                Location = new Huellitas.Web.Models.Api.Common.LocationModel { Id = 1, Name = "location" },
                Name = "Username del usuario",
                Town = "Barrio",
                User = new Huellitas.Web.Models.Api.Users.BaseUserModel { Id = 1, Name = "user" },
                PhoneNumber = "3669223"
            };
        }

        /// <summary>
        /// Gets the questions.
        /// </summary>
        /// <returns>the list</returns>
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
    }
}