//-----------------------------------------------------------------------
// <copyright file="IPictureService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System;
    using System.Threading.Tasks;
    using Beto.Core.Data.Files;
    using Huellitas.Business.Models;
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
        /// <param name="resizeMode">resize mode</param>
        /// <returns>the url file</returns>
        string GetPicturePath(File file, int width, int height, bool forceResize = false, ResizeMode resizeMode = ResizeMode.Crop);

        string RotateImage(File file, int width, int height, bool rotateOriginal = false);

        /// <summary>
        /// Creates the social network post.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="file">The file.</param>
        /// <param name="color">The color.</param>
        /// <param name="network">The network.</param>
        /// <param name="contentUrlFunction">Content URL function</param>
        /// <returns>the new path of the image</returns>
        Task<string> CreateSocialNetworkPost(
            Content content,
            File file,
            SocialPostColors color = SocialPostColors.Blue,
            SocialNetwork network = SocialNetwork.Facebook,
            Func<string, string> contentUrlFunction = null);
    }
}