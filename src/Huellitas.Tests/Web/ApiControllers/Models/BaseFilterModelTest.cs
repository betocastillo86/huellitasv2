//-----------------------------------------------------------------------
// <copyright file="BaseFilterModelTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Models
{
    using Huellitas.Web.Models.Api;
    using Mocks;
    using NUnit.Framework;

    /// <summary>
    /// Base Filter Model Test
    /// </summary>
    [TestFixture]
    public class BaseFilterModelTest
    {
        /// <summary>
        /// Validates the general validations.
        /// </summary>
        [Test]
        public void ValidGeneralValidations()
        {
            var model = this.GetMock();
            model.OrderBy = "a";
            model.PageSize = 20;
            model.Page = 1;

            Assert.IsTrue(model.IsValid());
        }

        /// <summary>
        /// Invalids the general validations.
        /// </summary>
        [Test]
        public void InvalidGeneralValidations()
        {
            var model = this.GetMock();

            model.PageSize = 25;
            Assert.IsFalse(model.IsValid());
            Assert.IsTrue(model.Errors[0].Message.Contains("Tamaño máximo de paginación"));

            model = this.GetMock();
            model.Page = -1;
            Assert.IsFalse(model.IsValid());
            Assert.IsTrue(model.Errors[0].Message.Contains("La pagina debe ser mayor a 0"));

            model = this.GetMock();
            model.OrderBy = "d";
            Assert.IsFalse(model.IsValid());
            Assert.IsTrue(model.Errors[0].Message.Contains("El parametro orderBy no"));
        }

        /// <summary>
        /// Gets the mock.
        /// </summary>
        /// <returns>the mock</returns>
        private MockBaseFilterModel GetMock()
        {
            return new MockBaseFilterModel(20, new string[] { "a", "b", "c" });
        }
    }
}