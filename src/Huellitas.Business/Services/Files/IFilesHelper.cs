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
    /// Interface of file helpers
    /// </summary>
    public interface IFilesHelper
    {
        /// <summary>
        /// Gets the full path.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>the path</returns>
        string GetFullPath(File file, Func<string, string> contentUrlFunction = null, int width = 0, int height = 0);
    }
}