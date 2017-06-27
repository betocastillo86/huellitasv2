//-----------------------------------------------------------------------
// <copyright file="FilesHelper.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System;
    using System.Threading.Tasks;
    using Huellitas.Business.Configuration;
    using Huellitas.Data.Entities;
    using ImageSharp;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// Files Helper
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.IFilesHelper" />
    public class FilesHelper : IFilesHelper
    {
        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// The general settings
        /// </summary>
        private readonly IGeneralSettings generalSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilesHelper"/> class.
        /// </summary>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        /// <param name="generalSettings">The general settings.</param>
        public FilesHelper(
            IHostingEnvironment hostingEnvironment,
            IGeneralSettings generalSettings)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.generalSettings = generalSettings;
        }

        /// <summary>
        /// Gets the name of the content type by file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// the content type
        /// </returns>
        public string GetContentTypeByFileName(string fileName)
        {
            var contentType = string.Empty;
            var fileExtension = System.IO.Path.GetExtension(fileName);

            switch (fileExtension)
            {
                case ".bmp":
                    contentType = "image/bmp";
                    break;

                case ".gif":
                    contentType = "image/gif";
                    break;

                case ".jpeg":
                case ".jpg":
                case ".jpe":
                case ".jfif":
                case ".pjpeg":
                case ".pjp":
                    contentType = "image/jpeg";
                    break;

                case ".png":
                    contentType = "image/png";
                    break;

                case ".tiff":
                case ".tif":
                    contentType = "image/tiff";
                    break;

                default:
                    break;
            }

            return contentType;
        }

        /// <summary>
        /// Gets the size of the file name with.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>the file name</returns>
        public string GetFileNameWithSize(File file, int width = 0, int height = 0)
        {
            var nameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
            var extension = System.IO.Path.GetExtension(file.FileName);

            if (width != 0 && height != 0)
            {
                return $"{file.Id}_{nameWithoutExtension}_{width}x{height}{extension}";
            }
            else
            {
                return $"{file.Id}{extension}";
            }
        }

        /// <summary>
        /// Gets the name of the folder depending of file name or creation
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="filesByFolder">number of files the can host by folder</param>
        /// <returns>the folder name</returns>
        public string GetFolderName(File file, int filesByFolder = 50)
        {
            ////Every 50 files creates a new Folder
            var folder = Math.Ceiling((decimal)file.Id / filesByFolder);
            return folder.ToString("000000");
        }

        /// <summary>
        /// Gets the full path.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>
        /// the path
        /// </returns>
        public string GetFullPath(File file, Func<string, string> contentUrlFunction = null, int width = 0, int height = 0)
        {
            var fileName = $"/img/content/{this.GetFolderName(file)}/{this.GetFileNameWithSize(file, width, height)}";

            if (contentUrlFunction != null)
            {
                return contentUrlFunction(fileName);
            }
            else
            {
                return fileName;
            }
        }

        /// <summary>
        /// Gets the physical path.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>
        /// the physical path
        /// </returns>
        public string GetPhysicalPath(File file, int width = 0, int height = 0)
        {
            var relativePath = this.GetFullPath(file, null, width, height);
            return string.Concat(this.hostingEnvironment.WebRootPath, relativePath);
        }

        public bool IsImageExtension(string fileName)
        {
            var extension = System.IO.Path.GetExtension(fileName);

            if (string.IsNullOrEmpty(extension))
            {
                extension = string.Empty;
            }

            extension = extension.ToLower();
            bool result = false;
            switch (extension)
            {
                case ".gif":
                case ".jpeg":
                case ".jpg":
                case ".png":
                    result = true;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Saves a file on the disk
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="bytes">The bytes.</param>
        /// <returns>
        /// the name of the new file
        /// </returns>
        public async Task SaveFileAsync(File file, byte[] bytes)
        {
            ////TODO:Test
            if (file.Id <= 0)
            {
                throw new ArgumentException("The file does not contain a valid id");
            }
            else if (bytes.Length == 0)
            {
                throw new ArgumentException("The bytes does not have content");
            }

            var fullPath = this.GetPhysicalPath(file);

            var directory = System.IO.Path.GetDirectoryName(fullPath);
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }

            var image = ImageSharp.Image.Load(bytes);
            int newWidth = image.Width > this.generalSettings.MaxWidthPictureSize ? this.generalSettings.MaxWidthPictureSize : image.Width;
            int newHeight = image.Height > this.generalSettings.MaxHeightPictureSize ? this.generalSettings.MaxHeightPictureSize : image.Height;

            image.AutoOrient()
                .Resize(new ImageSharp.Processing.ResizeOptions { Mode = ImageSharp.Processing.ResizeMode.Max, Size = new Size { Width = newWidth, Height = newHeight } })
                .Save(fullPath);
        }
    }
}