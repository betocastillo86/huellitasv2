//-----------------------------------------------------------------------
// <copyright file="PictureResizer.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using Beto.Core.Data.Files;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Processing;
    using SixLabors.ImageSharp.Processing.Transforms;
    using SixLabors.Primitives;

    /// <summary>
    /// Picture Resizer
    /// </summary>
    /// <seealso cref="Beto.Core.Data.Files.ICorePictureResizerService" />
    public class PictureResizer : ICorePictureResizerService
    {
        /// <summary>
        /// Resizes the picture.
        /// </summary>
        /// <param name="resizedPath">The resized path.</param>
        /// <param name="originalPath">The original path.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="mode">the mode of resizing</param>
        public void ResizePicture(string resizedPath, string originalPath, int width, int height, Beto.Core.Data.Files.ResizeMode mode = Beto.Core.Data.Files.ResizeMode.Crop)
        {
            var resizeOptions = new ResizeOptions
            {
                Size = new Size { Width = width, Height = height },
                Mode = this.GetResizeMode(mode)
            };

            using (var image = Image.Load(originalPath))
            {
                image.Mutate(c => c.AutoOrient()
                                    .Resize(resizeOptions));

                image.Save(resizedPath);
            }
        }

        /// <summary>
        /// Resizes the picture.
        /// </summary>
        /// <param name="contentFile">The content file.</param>
        /// <param name="resizedPath">The resized path.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="mode">the mode of resizing</param>
        public void ResizePicture(byte[] contentFile, string resizedPath, int width, int height, Beto.Core.Data.Files.ResizeMode mode = Beto.Core.Data.Files.ResizeMode.Crop)
        {
            var resizeOptions = new ResizeOptions
            {
                Size = new Size { Width = width, Height = height },
                Mode = this.GetResizeMode(mode)
            };

            using (var image = Image.Load(contentFile))
            {
                image.Mutate(c => c.AutoOrient()
                                    .Resize(resizeOptions));

                image.Save(resizedPath);
            }
        }

        /// <summary>
        /// Gets the resize mode.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns>the six labors mode</returns>
        private SixLabors.ImageSharp.Processing.Transforms.ResizeMode GetResizeMode(Beto.Core.Data.Files.ResizeMode mode)
        {
            switch (mode)
            {
                case Beto.Core.Data.Files.ResizeMode.Crop:
                    return SixLabors.ImageSharp.Processing.Transforms.ResizeMode.Crop;

                case Beto.Core.Data.Files.ResizeMode.Pad:
                    return SixLabors.ImageSharp.Processing.Transforms.ResizeMode.Pad;

                case Beto.Core.Data.Files.ResizeMode.BoxPad:
                    return SixLabors.ImageSharp.Processing.Transforms.ResizeMode.BoxPad;

                case Beto.Core.Data.Files.ResizeMode.Max:
                    return SixLabors.ImageSharp.Processing.Transforms.ResizeMode.Max;

                case Beto.Core.Data.Files.ResizeMode.Min:
                    return SixLabors.ImageSharp.Processing.Transforms.ResizeMode.Min;

                default:
                    return SixLabors.ImageSharp.Processing.Transforms.ResizeMode.Crop;
            }
        }
    }
}