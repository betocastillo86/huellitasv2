//-----------------------------------------------------------------------
// <copyright file="ImageResizeTask.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Tasks
{
    using Huellitas.Business.Configuration;
    using Huellitas.Business.Services;
    using Huellitas.Business.Tasks;
    using System.Linq;

    /// <summary>
    /// Image Resize Tasks
    /// </summary>
    public class ImageResizeTask : ITask
    {
        /// <summary>
        /// The file service
        /// </summary>
        private readonly IFileService fileService;

        /// <summary>
        /// The picture service
        /// </summary>
        private readonly IPictureService pictureService;

        /// <summary>
        /// The content settings
        /// </summary>
        private readonly IContentSettings contentSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageResizeTask"/> class.
        /// </summary>
        /// <param name="fileService">The file service.</param>
        public ImageResizeTask(
            IFileService fileService,
            IPictureService pictureService,
            IContentSettings contentSettings)
        {
            this.fileService = fileService;
            this.pictureService = pictureService;
            this.contentSettings = contentSettings;
        }

        /// <summary>
        /// Resizes the pet images.
        /// </summary>
        /// <param name="filesIds">The content files.</param>
        public void ResizeContentImages(int[] filesIds)
        {
            var files = this.fileService.GetByIds(filesIds.ToArray());

            foreach (var file in files)
            {
                this.pictureService.GetPicturePath(file, this.contentSettings.PictureSizeWidthDetail, this.contentSettings.PictureSizeHeightDetail, true);
                this.pictureService.GetPicturePath(file, this.contentSettings.PictureSizeWidthList, this.contentSettings.PictureSizeHeightList, true);
            }
        }

        /// <summary>
        /// Resizes the content image.
        /// </summary>
        /// <param name="fileId">The file identifier.</param>
        public void ResizeContentImage(int fileId)
        {
            this.ResizeContentImages(new[] { fileId });
        }
    }
}