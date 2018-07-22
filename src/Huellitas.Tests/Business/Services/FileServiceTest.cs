//-----------------------------------------------------------------------
// <copyright file="FileServiceTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Business.Services
{
    using Beto.Core.EventPublisher;
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Services;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;
    using Huellitas.Tests.Helpers;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// File Service Test
    /// </summary>
    [TestFixture]
    public class FileServiceTest
    {
        /// <summary>
        /// The content file repository
        /// </summary>
        private Mock<IRepository<ContentFile>> contentFileRepository = new Mock<IRepository<ContentFile>>();

        /// <summary>
        /// The file repository
        /// </summary>
        private Mock<IRepository<File>> fileRepository = new Mock<IRepository<File>>();

        /// <summary>
        /// The files helper/
        /// </summary>
        private Mock<IFilesHelper> filesHelper = new Mock<IFilesHelper>();

        private Mock<IPublisher> publisher = new Mock<IPublisher>();

        private Mock<IRepository<Content>> contentRepository = new Mock<IRepository<Content>>();

        /// <summary>
        /// Inserts the content file with invalid file.
        /// </summary>
        [Test]
        public void InsertContentFile_With_Invalid_File()
        {
            var mockException = SqlExceptionHelper.GetDbUpdateExceptionWithSqlException(547, "FK_ContentFile_File");

            this.contentFileRepository.Setup(c => c.InsertAsync(It.IsAny<ContentFile>()))
                .Throws(mockException);

            var service = this.GetService();

            var ex = Assert.ThrowsAsync<HuellitasException>(async () => await service.InsertContentFileAsync(new ContentFile()));
            Assert.AreEqual("File", ex.Target);
            Assert.AreEqual(HuellitasExceptionCode.InvalidForeignKey, ex.Code);
        }

        /// <summary>
        /// Inserts the content file with invalid file.
        /// </summary>
        [Test]
        public void InsertContentFile_With_Invalid_Content()
        {
            var mockException = SqlExceptionHelper.GetDbUpdateExceptionWithSqlException(547, "FK_ContentFile_Content");

            this.contentFileRepository.Setup(c => c.InsertAsync(It.IsAny<ContentFile>()))
                .Throws(mockException);

            var service = this.GetService();

            var ex = Assert.ThrowsAsync<HuellitasException>(async () => await service.InsertContentFileAsync(new ContentFile()));
            Assert.AreEqual("Content", ex.Target);
            Assert.AreEqual(HuellitasExceptionCode.InvalidForeignKey, ex.Code);
        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <returns>the service</returns>
        private FileService GetService()
        {
            return new FileService(
                this.fileRepository.Object,
                this.filesHelper.Object,
                this.contentFileRepository.Object,
                this.publisher.Object,
                this.contentRepository.Object);
        }
    }
}