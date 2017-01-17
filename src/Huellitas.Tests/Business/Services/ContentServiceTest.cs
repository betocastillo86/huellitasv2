//-----------------------------------------------------------------------
// <copyright file="ContentServiceTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Business
{
    using Huellitas.Business.EventPublisher;
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
            mockContentRepository.Setup(c => c.InsertAsync(It.IsAny<Content>()))
                .Throws(mockException);

            var contentService = this.MockContentService(contentRepository: mockContentRepository.Object);

            var ex = Assert.ThrowsAsync<HuellitasException>(async () => await contentService.InsertAsync(new Content()));
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
            mockContentRepository.Setup(c => c.InsertAsync(It.IsAny<Content>()))
                .Throws(mockException);

            var contentService = this.MockContentService(contentRepository: mockContentRepository.Object);

            var ex = Assert.ThrowsAsync<HuellitasException>(async () => await contentService.InsertAsync(new Content()));
            Assert.AreEqual("Location", ex.Target);
            Assert.AreEqual(HuellitasExceptionCode.InvalidForeignKey, ex.Code);
        }

        /// <summary>
        /// Inserts the content with repeated location.
        /// </summary>
        [Test]
        public void InsertContent_With_Invalid_User()
        {
            var mockException = SqlExceptionHelper.GetDbUpdateExceptionWithSqlException(547, "FK_Content_User");

            var mockContentRepository = new Mock<IRepository<Content>>();
            mockContentRepository.Setup(c => c.InsertAsync(It.IsAny<Content>()))
                .Throws(mockException);

            var contentService = this.MockContentService(contentRepository: mockContentRepository.Object);

            var ex = Assert.ThrowsAsync<HuellitasException>(async () => await contentService.InsertAsync(new Content()));
            Assert.AreEqual("User", ex.Target);
            Assert.AreEqual(HuellitasExceptionCode.InvalidForeignKey, ex.Code);
        }

        /// <summary>
        /// Updates the content with invalid file.
        /// </summary>
        [Test]
        public void UpdateContent_With_Invalid_File()
        {
            var mockException = SqlExceptionHelper.GetDbUpdateExceptionWithSqlException(547, "FK_Content_File FK_ContentFile_File");

            var mockContentRepository = new Mock<IRepository<Content>>();
            mockContentRepository.Setup(c => c.UpdateAsync(It.IsAny<Content>()))
                .Throws(mockException);

            var contentService = this.MockContentService(contentRepository: mockContentRepository.Object);

            var ex = Assert.ThrowsAsync<HuellitasException>(async () => await contentService.UpdateAsync(new Content()));
            Assert.AreEqual("File", ex.Target);
            Assert.AreEqual(HuellitasExceptionCode.InvalidForeignKey, ex.Code);
        }

        /// <summary>
        /// Updates the content with invalid location.
        /// </summary>
        [Test]
        public void UpdateContent_With_Invalid_Location()
        {
            var mockException = SqlExceptionHelper.GetDbUpdateExceptionWithSqlException(547, "FK_Content_Location");

            var mockContentRepository = new Mock<IRepository<Content>>();
            mockContentRepository.Setup(c => c.UpdateAsync(It.IsAny<Content>()))
                .Throws(mockException);

            var contentService = this.MockContentService(contentRepository: mockContentRepository.Object);

            var ex = Assert.ThrowsAsync<HuellitasException>(async () => await contentService.UpdateAsync(new Content()));
            Assert.AreEqual("Location", ex.Target);
            Assert.AreEqual(HuellitasExceptionCode.InvalidForeignKey, ex.Code);
        }

        /// <summary>
        /// Updates the content with invalid user.
        /// </summary>
        [Test]
        public void UpdateContent_With_Invalid_User()
        {
            var mockException = SqlExceptionHelper.GetDbUpdateExceptionWithSqlException(547, "FK_Content_User");

            var mockContentRepository = new Mock<IRepository<Content>>();
            mockContentRepository.Setup(c => c.UpdateAsync(It.IsAny<Content>()))
                .Throws(mockException);

            var contentService = this.MockContentService(contentRepository: mockContentRepository.Object);

            var ex = Assert.ThrowsAsync<HuellitasException>(async () => await contentService.UpdateAsync(new Content()));
            Assert.AreEqual("User", ex.Target);
            Assert.AreEqual(HuellitasExceptionCode.InvalidForeignKey, ex.Code);
        }

        /// <summary>
        /// Mocks the content service.
        /// </summary>
        /// <param name="contentRepository">The content repository.</param>
        /// <param name="contentAttributeRepository">The content attribute repository.</param>
        /// <param name="contentFileRepository">The content file repository.</param>
        /// <param name="seoService">The SEO service.</param>
        /// <param name="context">The context.</param>
        /// <param name="relatedContentRepository">The related content repository.</param>
        /// <param name="contentUserRepository">content user repository</param>
        /// <returns>the mock</returns>
        private ContentService MockContentService(
            IRepository<Content> contentRepository = null,
            IRepository<ContentAttribute> contentAttributeRepository = null,
            IRepository<ContentFile> contentFileRepository = null,
            ISeoService seoService = null,
            HuellitasContext context = null,
            IRepository<RelatedContent> relatedContentRepository = null,
            IRepository<ContentUser> contentUserRepository = null,
            IPublisher publisher = null)
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

            if (relatedContentRepository == null)
            {
                relatedContentRepository = new Mock<IRepository<RelatedContent>>().Object;
            }

            if (seoService == null)
            {
                seoService = new Mock<ISeoService>().Object;
            }

            if (context == null)
            {
                context = HuellitasContextHelpers.GetHuellitasContext();
            }

            if (contentUserRepository == null)
            {
                contentUserRepository = new Mock<IRepository<ContentUser>>().Object;
            }

            if (publisher == null)
            {
                publisher = new Mock<IPublisher>().Object;
            }

            return new ContentService(contentRepository, contentAttributeRepository, contentFileRepository, seoService, context, relatedContentRepository, contentUserRepository, publisher);
        }
    }
}