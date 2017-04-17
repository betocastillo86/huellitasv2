//-----------------------------------------------------------------------
// <copyright file="AdoptionFormFilterModelTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.AdoptionForms
{
    using Huellitas.Business.Services;
    using Huellitas.Web.Models.Api;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Adoption form filter model test
    /// </summary>
    /// <seealso cref="Huellitas.Tests.BaseTest" />
    [TestFixture]
    public class AdoptionFormFilterModelTest : BaseTest
    {
        /// <summary>
        /// Tests Adoption the form filter is valid false content user identifier.
        /// </summary>
        [Test]
        public void AdoptionFormFilter_IsValid_False_ContentUserId()
        {
            this.Setup();

            var filter = new AdoptionFormFilterModel();
            filter.ContentUserId = 2;

            Assert.IsFalse(filter.IsValid(1, this.contentService.Object, false));
            Assert.AreEqual("ContentUserId", filter.Errors[0].Target);
        }

        /// <summary>
        /// Tests Adoption the form filter is valid false filters null.
        /// </summary>
        [Test]
        public void AdoptionFormFilter_IsValid_False_FiltersNull()
        {
            this.Setup();

            var filter = new AdoptionFormFilterModel();

            Assert.IsFalse(filter.IsValid(1, this.contentService.Object, false));
            Assert.AreEqual("FormUserId", filter.Errors[0].Target);
        }

        /// <summary>
        /// Tests Adoption the form filter is valid false form user identifier.
        /// </summary>
        [Test]
        public void AdoptionFormFilter_IsValid_False_FormUserId()
        {
            var filter = new AdoptionFormFilterModel();
            filter.FormUserId = 2;

            Assert.IsFalse(filter.IsValid(1, this.contentService.Object, false));
            Assert.AreEqual("FormUserId", filter.Errors[0].Target);
        }

        /// <summary>
        /// Adoptions the form filter is valid false shelter.
        /// </summary>
        [Test]
        public void AdoptionFormFilter_IsValid_False_Shelter()
        {
            this.Setup();

            var filter = new AdoptionFormFilterModel();
            filter.ShelterId = 1;

            var userId = 1;

            this.contentService.Setup(c => c.IsUserInContent(userId, filter.ShelterId.Value, Data.Entities.Enums.ContentUserRelationType.Shelter))
                .Returns(false);

            Assert.IsFalse(filter.IsValid(userId, this.contentService.Object, false));
            Assert.AreEqual("ShelterId", filter.Errors[0].Target);
        }

        /// <summary>
        /// Tests Adoption form filter is valid true can see all true.
        /// </summary>
        [Test]
        public void AdoptionFormFilter_IsValid_True_CanSeeAll_True()
        {
            this.Setup();

            var filter = this.GetFilter();
            Assert.IsTrue(filter.IsValid(3, this.contentService.Object, true));
        }

        /// <summary>
        /// Tests Adoption the form filter is valid true content user identifier.
        /// </summary>
        [Test]
        public void AdoptionFormFilter_IsValid_True_ContentUserId()
        {
            this.Setup();

            var filter = new AdoptionFormFilterModel();
            filter.ContentUserId = 1;

            Assert.IsTrue(filter.IsValid(1, this.contentService.Object, false));
        }

        /// <summary>
        /// Tests Adoption the form filter is valid true form user identifier.
        /// </summary>
        [Test]
        public void AdoptionFormFilter_IsValid_True_FormUserId()
        {
            this.Setup();

            var filter = new AdoptionFormFilterModel();
            filter.FormUserId = 1;

            Assert.IsTrue(filter.IsValid(1, this.contentService.Object, false));
        }

        /// <summary>
        /// Tests Adoption the form filter is valid true share user identifier.
        /// </summary>
        [Test]
        public void AdoptionFormFilter_IsValid_True_SharedUserId()
        {
            this.Setup();

            var filter = new AdoptionFormFilterModel();
            filter.SharedToUserId = 1;

            Assert.IsTrue(filter.IsValid(1, this.contentService.Object, false));
        }

        /// <summary>
        /// Adoptions the form filter is valid false shared user identifier.
        /// </summary>
        [Test]
        public void AdoptionFormFilter_IsValid_False_SharedUserId()
        {
            var filter = new AdoptionFormFilterModel();
            filter.SharedToUserId = 2;

            Assert.IsFalse(filter.IsValid(1, this.contentService.Object, false));
            Assert.AreEqual("SharedToUserId", filter.Errors[0].Target);
        }

        /// <summary>
        /// Tests Adoption the form filter is valid true shelter.
        /// </summary>
        [Test]
        public void AdoptionFormFilter_IsValid_True_Shelter()
        {
            this.Setup();

            var filter = new AdoptionFormFilterModel();
            filter.ShelterId = 1;

            var userId = 1;

            this.contentService.Setup(c => c.IsUserInContent(userId, filter.ShelterId.Value, Data.Entities.Enums.ContentUserRelationType.Shelter))
                .Returns(true);

            Assert.IsTrue(filter.IsValid(1, this.contentService.Object, false));
        }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        protected override void Setup()
        {
            base.Setup();
            this.contentService = new Mock<IContentService>();
        }

        /// <summary>
        /// Gets the filter.
        /// </summary>
        /// <returns>the model</returns>
        private AdoptionFormFilterModel GetFilter()
        {
            var model = new AdoptionFormFilterModel();
            model.ContentId = 1;
            model.ContentUserId = 2;
            model.FormUserId = 3;
            model.LocationId = 1;
            model.PetId = 4;
            model.ShelterId = 5;
            model.Status = Data.Entities.AdoptionFormAnswerStatus.Approved;
            model.UserName = "username";
            return model;
        }
    }
}