//-----------------------------------------------------------------------
// <copyright file="PictureService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Files
{
    using Huellitas.Data.Entities;

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
        /// Initializes a new instance of the <see cref="PictureService"/> class.
        /// </summary>
        /// <param name="fileHelper">The file helper.</param>
        public PictureService(IFilesHelper fileHelper)
        {
            this.fileHelper = fileHelper;
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

            using (System.IO.FileStream stream = System.IO.File.OpenRead(pathOriginalFile))
            {
                using (System.IO.FileStream output = System.IO.File.OpenWrite(resizedPath))
                {
                    ////TODO:Implementar
                    stream.CopyTo(output);
                }
            }
        }
    }
}