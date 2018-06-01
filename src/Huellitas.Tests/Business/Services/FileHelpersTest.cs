//-----------------------------------------------------------------------
// <copyright file="FileHelpersTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Business.Services
{
    using System;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;
    using Microsoft.AspNetCore.Hosting;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// File Helpers Test
    /// </summary>
    [TestFixture]
    public class FileHelpersTest : BaseTest
    {
        /// <summary>
        /// Gets the folder name default files by folder.
        /// </summary>
        [Test]
        public void GetFolderName_DefaultFilesByFolder()
        {
            var mockHosting = new Mock<IHostingEnvironment>();

            var fileHelper = new FilesHelper(mockHosting.Object, this.generalSettings.Object);
            Assert.AreEqual("000001", fileHelper.GetFolderName(new File() { Id = 1 }));
            Assert.AreEqual("000002", fileHelper.GetFolderName(new File() { Id = 51 }));
            Assert.AreEqual("000003", fileHelper.GetFolderName(new File() { Id = 101 }));
            Assert.AreEqual("000027", fileHelper.GetFolderName(new File() { Id = 1325 }));
        }

        /// <summary>
        /// Gets the folder name different files by folder.
        /// </summary>
        [Test]
        public void GetFolderName_DifferentFilesByFolder()
        {
            var mockHosting = new Mock<IHostingEnvironment>();

            var fileHelper = new FilesHelper(mockHosting.Object, this.generalSettings.Object);
            Assert.AreEqual("000001", fileHelper.GetFolderName(new File() { Id = 1 }, 200));
            Assert.AreEqual("000001", fileHelper.GetFolderName(new File() { Id = 51 }, 200));
            Assert.AreEqual("000001", fileHelper.GetFolderName(new File() { Id = 101 }, 200));
            Assert.AreEqual("000007", fileHelper.GetFolderName(new File() { Id = 1325 }, 200));

            Assert.AreEqual("000001", fileHelper.GetFolderName(new File() { Id = 1 }, 20));
            Assert.AreEqual("000003", fileHelper.GetFolderName(new File() { Id = 51 }, 20));
            Assert.AreEqual("000006", fileHelper.GetFolderName(new File() { Id = 101 }, 20));
            Assert.AreEqual("000067", fileHelper.GetFolderName(new File() { Id = 1325 }, 20));

            Assert.AreEqual("000001", fileHelper.GetFolderName(new File() { Id = 1 }, 8));
            Assert.AreEqual("000007", fileHelper.GetFolderName(new File() { Id = 51 }, 8));
            Assert.AreEqual("000013", fileHelper.GetFolderName(new File() { Id = 101 }, 8));
            Assert.AreEqual("000166", fileHelper.GetFolderName(new File() { Id = 1325 }, 8));
        }

        /// <summary>
        /// gets physical path.
        /// </summary>
        [Test]
        public void FileHelpers_GetPhysicalPath()
        {
            var rootPath = @"c:\";
            var mockHosting = new Mock<IHostingEnvironment>();
            mockHosting.SetupGet(c => c.WebRootPath).Returns(rootPath);

            var fileHelper = new FilesHelper(mockHosting.Object, this.generalSettings.Object);
            var file = new File();
            file.Id = 1;
            file.FileName = "elarchivo.jpg";

            var newPath = fileHelper.GetPhysicalPath(file, 0, 0);
            var expected = $@"{rootPath}/img/content/000001/1.jpg";

            Assert.AreEqual(newPath, expected);
        }

        /// <summary>
        /// Gets file name with.
        /// </summary>
        [Test]
        public void FileHelpers_GetFileNameWithSize()
        {
            var mockHosting = new Mock<IHostingEnvironment>();
            var fileHelper = new FilesHelper(mockHosting.Object, this.generalSettings.Object);

            var file = new File();
            file.Id = 1;
            file.FileName = "elarchivo.jpg";

            var name = fileHelper.GetFileNameWithSize(file);
            Assert.AreEqual("1.jpg", name);

            name = fileHelper.GetFileNameWithSize(file, 100, 150);
            Assert.AreEqual("1_elarchivo_100x150.jpg", name);

            name = fileHelper.GetFileNameWithSize(file, 100);
            Assert.AreEqual("1.jpg", name);

            name = fileHelper.GetFileNameWithSize(file, 0, 100);
            Assert.AreEqual("1.jpg", name);
        }

        /// <summary>
        /// Gets full path.
        /// </summary>
        [Test]
        public void FileHelpers_GetFullPath()
        {
            var mockHosting = new Mock<IHostingEnvironment>();
            var fileHelper = new FilesHelper(mockHosting.Object, this.generalSettings.Object);

            var file = new File();
            file.Id = 1;
            file.FileName = "elarchivo.jpg";

            var name = fileHelper.GetFullPath(file, this.ContentUrlFunction, 0, 0);
            var expected = "ELPATH->/img/content/000001/1.jpg";
            Assert.AreEqual(expected, name);

            name = fileHelper.GetFullPath(file, this.ContentUrlFunction, 10, 15);
            expected = "ELPATH->/img/content/000001/1_elarchivo_10x15.jpg";
            Assert.AreEqual(expected, name);

            name = fileHelper.GetFullPath(file, null, 0, 0);
            expected = "/img/content/000001/1.jpg";
            Assert.AreEqual(expected, name);
        }

        /// <summary>
        /// Simulates the content url function
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>the url modified</returns>
        private string ContentUrlFunction(string path)
        {
            return $"ELPATH->{path}";
        }
    }
}