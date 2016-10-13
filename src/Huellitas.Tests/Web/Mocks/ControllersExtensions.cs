//-----------------------------------------------------------------------
// <copyright file="ControllersExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.Mocks
{
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
        /// <returns>the controller</returns>
        public static Controller AddUrl(this Controller controller)
        {
            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelper.Setup(c => c.Content(null)).Returns(string.Empty);
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
    }
}