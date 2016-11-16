//-----------------------------------------------------------------------
// <copyright file="ContentServiceTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Business
{
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Services.Contents;
    using Huellitas.Business.Services.Seo;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;
    using Huellitas.Tests.Helpers;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Content Service Test
    /// </summary>
    [TestFixture]
    public class ContentServiceTest
    {
        /// <summary>
        /// Inserts the content with invalid file.
        /// </summary>
        [Test]
        public void InsertContent_With_Invalid_File()
        {
            var mockException = SqlExceptionHelper.GetDbUpdateExceptionWithSqlException(547, "FK_Content_File");

            var mockContentRepository = new Mock<IRepository<Content>>();
            mockContentRepository.Setup(c => c.Insert(It.IsAny<Content>()))
                .Throws(mockException);

            var contentService = this.MockContentService(contentRepository: mockContentRepository.Object);

            var ex = Assert.Throws<HuellitasException>(() => contentService.Insert(new Content()));
            Assert.AreEqual("File", ex.Target);
            Assert.AreEqual(HuellitasExceptionCode.InvalidForeignKey, ex.Code);
        }

        /// <summary>
        /// Inserts the content with repeated location.
        /// </summary>
        [Test]
        public void InsertContent_With_Invalid_Location()
        {
            var mockException = SqlExceptionHelper.GetDbUpdateExceptionWithSqlException(547, "FK_Content_Location");

            var mockContentRepository = new Mock<IRepository<Content>>();
            mockContentRepository.Setup(c => c.Insert(It.IsAny<Content>()))
                .Throws(mockException);

            var contentService = this.MockContentService(contentRepository: mockContentRepository.Object);

            var ex = Assert.Throws<HuellitasException>(() => contentService.Insert(new Content()));
            Assert.AreEqual("Location", ex.Target);
            Assert.AreEqual(HuellitasExceptionCode.InvalidForeignKey, ex.Code);
        }

        /// <summary>
        /// Mocks the content service.
        /// </summary>
        /// <param name="contentRepository">The content repository.</param>
        /// <param name="contentAttributeRepository">The content attribute repository.</param>
        /// <param name="contentFileRepository">The content file repository.</param>
        /// <param name="seoService">The <c>seo</c> service.</param>
        /// <param name="context">The context.</param>
        /// <returns>the service</returns>
        private ContentService MockContentService(
            IRepository<Content> contentRepository = null,
            IRepository<ContentAttribute> contentAttributeRepository = null,
            IRepository<ContentFile> contentFileRepository = null,
            ISeoService seoService = null,
            HuellitasContext context = null)
        {
            if (contentRepository == null)
            {
                contentRepository = new Mock<IRepository<Content>>().Object;
            }

            if (contentAttributeRepository == null)
            {
                contentAttributeRepository = new Mock<IRepository<ContentAttribute>>().Object;
            }

            if (contentFileRepository == null)
            {
                contentFileRepository = new Mock<IRepository<ContentFile>>().Object;
            }

            if (seoService == null)
            {
                seoService = new Mock<ISeoService>().Object;
            }

            if (context == null)
            {
                context = HuellitasContextHelpers.GetHuellitasContext();
            }

            return new ContentService(contentRepository, contentAttributeRepository, contentFileRepository, seoService, context);
        }
    }
}