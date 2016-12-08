//-----------------------------------------------------------------------
// <copyright file="FileHelpersTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Business.Services
{
    using Huellitas.Business.Services.Files;
    using Huellitas.Data.Entities;
    using Microsoft.AspNetCore.Hosting;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// File Helpers Test
    /// </summary>
    [TestFixture]
    public class FileHelpersTest
    {
        /// <summary>
        /// Gets the folder name default files by folder.
        /// </summary>
        [Test]
        public void GetFolderName_DefaultFilesByFolder()
        {
            var mockHosting = new Mock<IHostingEnvironment>();

            var fileHelper = new FilesHelper(mockHosting.Object);
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

            var fileHelper = new FilesHelper(mockHosting.Object);
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
    }
}