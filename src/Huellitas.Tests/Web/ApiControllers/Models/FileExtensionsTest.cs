//-----------------------------------------------------------------------
// <copyright file="FileExtensionsTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Models
{
    using Huellitas.Business.Services.Files;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Extensions.Common;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// File Extensions Test
    /// </summary>
    [TestFixture]
    public class FileExtensionsTest
    {
        /// <summary>
        /// To the file model valid.
        /// </summary>
        [Test]
        public void ToFileModelValid()
        {
            var mockFileHelper = new Mock<IFilesHelper>();
            mockFileHelper.Setup(c => c.GetFullPath(It.IsAny<File>(), null, 0, 0)).Returns("thevalue");

            var file = new File();
            file.Id = 1;
            file.FileName = "fileName.jpg";
            file.Name = "The Name";
            var model = file.ToModel(mockFileHelper.Object, null);

            Assert.AreEqual(file.Name, model.Name);
            Assert.AreEqual(file.Id, model.Id);
            Assert.AreEqual("thevalue", model.FileName);
        }
    }
}