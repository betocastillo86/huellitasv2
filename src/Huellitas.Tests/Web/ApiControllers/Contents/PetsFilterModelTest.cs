//-----------------------------------------------------------------------
// <copyright file="PetsFilterModelTest.cs" company="Huellits sin Hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Contents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api;
    using NUnit.Framework;

    /// <summary>
    /// Pets Filter Model Test
    /// </summary>
    [TestFixture]
    public class PetsFilterModelTest
    {
        /// <summary>
        /// Validates all the valid order by
        /// </summary>
        [Test]
        public void AllValidOrderBy()
        {
            var validOrders = Enum.GetValues(typeof(ContentOrderBy)).Cast<ContentOrderBy>();
            foreach (var validOrder in validOrders)
            {
                var filter = new PetsFilterModel();
                filter.OrderBy = validOrder.ToString();
                Assert.IsTrue(filter.IsValid());
            }
        }

        /// <summary>
        /// Validate is Invalid the age.
        /// </summary>
        [Test]
        public void InvalidAge()
        {
            IList<FilterAttribute> selectedFilters = null;
            var filter = new PetsFilterModel();
            filter.Age = "-5";
            Assert.IsFalse(filter.IsValid(false, out selectedFilters));
            filter.Age = "s-1";
            Assert.IsFalse(filter.IsValid(false, out selectedFilters));
            filter.Age = "1-s";
            Assert.IsFalse(filter.IsValid(false, out selectedFilters));
            filter.Age = "a-s";
            Assert.IsFalse(filter.IsValid(false, out selectedFilters));
            filter.Age = "4-5-a";
            Assert.IsFalse(filter.IsValid(false, out selectedFilters));
            filter.Age = "4-5a";
            Assert.IsFalse(filter.IsValid(false, out selectedFilters));
            filter.Age = "4-5-6";
            Assert.IsFalse(filter.IsValid(false, out selectedFilters));
            filter.Age = null;
            Assert.IsFalse(filter.IsValid(false, out selectedFilters));
        }

        /// <summary>
        /// Validates Invalid the size of the filter page.
        /// </summary>
        [Test]
        public void InvalidFilterPageSize()
        {
            var filter = new PetsFilterModel();
            filter.PageSize = 25;

            Assert.IsFalse(filter.IsValid());
            Assert.AreEqual("BadArgument", filter.Errors[0].Code);
            Assert.AreEqual("PageSize", filter.Errors[0].Target);
        }

        /// <summary>
        /// Validates is Invalid the genre.
        /// </summary>
        [Test]
        public void InvalidGenre()
        {
            IList<FilterAttribute> selectedFilters = null;
            var filter = new PetsFilterModel();
            filter.Genre = "f";
            Assert.IsFalse(filter.IsValid(false, out selectedFilters));
            filter.Genre = "1f";
            Assert.IsFalse(filter.IsValid(false, out selectedFilters));
        }

        /// <summary>
        /// Validates Invalid order by.
        /// </summary>
        [Test]
        public void InvalidOrderBy()
        {
            var filter = new PetsFilterModel();
            filter.OrderBy = "CreationDate";

            Assert.IsFalse(filter.IsValid());
            Assert.AreEqual("BadArgument", filter.Errors[0].Code);
            Assert.AreEqual("OrderBy", filter.Errors[0].Target);
        }

        /// <summary>
        /// Validate is Invalid the shelter.
        /// </summary>
        [Test]
        public void InvalidShelter()
        {
            IList<FilterAttribute> selectedFilters = null;
            var filter = new PetsFilterModel();
            filter.Shelter = "1,g,c,4";
            Assert.IsFalse(filter.IsValid(true, out selectedFilters));
            filter.Shelter = "1,2g,4";
            Assert.IsFalse(filter.IsValid(true, out selectedFilters));
            filter.Shelter = ",1";
            Assert.IsFalse(filter.IsValid(true, out selectedFilters));
            filter.Shelter = "ddd";
            Assert.IsFalse(filter.IsValid(true, out selectedFilters));
            filter.Shelter = "1,";
            Assert.IsFalse(filter.IsValid(true, out selectedFilters));
            filter.Shelter = "1,,d1";
            Assert.IsFalse(filter.IsValid(true, out selectedFilters));
        }

        /// <summary>
        /// Determines whether [is valid model unpublished false].
        /// </summary>
        [Test]
        public void IsValidModel_Unpublished_False()
        {
            IList<FilterAttribute> selectedFilters = null;
            var filter = new PetsFilterModel();
            filter.Status = StatusType.Closed;
            Assert.IsFalse(filter.IsValid(false, out selectedFilters));

            filter.Status = StatusType.Created;
            Assert.IsFalse(filter.IsValid(false, out selectedFilters));

            filter.Status = StatusType.Hidden;
            Assert.IsFalse(filter.IsValid(false, out selectedFilters));
        }

        /// <summary>
        /// Determines whether [is valid model unpublished false].
        /// </summary>
        [Test]
        public void IsValidModel_Unpublished_True()
        {
            IList<FilterAttribute> selectedFilters = null;
            var filter = new PetsFilterModel();
            filter.Status = StatusType.Closed;
            Assert.IsTrue(filter.IsValid(true, out selectedFilters));
        }

        /// <summary>
        /// Determines whether [is valid model unpublished false].
        /// </summary>
        [Test]
        public void IsValidModel_Published_True()
        {
            IList<FilterAttribute> selectedFilters = null;
            var filter = new PetsFilterModel();
            filter.Status = StatusType.Published;
            Assert.IsTrue(filter.IsValid(true, out selectedFilters));
        }

        /// <summary>
        /// Validate is Invalid the size.
        /// </summary>
        [Test]
        public void InvalidSize()
        {
            IList<FilterAttribute> selectedFilters = null;
            var filter = new PetsFilterModel();
            filter.Size = "1,g,c,4";
            Assert.IsFalse(filter.IsValid(true, out selectedFilters));
            filter.Size = "1,2g,4";
            Assert.IsFalse(filter.IsValid(true, out selectedFilters));
            filter.Size = ",1";
            Assert.IsFalse(filter.IsValid(true, out selectedFilters));
            filter.Size = "ddd";
            Assert.IsFalse(filter.IsValid(true, out selectedFilters));
            filter.Size = "1,,d1";
            Assert.IsFalse(filter.IsValid(true, out selectedFilters));
            filter.Size = "1,";
            Assert.IsFalse(filter.IsValid(true, out selectedFilters));
        }

        /// <summary>
        /// Validate is Invalid the subtype.
        /// </summary>
        [Test]
        public void InvalidSubtype()
        {
            IList<FilterAttribute> selectedFilters = null;
            var filter = new PetsFilterModel();
            filter.SubType = "1,g,c,4";
            Assert.IsFalse(filter.IsValid(true, out selectedFilters));
            filter.SubType = "1,2g,4";
            Assert.IsFalse(filter.IsValid(true, out selectedFilters));
            filter.SubType = ",1";
            Assert.IsFalse(filter.IsValid(true, out selectedFilters));
            filter.SubType = "ddd";
            Assert.IsFalse(filter.IsValid(true, out selectedFilters));
            filter.Shelter = "1,";
            Assert.IsFalse(filter.IsValid(true, out selectedFilters));
            filter.SubType = "1,,d1";
            Assert.IsFalse(filter.IsValid(true, out selectedFilters));
        }

        /// <summary>
        /// Tests is valid the age.
        /// </summary>
        [Test]
        public void ValidAge()
        {
            IList<FilterAttribute> selectedFilters = null;
            var filter = new PetsFilterModel();
            filter.Age = "1-5";
            Assert.IsTrue(filter.IsValid(true, out selectedFilters));
            Assert.AreEqual("1", selectedFilters[0].Value.ToString());
            Assert.AreEqual("5", selectedFilters[0].ValueTo.ToString());

            selectedFilters = null;
            filter.Age = "5-1";
            Assert.IsTrue(filter.IsValid(true, out selectedFilters));

            selectedFilters = null;
            filter.Age = "1-";
            Assert.IsTrue(filter.IsValid(true, out selectedFilters));
            Assert.AreEqual("1", selectedFilters[0].Value.ToString());
            Assert.AreEqual(int.MaxValue.ToString(), selectedFilters[0].ValueTo.ToString());

            selectedFilters = null;
            filter.Age = null;
            Assert.IsTrue(filter.IsValid(true, out selectedFilters));
        }

        /// <summary>
        /// Tests is valid the genre.
        /// </summary>
        [Test]
        public void ValidGenre()
        {
            IList<FilterAttribute> selectedFilters = null;
            var filter = new PetsFilterModel();
            filter.Genre = "1";
            Assert.IsTrue(filter.IsValid(true, out selectedFilters));
            filter.Genre = null;
            Assert.IsTrue(filter.IsValid(true, out selectedFilters));
        }

        /// <summary>
        /// Tests is valid the shelter.
        /// </summary>
        [Test]
        public void ValidShelter()
        {
            IList<FilterAttribute> selectedFilters = null;
            var filter = new PetsFilterModel();
            filter.Shelter = "1,2,3,4";
            Assert.IsTrue(filter.IsValid(true, out selectedFilters));
            filter.Shelter = "1";
            Assert.IsTrue(filter.IsValid(true, out selectedFilters));
            filter.Shelter = null;
            Assert.IsTrue(filter.IsValid(true, out selectedFilters));
        }

        /// <summary>
        /// Tests is valid the size.
        /// </summary>
        [Test]
        public void ValidSize()
        {
            IList<FilterAttribute> selectedFilters = null;
            var filter = new PetsFilterModel();
            filter.Size = "1,2,3,4";
            Assert.IsTrue(filter.IsValid(true, out selectedFilters));
            filter.Size = "1";
            Assert.IsTrue(filter.IsValid(true, out selectedFilters));
            filter.Size = null;
            Assert.IsTrue(filter.IsValid(true, out selectedFilters));
        }

        /// <summary>
        /// Tests is valid the subtype.
        /// </summary>
        [Test]
        public void ValidSubtype()
        {
            IList<FilterAttribute> selectedFilters = null;
            var filter = new PetsFilterModel();
            filter.SubType = "1,2,3,4";
            Assert.IsTrue(filter.IsValid(true, out selectedFilters));
            filter.SubType = "1";
            Assert.IsTrue(filter.IsValid(true, out selectedFilters));
            filter.SubType = null;
            Assert.IsTrue(filter.IsValid(true, out selectedFilters));
        }
    }
}