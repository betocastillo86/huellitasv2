//-----------------------------------------------------------------------
// <copyright file="SeoServiceTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Business.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Business.Configuration;
    using Huellitas.Business.Services;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// <c>Seo</c> Service Test
    /// </summary>
    [TestFixture]
    public class SeoServiceTest : BaseTest
    {
        /// <summary>
        /// Generates the name of the correct friendly.
        /// </summary>
        [Test]
        public void GenerateCorrectFriendlyName()
        {
            this.Setup();

            var contentRepository = new Mock<IRepository<Content>>();

            var service = new SeoService(this.generalSettings.Object, contentRepository.Object, this.logService.Object);
            var contents = new List<Content>().AsQueryable();
            Assert.AreEqual("el-primer-contenido", service.GenerateFriendlyName("El primer contenido", contents));
            Assert.AreEqual("con-punto", service.GenerateFriendlyName("Con punto.", contents));
            Assert.AreEqual("con-coma", service.GenerateFriendlyName("Con coma,", contents));
            Assert.AreEqual("con-ene", service.GenerateFriendlyName("Con eñe", contents));
            Assert.AreEqual("con-las-tildes-aeiou", service.GenerateFriendlyName("Con las tildes áéíóú", contents));
            Assert.AreEqual("con-especiales", service.GenerateFriendlyName("Con Especiales{}'/()<>#$&!*", contents));
            Assert.AreEqual("con-espacios", service.GenerateFriendlyName("Con      Espacios", contents));
            Assert.AreEqual("con-espacio-final", service.GenerateFriendlyName("Con      Espacio final ", contents));
            Assert.AreEqual("con-espacio-inicial", service.GenerateFriendlyName(" con espacio inicial", contents));
        }

        /// <summary>
        /// Generates the friendly name with the query null.
        /// </summary>
        [Test]
        public void GenerateFriendlyName_Query_Null()
        {
            this.Setup();
            var contentRepository = new Mock<IRepository<Content>>();
            var service = new SeoService(this.generalSettings.Object, contentRepository.Object, this.logService.Object);
            Assert.AreEqual("el-primer-contenido", service.GenerateFriendlyName("El primer contenido", null));
            Assert.AreEqual("con-punto", service.GenerateFriendlyName("Con punto.", null));
            Assert.AreEqual("con-coma", service.GenerateFriendlyName("Con coma,", null));
        }

        /// <summary>
        /// Generates the maximum length of the repeated with.
        /// </summary>
        [Test]
        public void GenerateRepeatedWithMaxLength()
        {
            this.Setup();
            var contentRepository = new Mock<IRepository<Content>>();
            var service = new SeoService(this.generalSettings.Object, contentRepository.Object, this.logService.Object);
            var contents = new List<Content>().AsQueryable();
            Assert.AreEqual("el-primer", service.GenerateFriendlyName("El primer contenido", contents, 10));
        }

        /// <summary>
        /// Generates the repeated with maximum length and finish word.
        /// </summary>
        [Test]
        public void GenerateRepeatedWithMaxLengthAndFinishWord()
        {
            this.Setup();
            var contentRepository = new Mock<IRepository<Content>>();
            var service = new SeoService(this.generalSettings.Object, contentRepository.Object, this.logService.Object);
            var contents = new List<Content>().AsQueryable();
            Assert.AreEqual("el-primer-contenido", service.GenerateFriendlyName("El primer contenido con palabra final", contents, 13));
            Assert.AreEqual("el-primer-si", service.GenerateFriendlyName("El primer si contenido con palabra final", contents, 13));
        }

        /// <summary>
        /// Generates the maximum length of the with.
        /// </summary>
        [Test]
        public void GenerateWithMaxLength()
        {
            this.Setup();
            var contentRepository = new Mock<IRepository<Content>>();
            var service = new SeoService(this.generalSettings.Object, contentRepository.Object, this.logService.Object);
            var contents = new List<Content>();
            contents.Add(new Content() { Name = "El primer contenido", FriendlyName = "el-primer" });
            contents.Add(new Content() { Name = "El segundo contenido", FriendlyName = "el-segundo" });

            var url = service.GenerateFriendlyName("El primer contenido", contents.AsQueryable(), 10);
            Assert.AreNotEqual("el-primer", url);
            Assert.IsTrue(url.Contains("el-primer"));
        }

        /// <summary>
        /// Generates the with no repeated url.
        /// </summary>
        [Test]
        public void GenerateWithNoRepeatedUrls()
        {
            this.Setup();
            var contentRepository = new Mock<IRepository<Content>>();
            var service = new SeoService(this.generalSettings.Object, contentRepository.Object, this.logService.Object);
            var contents = new List<Content>();
            contents.Add(new Content() { Name = "El primer contenido", FriendlyName = "el-primer-contenido" });
            contents.Add(new Content() { Name = "El segundo contenido", FriendlyName = "el-segundo-contenido" });

            var generatedUrl = service.GenerateFriendlyName("El primer contenido", contents.AsQueryable());
            Assert.AreNotEqual("el-primer-contenido", generatedUrl);
            Assert.IsTrue(generatedUrl.Contains("el-primer-contenido"));

            generatedUrl = service.GenerateFriendlyName("El segundo *}{contenido", contents.AsQueryable());
            Assert.AreNotEqual("el-segundo-contenido", generatedUrl);
            Assert.IsTrue(generatedUrl.Contains("el-segundo-contenido"));

            generatedUrl = service.GenerateFriendlyName("El tercer contenido", contents.AsQueryable());
            Assert.AreEqual("el-tercer-contenido", generatedUrl);
        }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        protected override void Setup()
        {
            this.generalSettings = new Mock<IGeneralSettings>();
            base.Setup();
        }
    }
}