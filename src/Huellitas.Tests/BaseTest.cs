//-----------------------------------------------------------------------
// <copyright file="BaseTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using Huellitas.Business.Caching;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Infraestructure.Security;
    using Moq;

    /// <summary>
    /// Base class for testing
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
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
        /// Gets or sets the work context.
        /// </summary>
        /// <value>
        /// The work context.
        /// </value>
        protected Mock<IWorkContext> workContext { get; set; }

        /// <summary>
        /// The cache manager
        /// </summary>
        protected Mock<ICacheManager> cacheManager = new Mock<ICacheManager>();

        /// <summary>
        /// Mocks the work context.
        /// </summary>
        private void MockWorkContext()
        {
            this.workContext = new Mock<IWorkContext>();
            this.workContext.SetupGet(c => c.CurrentUser).Returns(new User() { Id = 1, Name = "Admin", RoleEnum = Data.Entities.Enums.RoleEnum.SuperAdmin });
            this.workContext.SetupGet(c => c.CurrentUserId).Returns(1);
        }
    }
}