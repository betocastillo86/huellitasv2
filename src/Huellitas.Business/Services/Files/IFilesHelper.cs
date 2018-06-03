//-----------------------------------------------------------------------
// <copyright file="IFilesHelper.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using Huellitas.Data.Entities;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface of file helpers
    /// </summary>
    public interface IFilesHelper
    {
        /// <summary>
        /// Gets the name of the content type by file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>the content type</returns>
        string GetContentTypeByFileName(string fileName);

        string GetFolderName(File file, int filesByFolder = 50);

        /// <summary>
        /// Gets the full path.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>the path</returns>
        string GetFullPath(File file, Func<string, string> contentUrlFunction = null, int width = 0, int height = 0);

        /// <summary>
        /// Gets the physical path.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>the physical path</returns>
        string GetPhysicalPath(File file, int width = 0, int height = 0);

        bool IsImageExtension(string fileName);

        /// <summary>
        /// Saves the file asynchronous.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="bytes">The bytes.</param>
        /// <returns>the asynchronous result</returns>
        void SaveFile(File file, byte[] bytes);
    }
}