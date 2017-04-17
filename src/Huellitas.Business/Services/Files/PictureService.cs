//-----------------------------------------------------------------------
// <copyright file="PictureService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Files
{
    using Common;
    using Extensions.Services;
    using Huellitas.Data.Entities;
    using ImageSharp;
    using ImageSharp.Processing;

    /// <summary>
    /// Picture Service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.Files.IPictureService" />
    public class PictureService : IPictureService
    {
        /// <summary>
        /// The file helper
        /// </summary>
        private readonly IFilesHelper fileHelper;

        /// <summary>
        /// The log service
        /// </summary>
        private readonly ILogService logService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PictureService"/> class.
        /// </summary>
        /// <param name="fileHelper">The file helper.</param>
        /// <param name="logService">the log service</param>
        public PictureService(
            IFilesHelper fileHelper,
            ILogService logService)
        {
            this.fileHelper = fileHelper;
            this.logService = logService;
        }

        /// <summary>
        /// Gets the picture path.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="forceResize">forces the resize of the image</param>
        /// <returns>
        /// the url file
        /// </returns>
        public string GetPicturePath(File file, int width, int height, bool forceResize = false)
        {
            var resizedPhysicalPath = this.fileHelper.GetPhysicalPath(file, width, height);

            if (forceResize && !System.IO.File.Exists(resizedPhysicalPath))
            {
                this.ResizePicture(resizedPhysicalPath, file, width, height);
            }

            return this.fileHelper.GetFullPath(file, null, width, height);
        }

        /// <summary>
        /// Resizes the picture.
        /// </summary>
        /// <param name="resizedPath">The resized path.</param>
        /// <param name="file">The file.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void ResizePicture(string resizedPath, File file, int width, int height)
        {
            ////Takes the original image path
            var pathOriginalFile = this.fileHelper.GetPhysicalPath(file);
            try
            {
                using (var image = Image.Load(pathOriginalFile))
                {
                    var resizeOptions = new ResizeOptions()
                    {
                        Size = new Size { Width = width, Height = height },
                        Mode = ResizeMode.Crop
                    };

                    image.Resize(resizeOptions)
                       .Save(resizedPath);
                }

                ////using (var image = Image.Load(pathOriginalFile))
                ////{
                ////    var resizeOptions = new ResizeOptions()
                ////    {
                ////        Size = new Size { Width = width, Height = height },
                ////        Mode = ResizeMode.Max
                ////    };

                ////    image.Resize(resizeOptions)
                ////        .Save(System.IO.Path.GetDirectoryName(resizedPath) + $"\\{width}_{height}_max{System.IO.Path.GetExtension(resizedPath)}");

                ////}

                ////using (var image = Image.Load(pathOriginalFile))
                ////{
                ////    var resizeOptions = new ResizeOptions()
                ////    {
                ////        Size = new Size { Width = width, Height = height },
                ////        Mode = ResizeMode.Min
                ////    };

                ////    image.Resize(resizeOptions)
                ////        .Save(System.IO.Path.GetDirectoryName(resizedPath) + $"\\{width}_{height}_min{System.IO.Path.GetExtension(resizedPath)}");
                ////}

                ////using (var image = Image.Load(pathOriginalFile))
                ////{
                ////    var resizeOptions = new ResizeOptions()
                ////    {
                ////        Size = new Size { Width = width, Height = height },
                ////        Mode = ResizeMode.Pad
                ////    };

                ////    image.Resize(resizeOptions)
                ////        .Save(System.IO.Path.GetDirectoryName(resizedPath) + $"\\{width}_{height}_pad{System.IO.Path.GetExtension(resizedPath)}");
                ////}

                ////using (var image = Image.Load(pathOriginalFile))
                ////{
                ////    var resizeOptions = new ResizeOptions()
                ////    {
                ////        Size = new Size { Width = width, Height = height },
                ////        Mode = ResizeMode.Stretch
                ////    };

                ////    image.Resize(resizeOptions)
                ////        .Save(System.IO.Path.GetDirectoryName(resizedPath) + $"\\{width}_{height}_Stretch{System.IO.Path.GetExtension(resizedPath)}");
                ////}



                ////using (var image = Image.Load(pathOriginalFile))
                ////{
                ////    var resizeOptions = new ResizeOptions()
                ////    {
                ////        Size = new Size { Width = width, Height = height },
                ////        Mode = ResizeMode.BoxPad
                ////    };

                ////    image.Resize(resizeOptions)
                ////        .Save(System.IO.Path.GetDirectoryName(resizedPath) + $"\\{width}_{height}_BoxPad{System.IO.Path.GetExtension(resizedPath)}");
                ////}
            }
            catch (System.Exception e)
            {
                this.logService.Error(e);
            }
        }
    }
}