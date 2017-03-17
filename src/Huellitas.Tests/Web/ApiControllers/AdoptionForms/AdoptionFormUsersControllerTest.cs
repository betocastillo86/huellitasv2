using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Huellitas.Business.Services.AdoptionForms;
using Huellitas.Business.Services.Contents;
using Huellitas.Data.Entities;
using Huellitas.Web.Controllers.Api.AdoptionForms;
using Huellitas.Web.Models.Api.AdoptionForms;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Huellitas.Tests.Web.ApiControllers.AdoptionForms
{
    [TestFixture]
    public class AdoptionFormUsersControllerTest : BaseTest
    {
        private Mock<IContentService> contentService = new Mock<IContentService>();

        private Mock<IAdoptionFormService> adoptionFormService = new Mock<IAdoptionFormService>();

        protected override void Setup()
        {
            this.contentService = new Mock<IContentService>();
            this.adoptionFormService = new Mock<IAdoptionFormService>();

            base.Setup();
        }

        [Test]
        public async Task PostAdoptionFormUser_Ok()
        {
            var model = this.GetModel();

            this.adoptionFormService.Setup(c => c.GetById(1))
                .Returns(this.GetAdoptionForm());

            this.adoptionFormService.Setup(c => c.InsertUser(It.IsAny<AdoptionFormUser>()))
                .Callback((AdoptionFormUser c) => { c.Id = 1; })
                .Returns(Task.FromResult(0));
            
            var controller = this.GetController();

            var response = await controller.Post(1, model) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        private AdoptionFormUserModel GetModel()
        {
            return new AdoptionFormUserModel() {
                UserId = 55
            };
        }

        private AdoptionForm GetAdoptionForm()
        {
            return new AdoptionForm()
            {
                Id = 1,
                Address = "cr 10 10 10",
                Attributes = new List<AdoptionFormAttribute>() { },
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

        private AdoptionFormUsersController GetController()
        {
            return new AdoptionFormUsersController(
                this.workContext.Object,
                this.contentService.Object,
                this.adoptionFormService.Object);
        }
    }
}
