//-----------------------------------------------------------------------
// <copyright file="BaseTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests
{
    using Huellitas.Data.Entities;
    using Huellitas.Web.Infraestructure.Security;
    using Moq;

    /// <summary>
    /// Base class for testing
    /// </summary>
    public class BaseTest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTest"/> class.
        /// </summary>
        public BaseTest()
        {
            this.MockWorkContext();
        }

        /// <summary>
        /// Gets or sets the work context mock.
        /// </summary>
        /// <value>
        /// The work context mock.
        /// </value>
        public Mock<IWorkContext> WorkContextMock { get; set; }

        /// <summary>
        /// Mocks the work context.
        /// </summary>
        private void MockWorkContext()
        {
            this.WorkContextMock = new Mock<IWorkContext>();
            this.WorkContextMock.SetupGet(c => c.CurrentUser).Returns(new User() { Id = 1, Name = "Admin", RoleEnum = Data.Entities.Enums.RoleEnum.SuperAdmin });
            this.WorkContextMock.SetupGet(c => c.CurrentUserId).Returns(1);
        }
    }
}