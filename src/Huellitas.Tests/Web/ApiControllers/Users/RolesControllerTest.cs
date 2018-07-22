//-----------------------------------------------------------------------
// <copyright file="RolesControllerTest.cs" company="Dasigno">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Users
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities;
    using Huellitas.Business.Services;
    using Huellitas.Web.Controllers.Api;
    using Huellitas.Web.Models.Api;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Roles Controller Test
    /// </summary>
    /// <seealso cref="Huellitas.Tests.BaseTest" />
    [TestFixture]
    public class RolesControllerTest : BaseTest
    {
        /// <summary>
        /// The role service
        /// </summary>
        private Mock<IRoleService> roleService = new Mock<IRoleService>();

        /// <summary>
        /// Gets the roles bad request.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task GetRoles_BadRequest()
        {
            this.Setup();
            this.SetupPublicUser();

            var controller = this.GetController();
            var result = await controller.Get();

            Assert.IsAssignableFrom(typeof(ForbidResult), result);
        }

        /// <summary>
        /// Gets the roles ok.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task GetRoles_Ok()
        {
            this.Setup();

            this.roleService.Setup(c => c.GetAll())
                .ReturnsAsync(new List<Role>() { new Role { Id = 1, Name = string.Empty, Description = string.Empty } });

            var controller = this.GetController();
            var result = await controller.Get() as ObjectResult;

            Assert.AreEqual(200, result.StatusCode);
            Assert.IsAssignableFrom(typeof(List<RoleModel>), result.Value);
        }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        protected override void Setup()
        {
            this.roleService = new Mock<IRoleService>();
            base.Setup();
        }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        /// <returns>the controller</returns>
        private RolesController GetController()
        {
            return new RolesController(
                this.roleService.Object, 
                this.workContext.Object,
                this.messageExceptionFinder.Object);
        }
    }
}