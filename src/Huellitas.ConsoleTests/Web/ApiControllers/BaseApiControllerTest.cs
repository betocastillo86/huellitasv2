//-----------------------------------------------------------------------
// <copyright file="BaseApiControllerTest.cs" company="Dasigno">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.ConsoleTests.Web.ApiControllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Huellitas.Web.Infraestructure.WebApi;

    /// <summary>
    /// Basei Api Controller Tests
    /// </summary>
    [TestClass]
    public class BaseApiControllerTest
    {
        /// <summary>
        /// Tests the Bad the state of the request with model.
        /// </summary>
        [TestMethod]
        public void BadRequestWithModelState()
        {
            var controller = new BaseApiController();
            controller.ModelState.AddModelError("Name", "El nombre no es valido");
            controller.ModelState.AddModelError("Description", "La descripción no es valida");

            var response = controller.BadRequest(controller.ModelState);

            var error = response.Value as ApiError;

            Assert.Equals(error.Code, "BadArgument");
            Assert.Equals(error.Details.Count, 2);
            Assert.Equals(error.Details[0].Target, "Name");
            Assert.Equals(error.Details[1].Target, "Description");
            Assert.AreEqual("BadArgument", error.Details[1].Code, error.Details[0].Code);
        }
    }
}
