//-----------------------------------------------------------------------
// <copyright file="RelatedContentsControllerTest.cs" company="Huellitas Sin Hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Contents
{
    using Huellitas.Business.Configuration;
    using Huellitas.Business.Services;
    using Huellitas.Business.Services;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Infraestructure;
    using Huellitas.Tests.Web.Mocks;
    using Huellitas.Web.Controllers.Api;
    using Huellitas.Web.Controllers.Api;
    using Huellitas.Web.Models.Api;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Related Contents Controller Test
    /// </summary>
    /// <seealso cref="Huellitas.Tests.BaseTest" />
    [TestFixture]
    public class RelatedContentsControllerTest : BaseTest
    {
        /// <summary>
        /// The custom table service
        /// </summary>
        private Mock<ICustomTableService> customTableService = new Mock<ICustomTableService>();

        /// <summary>
        /// The files helper
        /// </summary>
        private Mock<IFilesHelper> filesHelper = new Mock<IFilesHelper>();

        /// <summary>
        /// Gets the related contents as content type false ok.
        /// </summary>
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

        /// <summary>
        /// Gets the related contents as content type ok.
        /// </summary>
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

        /// <summary>
        /// Gets the related contents bad request filter.
        /// </summary>
        [Test]
        public void GetRelatedContents_BadRequest_Filter()
        {
            var filter = new RelatedContentFilterModel();
            filter.PageSize = 10000;

            var controller = this.GetController();

            var response = controller.Get(1, filter) as ObjectResult;

            Assert.AreEqual(400, response.StatusCode);
        }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        /// <returns>the controller</returns>
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