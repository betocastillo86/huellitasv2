﻿//-----------------------------------------------------------------------
// <copyright file="BaseTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using Data.Entities.Enums;
    using Huellitas.Business.Caching;
    using Huellitas.Business.Security;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Infraestructure.Security;
    using Moq;

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
            this.workContext.SetupGet(c => c.CurrentUser).Returns(new User() { Id = 1, Name = "Admin", RoleEnum = Data.Entities.Enums.RoleEnum.SuperAdmin });
            this.workContext.SetupGet(c => c.CurrentUserId).Returns(1);
            this.workContext.SetupGet(c => c.IsAuthenticated).Returns(true);
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