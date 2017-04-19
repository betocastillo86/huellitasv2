//-----------------------------------------------------------------------
// <copyright file="BannerExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Extensions
{
    using System;
    using System.Collections.Generic;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api;

    /// <summary>
    /// Banner Extensions
    /// </summary>
    public static class BannerExtensions
    {
        /// <summary>
        /// To the entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the banner entity</returns>
        public static Banner ToEntity(this BannerModel model)
        {
            var entity = new Banner()
            {
                Id = model.Id,
                Body = model.Body,
                FileId = model.FileId,
                Name = model.Name,
                Section = model.Section.Value,
                DisplayOrder = model.DisplayOrder,
                Active = model.Active,
                EmbedUrl = model.EmbedUrl
            };

            return entity;
        }

        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="fileHelper">The file helper.</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="imageWidth">Width of the image.</param>
        /// <param name="imageHeight">Height of the image.</param>
        /// <returns>the model</returns>
        public static BannerModel ToModel(
            this Banner entity,
            IFilesHelper fileHelper,
            Func<string, string> contentUrlFunction = null,
            int imageWidth = 0,
            int imageHeight = 0)
        {
            var model = new BannerModel()
            {
                Id = entity.Id,
                Body = entity.Body,
                Section = entity.Section,
                Name = entity.Name,
                Active = entity.Active
            };

            if (entity.FileId.HasValue)
            {
                model.FileUrl = fileHelper.GetFullPath(entity.File, contentUrlFunction, imageWidth, imageHeight);
                model.FileId = entity.FileId;
            }
            else
            {
                model.EmbedUrl = entity.EmbedUrl;
            }

            return model;
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="fileHelper">file helper</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="imageWidth">Width of the image.</param>
        /// <param name="imageHeight">Height of the image</param>
        /// <returns>the models</returns>
        public static IList<BannerModel> ToModels(
            this IList<Banner> entities,
            IFilesHelper fileHelper,
            Func<string, string> contentUrlFunction = null,
            int imageWidth = 0,
            int imageHeight = 0)
        {
            var list = new List<BannerModel>();
            foreach (var entity in entities)
            {
                list.Add(entity.ToModel(fileHelper, contentUrlFunction, imageWidth, imageHeight));
            }

            return list;
        }
    }
}