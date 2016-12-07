//-----------------------------------------------------------------------
// <copyright file="IContentSettings.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Configuration
{
    /// <summary>
    /// Interface of Content Settings
    /// </summary>
    public interface IContentSettings
    {
        /// <summary>
        /// Gets the picture size width detail.
        /// </summary>
        /// <value>
        /// The picture size width detail.
        /// </value>
        int PictureSizeWidthDetail { get; }

        /// <summary>
        /// Gets the picture size height detail.
        /// </summary>
        /// <value>
        /// The picture size height detail.
        /// </value>
        int PictureSizeHeightDetail { get; }

        /// <summary>
        /// Gets the picture size width list.
        /// </summary>
        /// <value>
        /// The picture size width list.
        /// </value>
        int PictureSizeWidthList { get; }

        /// <summary>
        /// Gets the picture size height list.
        /// </summary>
        /// <value>
        /// The picture size height list.
        /// </value>
        int PictureSizeHeightList { get; }
    }
}