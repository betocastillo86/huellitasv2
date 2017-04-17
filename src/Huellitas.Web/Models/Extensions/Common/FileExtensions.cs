//-----------------------------------------------------------------------
// <copyright file="FileExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Extensions
{
    using System;
    using System.Collections.Generic;
    using Business.Services;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api;

    /// <summary>
    /// File Extensions
    /// </summary>
    public static class FileExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileHelper">The file helper.</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="thumbnailWidth">Width of the thumbnail.</param>
        /// <param name="thumbnailHeight">Height of the thumbnail.</param>
        /// <returns>the model</returns>
        public static FileModel ToModel(
            this File file,
            IFilesHelper fileHelper,
            Func<string, string> contentUrlFunction = null,
            int width = 0,
            int height = 0,
            int thumbnailWidth = 0,
            int thumbnailHeight = 0)
        {
            string thumbnail = null;
            if (thumbnailHeight > 0 && thumbnailWidth > 0)
            {
                thumbnail = fileHelper.GetFullPath(file, contentUrlFunction, thumbnailWidth, thumbnailHeight);
            }

            return new Api.FileModel()
            {
                Id = file.Id,
                FileName = fileHelper.GetFullPath(file, contentUrlFunction, width, height),
                Name = file.Name,
                Thumbnail = thumbnail
            };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="fileHelper">The file helper.</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="thumbnailWidth">Width of the thumbnail.</param>
        /// <param name="thumbnailHeight">Height of the thumbnail.</param>
        /// <returns>the model</returns>
        public static IList<FileModel> ToModels(
            this IList<File> files,
            IFilesHelper fileHelper, 
            Func<string, string> contentUrlFunction = null,
            int width = 0,
            int height = 0,
            int thumbnailWidth = 0,
            int thumbnailHeight = 0)
        {
            var list = new List<FileModel>();

            foreach (var file in files)
            {
                list.Add(file.ToModel(fileHelper, contentUrlFunction, width, height, thumbnailWidth, thumbnailHeight));
            }

            return list;
        }
    }
}