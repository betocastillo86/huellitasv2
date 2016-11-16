//-----------------------------------------------------------------------
// <copyright file="FilesHelper.cs" company="Huellitas sin hogar">
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
            ////TODO:Especificar mejor y terminar desarrollo
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

        /// <summary>
        /// Gets the size of the file name with.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>the file name</returns>
        private string GetFileNameWithSize(string fileName, int width = 0, int height = 0)
        {
            ////TODO:Implementar metodo
            if (width != 0 && height != 0)
            {
                return fileName;
            }
            else
            {
                return fileName;
            }
        }

        /// <summary>
        /// Gets the name of the folder depending of file name or creation
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>the folder name</returns>
        private string GetFolderName(File file)
        {
            ////TODO: Implementar metodo
            return "000000";
        }
    }
}