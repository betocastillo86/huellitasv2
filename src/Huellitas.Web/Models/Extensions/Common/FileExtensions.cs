//-----------------------------------------------------------------------
// <copyright file="FileExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Extensions.Common
{
    using System;
    using System.Collections.Generic;
    using Business.Services.Files;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api.Files;

    /// <summary>
    /// File Extensions
    /// </summary>
    public static class FileExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileHelper">The file helper</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <returns>the model</returns>
        public static FileModel ToModel(this File file, IFilesHelper fileHelper, Func<string, string> contentUrlFunction = null)
        {
            return new Api.Files.FileModel()
            {
                Id = file.Id,
                FileName = fileHelper.GetFullPath(file, contentUrlFunction),
                Name = file.Name
            };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="fileHelper">The file helper</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <returns>the models</returns>
        public static IList<FileModel> ToModels(this IList<File> files, IFilesHelper fileHelper, Func<string, string> contentUrlFunction = null)
        {
            var list = new List<FileModel>();

            foreach (var file in files)
            {
                list.Add(file.ToModel(fileHelper, contentUrlFunction));
            }

            return list;
        }
    }
}