//-----------------------------------------------------------------------
// <copyright file="FilesRemoverSubscriber.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Subscribers
{
    using System.Threading.Tasks;
    using Beto.Core.Data.Files;
    using Beto.Core.EventPublisher;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Security;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// Removes the physical files
    /// </summary>
    /// <seealso cref="Huellitas.Business.EventPublisher.ISubscriber{Huellitas.Business.EventPublisher.EntityDeletedMessage{Huellitas.Data.Entities.File}}" />
    public class FilesRemoverSubscriber : ISubscriber<EntityDeletedMessage<File>>
    {
        /// <summary>
        /// The files helper
        /// </summary>
        private readonly IFilesHelper filesHelper;

        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// The log service
        /// </summary>
        private readonly ILogService logService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilesRemoverSubscriber"/> class.
        /// </summary>
        /// <param name="filesHelper">The files helper.</param>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        /// <param name="logService">The log service.</param>
        /// <param name="workContext">The work context.</param>
        public FilesRemoverSubscriber(
            IFilesHelper filesHelper,
            IHostingEnvironment hostingEnvironment,
            ILogService logService,
            IWorkContext workContext)
        {
            this.filesHelper = filesHelper;
            this.hostingEnvironment = hostingEnvironment;
            this.logService = logService;
            this.workContext = workContext;
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task HandleEvent(EntityDeletedMessage<File> message)
        {
            var mainfile = this.filesHelper.GetPhysicalPath(message.Entity);

            try
            {
                //// Deletes the file
                System.IO.File.Delete(mainfile);

                if (this.filesHelper.IsImageExtension(message.Entity.FileName))
                {
                    var folderRoot = string.Concat(this.hostingEnvironment.WebRootPath, "/img/content/", this.filesHelper.GetFolderName(message.Entity));

                    var thumbnails = System.IO.Directory.GetFiles(folderRoot, $"{message.Entity.Id}_*");
                    foreach (var thumbnail in thumbnails)
                    {
                        System.IO.File.Delete(thumbnail);
                    }
                }

                await Task.FromResult(0);
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                this.logService.Error(e, this.workContext.CurrentUser);
            }
        }
    }
}