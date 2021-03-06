﻿//-----------------------------------------------------------------------
// <copyright file="RelatedContentFilterModelTest.cs" company="Huellitas Sin Hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Contents
{
    using System;
    using System.Linq;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api;
    using NUnit.Framework;

    /// <summary>
    /// Related Contents Filter Model Test
    /// </summary>
    [TestFixture]
    public class RelatedContentFilterModelTest
    {
        /// <summary>
        /// Related the content filter is valid as content type false.
        /// </summary>
        [Test]
        public void RelatedContentFilter_IsValid_AsContentType_False()
        {
            var filter = new RelatedContentFilterModel();
            filter.AsContentType = true;
            Assert.IsFalse(filter.IsValid());
            Assert.AreEqual("AsContentType", filter.Errors.FirstOrDefault().Target);
        }

        /// <summary>
        /// Related the content filter is valid as content type true.
        /// </summary>
        [Test]
        public void RelatedContentFilter_IsValid_AsContentType_True()
        {
            var filter = new RelatedContentFilterModel();
            filter.AsContentType = true;
            filter.RelationType = RelationType.SimilarPets;
            Assert.IsTrue(filter.IsValid());
        }

        /// <summary>
        /// Related the content filter is valid relation type false.
        /// </summary>
        [Test]
        public void RelatedContentFilter_IsValid_RelationType_False()
        {
            var filter = new RelatedContentFilterModel();
            filter.RelationType = (RelationType)Enum.Parse(typeof(RelationType), "123");
            Assert.IsFalse(filter.IsValid());
            Assert.AreEqual("RelationType", filter.Errors.FirstOrDefault().Target);
        }

        /// <summary>
        /// Related the content filter is valid relation type true.
        /// </summary>
        [Test]
        public void RelatedContentFilter_IsValid_RelationType_True()
        {
            var filter = new RelatedContentFilterModel();
            filter.RelationType = (RelationType)Enum.Parse(typeof(RelationType), "SimilarPets");
            Assert.IsTrue(filter.IsValid());
        }
    }
}