namespace Huellitas.Tests.Web.ApiControllers.Contents
{
    using System;
    using System.Linq;
    using Huellitas.Web.Models.Api.Contents;
    using NUnit.Framework;

    [TestFixture]
    public class RelatedContentFilterModelTest
    {
        [Test]
        public void RelatedContentFilter_IsValid_AsContentType_False()
        {
            var filter = new RelatedContentFilterModel();
            filter.AsContentType = true;
            Assert.IsFalse(filter.IsValid());
            Assert.AreEqual("AsContentType", filter.Errors.FirstOrDefault().Target);
        }

        [Test]
        public void RelatedContentFilter_IsValid_AsContentType_True()
        {
            var filter = new RelatedContentFilterModel();
            filter.AsContentType = true;
            filter.RelationType = Data.Entities.Enums.RelationType.SimilarPets;
            Assert.IsTrue(filter.IsValid());
        }

        [Test]
        public void RelatedContentFilter_IsValid_RelationType_False()
        {
            var filter = new RelatedContentFilterModel();
            filter.RelationType = (Data.Entities.Enums.RelationType)Enum.Parse(typeof(Data.Entities.Enums.RelationType), "123");
            Assert.IsFalse(filter.IsValid());
            Assert.AreEqual("RelationType", filter.Errors.FirstOrDefault().Target);
        }

        [Test]
        public void RelatedContentFilter_IsValid_RelationType_True()
        {
            var filter = new RelatedContentFilterModel();
            filter.RelationType = (Data.Entities.Enums.RelationType)Enum.Parse(typeof(Data.Entities.Enums.RelationType), "SimilarPets");
            Assert.IsTrue(filter.IsValid());
        }
    }
}