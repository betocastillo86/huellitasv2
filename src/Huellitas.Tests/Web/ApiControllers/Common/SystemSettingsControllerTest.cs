﻿//-----------------------------------------------------------------------
// <copyright file="SystemSettingsControllerTest.cs" company="Dasigno">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Entities;
    using Data.Infraestructure;
    using Huellitas.Business.Services.Configuration;
    using Huellitas.Web.Controllers.Api.Common;
    using Huellitas.Web.Models.Api.Common;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Test of system settings controller
    /// </summary>
    [TestFixture]
    public class SystemSettingsControllerTest : BaseTest
    {
        /// <summary>
        /// The system setting service
        /// </summary>
        private Mock<ISystemSettingService> systemSettingService;

        /// <summary>
        /// Gets the system settings bad request.
        /// </summary>
        [Test]
        public void GetSystemSettings_BadRequest()
        {
            this.Setup();
            var controller = this.GetController();
            var filter = this.GetFilter();
            filter.PageSize = 50000;

            var response = controller.Get(filter) as ObjectResult;

            Assert.AreEqual(400, response.StatusCode);
        }

        /// <summary>
        /// Gets the system settings forbid.
        /// </summary>
        [Test]
        public void GetSystemSettings_Forbid()
        {
            this.Setup();
            this.SetupPublicUser();

            var controller = this.GetController();
            var filter = this.GetFilter();

            var response = controller.Get(filter);

            Assert.IsAssignableFrom(typeof(ForbidResult), response);
        }

        /// <summary>
        /// Gets the system settings ok.
        /// </summary>
        [Test]
        public void GetSystemSettings_Ok()
        {
            this.Setup();

            this.systemSettingService.Setup(c => c.Get(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(this.GetList());

            var controller = this.GetController();
            var filter = this.GetFilter();

            var response = controller.Get(filter) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        /// <summary>
        /// Puts the system settings different identifier not found.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PutSystemSettings_DifferentId_NotFound()
        {
            this.Setup();
            var entity = this.GetEntity();
            var model = this.GetModel();

            this.systemSettingService.Setup(c => c.GetByKey(model.Name))
                .Returns(entity);

            var controller = this.GetController();

            var response = await controller.Put(50, model) as NotFoundResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        /// <summary>
        /// Puts the system settings forbidden.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PutSystemSettings_Forbidden()
        {
            this.Setup();
            this.SetupPublicUser(55);

            var entity = this.GetEntity();
            var model = this.GetModel();

            var controller = this.GetController();

            var response = await controller.Put(model.Id, model);

            Assert.IsAssignableFrom(typeof(ForbidResult), response);
        }

        /// <summary>
        /// Puts the system settings not found.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PutSystemSettings_NotFound()
        {
            this.Setup();
            var entity = this.GetEntity();
            var model = this.GetModel();

            var controller = this.GetController();

            var response = await controller.Put(model.Id, model) as NotFoundResult;

            Assert.AreEqual(404, response.StatusCode);
        }

        /// <summary>
        /// Puts the system settings ok.
        /// </summary>
        /// <returns>the task</returns>
        [Test]
        public async Task PutSystemSettings_Ok()
        {
            this.Setup();
            var entity = this.GetEntity();
            var model = this.GetModel();

            this.systemSettingService.Setup(c => c.GetByKey(model.Name))
                .Returns(entity);

            var controller = this.GetController();

            var response = await controller.Put(model.Id, model) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        protected override void Setup()
        {
            base.Setup();
            this.systemSettingService = new Mock<ISystemSettingService>();
        }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        /// <returns>the controller</returns>
        private SystemSettingsController GetController()
        {
            return new SystemSettingsController(
                this.systemSettingService.Object,
                this.workContext.Object);
        }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <returns>the entity</returns>
        private SystemSetting GetEntity()
        {
            return new SystemSetting()
            {
                Id = 1,
                Name = "Name",
                Value = "Value"
            };
        }

        /// <summary>
        /// Gets the filter.
        /// </summary>
        /// <returns>the filter</returns>
        private SystemSettingFilterModel GetFilter()
        {
            return new SystemSettingFilterModel()
            {
            };
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <returns>the list</returns>
        private IPagedList<SystemSetting> GetList()
        {
            var list = new List<SystemSetting>()
            {
                new SystemSetting() { Id = 1, Name = "Name", Value = "Value" },
                new SystemSetting() { Id = 2, Name = "Name2", Value = "Value2" }
            };

            return new PagedList<SystemSetting>(list.AsQueryable(), 1, 10);
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <returns>the model</returns>
        private SystemSettingModel GetModel()
        {
            return new SystemSettingModel()
            {
                Id = 1,
                Name = "Name",
                Value = "Value"
            };
        }
    }
}