using Beto.Core.Exceptions;
using Beto.Core.Web.Api.Controllers;
using Hangfire;
using Huellitas.Business.Configuration;
using Huellitas.Business.Security;
using Huellitas.Business.Tasks;
using Huellitas.Web.Infraestructure.Tasks;
using Huellitas.Web.Models.Api.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huellitas.Web.Controllers.Api.Contents
{
    [Route("api/v1/tasks")]
    public class TasksController : BaseApiController
    {
        private readonly IWorkContext workContext;
        private readonly IGeneralSettings generalSettings;
        private readonly ImageResizeTask imageResizeTask;
        private readonly DeleteOldestFilesTask deleteOldestFilesTask;

        public TasksController(
            IMessageExceptionFinder messageExceptionFinder,
            IWorkContext workContext,
            IGeneralSettings generalSettings,
            ImageResizeTask imageResizeTask,
            DeleteOldestFilesTask deleteOldestFilesTask) : base(messageExceptionFinder)
        {
            this.workContext = workContext;
            this.generalSettings = generalSettings;
            this.imageResizeTask = imageResizeTask;
            this.deleteOldestFilesTask = deleteOldestFilesTask;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody]TaskModel model)
        {
            if (this.workContext.CurrentUser.RoleEnum != Data.Entities.RoleEnum.SuperAdmin)
            {
                return this.Forbid();
            }

            switch (model.TaskId)
            {
                case 1:
                    this.ResizeImages(model);
                    break;
                case 2:
                    await this.DeleteOldFiles();
                    break;
                default:
                    break;
            }

            return this.Ok();
        }

        private async Task DeleteOldFiles()
        {
            await this.deleteOldestFilesTask.DeleteFilesAsync();
        }

        private void ResizeImages(TaskModel model)
        {
            if (this.generalSettings.EnableHangfire)
            {
                BackgroundJob.Enqueue<ImageResizeTask>(c => c.ResizeContentImages(model.Options));
            }
            else
            {
                this.imageResizeTask.ResizeContentImages(model.Options);
            }
        }
    }
}
