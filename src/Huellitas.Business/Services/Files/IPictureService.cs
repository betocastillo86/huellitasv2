//-----------------------------------------------------------------------
// <copyright file="IPictureService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using Huellitas.Data.Entities;

    /// <summary>
    /// Interface of picture service
    /// </summary>
    public interface IPictureService
    {
        /// <summary>
        /// Gets the picture path.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="forceResize">forces the resize of the image</param>
        /// <returns>the url file</returns>
        string GetPicturePath(File file, int width, int height, bool forceResize = false);
    }
}