﻿//-----------------------------------------------------------------------
// <copyright file="PictureService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Extensions;
    using Huellitas.Business.Models;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Extensions;
    using ImageSharp;
    using ImageSharp.Processing;
    using Microsoft.AspNetCore.Hosting;
    using SixLabors.Fonts;

    /// <summary>
    /// Picture Service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.IPictureService" />
    public class PictureService : IPictureService
    {
        /// <summary>
        /// The file helper
        /// </summary>
        private readonly IFilesHelper fileHelper;

        /// <summary>
        /// The log service
        /// </summary>
        private readonly ILogService logService;

        /// <summary>
        /// The system setting service
        /// </summary>
        private readonly ISystemSettingService systemSettingService;

        /// <summary>
        /// The custom table service
        /// </summary>
        private readonly ICustomTableService customTableService;

        /// <summary>
        /// The host
        /// </summary>
        private readonly IHostingEnvironment host;

        /// <summary>
        /// The content service
        /// </summary>
        private readonly IContentService contentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PictureService"/> class.
        /// </summary>
        /// <param name="fileHelper">The file helper.</param>
        /// <param name="logService">The log service.</param>
        /// <param name="systemSettingService">The system setting service.</param>
        /// <param name="customTableService">Custom table service</param>
        /// <param name="contentService">The content service</param>
        public PictureService(
            IFilesHelper fileHelper,
            ILogService logService,
            ISystemSettingService systemSettingService,
            ICustomTableService customTableService,
            IHostingEnvironment host,
            IContentService contentService)
        {
            this.fileHelper = fileHelper;
            this.logService = logService;
            this.systemSettingService = systemSettingService;
            this.customTableService = customTableService;
            this.host = host;
            this.contentService = contentService;
        }

        /// <summary>
        /// Creates the social network post.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="file">The file.</param>
        /// <param name="color">The color.</param>
        /// <param name="network">The network.</param>
        /// <param name="contentUrlFunction">Content URL function</param>
        /// <returns>
        /// the new path of the image
        /// </returns>
        public async Task<string> CreateSocialNetworkPost(
            Content content, 
            File file, 
            SocialPostColors color = SocialPostColors.Blue, 
            SocialNetwork network = SocialNetwork.Facebook,
            Func<string, string> contentUrlFunction = null)
        {
            int width = this.systemSettingService.GetCachedSetting<int>($"GeneralSettings.PostImageWidth{network.ToString()}");
            int height = this.systemSettingService.GetCachedSetting<int>($"GeneralSettings.PostImageHeight{network.ToString()}");

            var newImagePath = this.fileHelper.GetPhysicalPath(file, width, height);

            int fontBigSize = 0;
            int fontSmallSize = 0;

            ImageSharp.Size sizeLogo;
            Point pointLogo;

            System.Numerics.Vector2 positionFontBig;
            System.Numerics.Vector2 positionFontSmall;

            switch (network)
            {
                case SocialNetwork.Instagram:
                    fontBigSize = 55;
                    fontSmallSize = 35;
                    sizeLogo = new ImageSharp.Size() { Width = 180, Height = 98 };
                    pointLogo = new Point() { X = width - 200, Y = 110 };
                    positionFontBig = new System.Numerics.Vector2() { X = 20, Y = height - 140 };
                    positionFontSmall = new System.Numerics.Vector2() { X = 20, Y = height - 70 };
                    break;
                default:
                case SocialNetwork.Facebook:
                    fontBigSize = 50;
                    fontSmallSize = 30;
                    sizeLogo = new ImageSharp.Size() { Width = 150, Height = 81 };
                    pointLogo = new Point() { X = width - 200, Y = height - 100 };
                    positionFontBig = new System.Numerics.Vector2() { X = 20, Y = height - 120 };
                    positionFontSmall = new System.Numerics.Vector2() { X = 20, Y = height - 50 };
                    break;
            }

            var fontCollection = new FontCollection();
            fontCollection.Install($"{this.host.WebRootPath}/fonts/Oswald-DemiBold.ttf");
            

            var resizeOptions = new ResizeOptions()
            {
                Size = new ImageSharp.Size { Width = width, Height = height },
                Mode = ResizeMode.Crop
            };

            var rgbColor = this.GetRgbColor(color);

            var genreText = this.customTableService.GetValueByCustomTableAndId(CustomTableType.AnimalGenre, content.GetAttribute<int>(ContentAttributeType.Genre));

            var phone = string.Empty;
            if (content.Type == ContentType.Pet)
            {
                var parent = this.contentService.GetUsersByContentId(content.Id, ContentUserRelationType.Parent, true)
                                                .FirstOrDefault();
                if (parent != null)
                {
                    phone = parent.User.PhoneNumber;
                }
                else if (!string.IsNullOrEmpty(content.GetAttribute<string>(ContentAttributeType.Shelter)))
                {
                    var shelter = this.contentService.GetShelterByPet(content.Id);
                    if (shelter != null)
                    {
                        phone = shelter.GetAttribute<string>(ContentAttributeType.Phone1);
                    }
                }
                else if (!string.IsNullOrEmpty(content.User?.PhoneNumber))
                {
                    phone = content.User.PhoneNumber;
                }
            }
            else if (content.Type == ContentType.LostPet)
            {
                if (!string.IsNullOrEmpty(content.User?.PhoneNumber))
                {
                    phone = content.User.PhoneNumber;
                }
            }
            else if (content.Type == ContentType.Shelter)
            {
                phone = content.GetAttribute<string>(ContentAttributeType.Phone1);
            }

            phone = !string.IsNullOrEmpty(phone) ? " - TEL:" + phone : string.Empty;

            using (var image = Image.Load(this.fileHelper.GetPhysicalPath(file)))
            {
                using (var logo = Image.Load($"{this.host.WebRootPath}/img/front/{this.GetLogoByColor(color)}"))
                {
                    var family = fontCollection.Families.FirstOrDefault();

                    image
                    .AutoOrient()
                    .Resize(resizeOptions)
                    .DrawPolygon(rgbColor, 125, new System.Numerics.Vector2[] { new System.Numerics.Vector2() { X = 0, Y = height - 62 }, new System.Numerics.Vector2() { X = width, Y = height - 62 } }, new GraphicsOptions() { BlenderMode = ImageSharp.PixelFormats.PixelBlenderMode.Normal, BlendPercentage = 90 })
                    .DrawText(content.Name.ToUpper(), new SixLabors.Fonts.Font(family, fontBigSize, FontStyle.Bold), Rgba32.White, positionFontBig, ImageSharp.Drawing.TextGraphicsOptions.Default)
                    .DrawText($"EDAD: {content.GetTextAge().ToUpper()} - {genreText.ToUpper()} - UBICACIÓN: {content.Location.Name.ToUpper()} {phone}", new SixLabors.Fonts.Font(family, fontSmallSize, FontStyle.Italic), Rgba32.White, positionFontSmall, ImageSharp.Drawing.TextGraphicsOptions.Default)
                    .DrawImage(logo, 100, sizeLogo, pointLogo)
                    .Save(newImagePath);
                }
            }

            await Task.FromResult(0);


            return this.fileHelper.GetFullPath(file, contentUrlFunction, width, height);
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
            try
            {
                using (var image = Image.Load(pathOriginalFile))
                {
                    var resizeOptions = new ResizeOptions()
                    {
                        Size = new ImageSharp.Size { Width = width, Height = height },
                        Mode = ResizeMode.Crop
                    };

                    image
                        .AutoOrient()
                        .Resize(resizeOptions)
                        .Save(resizedPath);
                }
            }
            catch (System.Exception e)
            {
                this.logService.Error(e);
            }
        }

        /// <summary>
        /// Gets the color of the RGB.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>the RGBA32 color</returns>
        private Rgba32 GetRgbColor(SocialPostColors color)
        {
            switch (color)
            {
                case SocialPostColors.Pink:
                    return new Rgba32(252, 146, 130);

                case SocialPostColors.Green:
                    return new Rgba32(184, 234, 129);

                case SocialPostColors.DarkBlue:
                    return new Rgba32(60, 117, 194);

                case SocialPostColors.Violet:
                    return new Rgba32(185, 107, 254);
                default:
                case SocialPostColors.Blue:
                    return new Rgba32(124, 210, 225);
            }
        }

        /// <summary>
        /// Gets the color of the logo by.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>the name of the logo image</returns>
        private string GetLogoByColor(SocialPostColors color)
        {
            switch (color)
            {
                case SocialPostColors.DarkBlue:
                case SocialPostColors.Violet:
                    return "logohuellitas-blanco.png";
                case SocialPostColors.Green:
                case SocialPostColors.Blue:
                case SocialPostColors.Pink:
                default:
                    return "logohuellitas.png";
            }
        }
    }
}