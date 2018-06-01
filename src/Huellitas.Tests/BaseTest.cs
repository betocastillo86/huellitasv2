//-----------------------------------------------------------------------
// <copyright file="BaseTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests
{
    using Huellitas.Business.Caching;
    using Huellitas.Business.Configuration;
    using Huellitas.Business.EventPublisher;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;
    using Moq;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Base class for testing
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed.")]
    public class BaseTest
    {
        /// <summary>
        /// The cache manager
        /// </summary>
        protected Mock<ICacheManager> cacheManager = new Mock<ICacheManager>();

        /// <summary>
        /// The content service
        /// </summary>
        protected Mock<IContentService> contentService = new Mock<IContentService>();

        /// <summary>
        /// The content settings
        /// </summary>
        protected Mock<IContentSettings> contentSettings = new Mock<IContentSettings>();

        protected Mock<IGeneralSettings> generalSettings = new Mock<IGeneralSettings>();

        protected Mock<ILogService> logService = new Mock<ILogService>();

        /// <summary>
        /// The publisher
        /// </summary>
        protected Mock<IPublisher> publisher = new Mock<IPublisher>();

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
        /// Setups this instance.
        /// </summary>
        protected virtual void Setup()
        {
            this.workContext = new Mock<IWorkContext>();
            this.contentService = new Mock<IContentService>();
            this.contentSettings = new Mock<IContentSettings>();
            this.publisher = new Mock<IPublisher>();
            this.workContext.SetupGet(c => c.CurrentUser).Returns(new User() { Id = 1, Name = "Admin", RoleEnum = RoleEnum.SuperAdmin });
            this.workContext.SetupGet(c => c.CurrentUserId).Returns(1);
            this.workContext.SetupGet(c => c.IsAuthenticated).Returns(true);
            this.generalSettings = new Mock<IGeneralSettings>();
            this.logService = new Mock<ILogService>();
        }

        /// <summary>
        /// Setups the not authenticated.
        /// </summary>
        protected virtual void SetupNotAuthenticated()
        {
            this.workContext = new Mock<IWorkContext>();
            this.workContext.SetupGet(c => c.CurrentUser).Returns((User)null);
            this.workContext.SetupGet(c => c.CurrentUserId).Returns(0);
            this.workContext.SetupGet(c => c.IsAuthenticated).Returns(false);
        }

        /// <summary>
        /// Setups the public user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        protected virtual void SetupPublicUser(int id = 1)
        {
            this.workContext = new Mock<IWorkContext>();
            this.workContext.SetupGet(c => c.CurrentUser).Returns(new User() { Id = id, Name = "Publico", RoleEnum = RoleEnum.Public });
            this.workContext.SetupGet(c => c.CurrentUserId).Returns(id);
            this.workContext.SetupGet(c => c.IsAuthenticated).Returns(true);
        }

        /// <summary>
        /// Mocks the work context.
        /// </summary>
        private void MockWorkContext()
        {
            this.Setup();
        }
    }
}