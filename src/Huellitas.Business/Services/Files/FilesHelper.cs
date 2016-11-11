//-----------------------------------------------------------------------
// <copyright file="IFilesHelper.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Files
{
    using System;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Files Helper
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.Files.IFilesHelper" />
    public class FilesHelper : IFilesHelper
    {
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
            var fileName = $"~/img/content/{this.GetFolderName(file)}/{this.GetFileNameWithSize(file.FileName, width, height)}";

            if (contentUrlFunction != null)
            {
                return contentUrlFunction(fileName);
            }
            else
            {
                return fileName;
            }
        }

        private string GetFolderName(File file)
        {
            return "000000";
        }

        private string GetFileNameWithSize(string fileName, int width = 0, int height = 0)
        {
            if (width != 0 && height != 0)
            {
                return fileName;
            }
            else
            {
                return fileName;
            }
        }
    }
}