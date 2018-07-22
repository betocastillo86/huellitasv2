//-----------------------------------------------------------------------
// <copyright file="ContentSettings.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Configuration
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Beto.Core.Data.Configuration;
    using Huellitas.Business.Extensions.Services;
    using Huellitas.Business.Services;

    /// <summary>
    /// Content Settings
    /// </summary>
    /// <seealso cref="Huellitas.Business.Configuration.IContentSettings" />
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1502:ElementMustNotBeOnSingleLine", Justification = "Reviewed.")]
    public class ContentSettings : IContentSettings
    {
        /// <summary>
        /// The setting service
        /// </summary>
        private readonly ICoreSettingService settingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentSettings"/> class.
        /// </summary>
        /// <param name="settingService">The setting service.</param>
        public ContentSettings(ICoreSettingService settingService)
        {
            this.settingService = settingService;
        }

        /// <summary>
        /// Gets the picture size width detail.
        /// </summary>
        /// <value>
        /// The picture size width detail.
        /// </value>
        public int PictureSizeWidthDetail { get { return this.settingService.Get<int>("ContentSettings.PictureSizeWidthDetail"); } }

        /// <summary>
        /// Gets the picture size height detail.
        /// </summary>
        /// <value>
        /// The picture size height detail.
        /// </value>
        public int PictureSizeHeightDetail { get { return this.settingService.Get<int>("ContentSettings.PictureSizeHeightDetail"); } }

        /// <summary>
        /// Gets the picture size width list.
        /// </summary>
        /// <value>
        /// The picture size width list.
        /// </value>
        public int PictureSizeWidthList { get { return this.settingService.Get<int>("ContentSettings.PictureSizeWidthList"); } }

        /// <summary>
        /// Gets the picture size height list.
        /// </summary>
        /// <value>
        /// The picture size height list.
        /// </value>
        public int PictureSizeHeightList { get { return this.settingService.Get<int>("ContentSettings.PictureSizeHeightList"); } }

        /// <summary>
        /// Gets the days to automatic closing pet.
        /// </summary>
        /// <value>
        /// The days to automatic closing pet.
        /// </value>
        public int DaysToAutoClosingPet { get { return this.settingService.Get<int>("ContentSettings.DaysToAutoClosingPet"); } }
    }
}