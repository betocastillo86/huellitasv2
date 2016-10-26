//-----------------------------------------------------------------------
// <copyright file="ControllersExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.Mocks
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;

    /// <summary>
    /// Controller Extensions for testing
    /// </summary>
    public static class ControllersExtensions
    {
        /// <summary>
        /// Adds the URL.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="anyLink">if set to <c>true</c> [any link].</param>
        /// <returns>the controller</returns>
        public static Controller AddUrl(
            this Controller controller, 
            bool anyLink = false)
        {
            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelper.Setup(c => c.Content(null)).Returns(string.Empty);

            if (anyLink)
            {
                mockUrlHelper.Setup(c => c.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("route/1");
            }

            controller.Url = mockUrlHelper.Object;

            return controller;
        }

        /// <summary>
        /// Adds the response to a controller
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns>same controller</returns>
        public static Controller AddResponse(this Controller controller)
        {
            var mockHeaderDictionary = new HeaderDictionary();
            var mockResponse = new Mock<HttpResponse>();
            mockResponse.SetupGet(r => r.Headers).Returns(mockHeaderDictionary);

            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(a => a.Response).Returns(mockResponse.Object);

            controller.ControllerContext = new ControllerContext() { HttpContext = mockHttpContext.Object };

            return controller;
        }

        /// <summary>
        /// Determines whether [is valid model state] [the specified model].
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <c>true</c> if [is valid model state] [the specified model]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidModelState(this Controller controller, object model)
        {
            var validationErrors = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            return System.ComponentModel.DataAnnotations.Validator.TryValidateObject(model, new System.ComponentModel.DataAnnotations.ValidationContext(model), validationErrors);
        }
    }
}