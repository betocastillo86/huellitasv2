using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Huellitas.Business.Caching;
using Huellitas.Business.Configuration;
using Huellitas.Business.Services.Common;
using Huellitas.Business.Services.Contents;
using Huellitas.Business.Services.Files;
using Huellitas.Data.Entities;
using Huellitas.Data.Infraestructure;
using Huellitas.Tests.Web.Mocks;
using Huellitas.Web.Controllers.Api.Common;
using Huellitas.Web.Controllers.Api.Contents;
using Huellitas.Web.Models.Api.Contents;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Huellitas.Tests.Web.ApiControllers.Contents
{
    [TestFixture]
    public class RelatedContentsControllerTest : BaseTest
    {
        private Mock<IContentService> contentService = new Mock<IContentService>();

        private Mock<ICustomTableService> customTableService = new Mock<ICustomTableService>();

        private Mock<IFilesHelper> filesHelper = new Mock<IFilesHelper>();

        private Mock<IContentSettings> contentSettings = new Mock<IContentSettings>();

        [Test]
        public void GetRelatedContents_AsContentType_Ok()
        {
            var filter = new RelatedContentFilterModel();
            filter.RelationType = Data.Entities.Enums.RelationType.SimilarPets;
            filter.AsContentType = true;

            this.contentService
                .Setup(c => c.GetRelated(It.IsAny<int>(), filter.RelationType, It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new PagedList<Content> { });

            var controller = this.GetController();
            controller.AddUrl();
            controller.AddResponse();
            var response = controller.Get(1, filter) as ObjectResult;
            
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsAssignableFrom<PaginationResponseModel<PetModel>>(response.Value);
        }

        [Test]
        public void GetRelatedContents_AsContentType_False_Ok()
        {
            var filter = new RelatedContentFilterModel();
            filter.RelationType = Data.Entities.Enums.RelationType.SimilarPets;
            filter.AsContentType = false;

            this.contentService
                .Setup(c => c.GetRelated(It.IsAny<int>(), filter.RelationType, It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new PagedList<Content> { });

            var controller = this.GetController();
            controller.AddUrl();
            controller.AddResponse();
            var response = controller.Get(1, filter) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
            Assert.IsAssignableFrom<PaginationResponseModel<ContentModel>>(response.Value);
        }

        [Test]
        public void GetRelatedContents_BadRequest_Filter()
        {
            var filter = new RelatedContentFilterModel();
            filter.PageSize = 10000;

            var controller = this.GetController();

            var response = controller.Get(1, filter) as ObjectResult;

            Assert.AreEqual(400, response.StatusCode);
        }

        private RelatedContentsController GetController()
        {
            return new RelatedContentsController(
                this.contentService.Object,
                this.customTableService.Object,
                this.cacheManager.Object,
                this.filesHelper.Object,
                this.contentSettings.Object);
        }
    }
}
