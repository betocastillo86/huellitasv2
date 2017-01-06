//-----------------------------------------------------------------------
// <copyright file="ShelterFilterModelTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers
{
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api.Contents;
    using NUnit.Framework;

    /// <summary>
    /// Shelter Filter Model Test
    /// </summary>
    [TestFixture]
    public class ShelterFilterModelTest
    {
        /// <summary>
        /// Determines whether [is valid model unpublished false].
        /// </summary>
        [Test]
        public void ShelterFilter_IsValidModel_Published_True()
        {
            var filter = new ShelterFilterModel();
            filter.Status = StatusType.Published;
            Assert.IsTrue(filter.IsValid(true));
        }

        /// <summary>
        /// Determines whether [is valid model unpublished false].
        /// </summary>
        [Test]
        public void ShelterFilter_IsValidModel_Unpublished_False()
        {
            var filter = new ShelterFilterModel();
            filter.Status = StatusType.Closed;
            Assert.IsFalse(filter.IsValid(false));

            filter.Status = StatusType.Created;
            Assert.IsFalse(filter.IsValid(false));

            filter.Status = StatusType.Hidden;
            Assert.IsFalse(filter.IsValid(false));
        }

        /// <summary>
        /// Determines whether [is valid model unpublished false].
        /// </summary>
        [Test]
        public void ShelterFilter_IsValidModel_Unpublished_True()
        {
            var filter = new ShelterFilterModel();
            filter.Status = StatusType.Closed;
            Assert.IsTrue(filter.IsValid(true));
        }
    }
}