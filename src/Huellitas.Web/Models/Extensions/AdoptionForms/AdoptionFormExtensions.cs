//-----------------------------------------------------------------------
// <copyright file="AdoptionFormExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Beto.Core.Data.Files;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api;

    /// <summary>
    /// Adoption Forms Extensions
    /// </summary>
    public static class AdoptionFormExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="filesHelper">The files helper.</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>the return</returns>
        public static AdoptionFormModel ToModel(
            this AdoptionForm entity,
            IFilesHelper filesHelper,
            Func<string, string> contentUrlFunction = null,
            int width = 0,
            int height = 0)
        {
            var model = new AdoptionFormModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                CreationDate = entity.CreationDate,
                Address = entity.Address,
                BirthDate = entity.BirthDate,
                FamilyMembers = entity.FamilyMembers,
                PhoneNumber = entity.PhoneNumber,
                Town = entity.Town,
                Status = entity.LastStatusEnum,
                Job = new ContentAttributeModel<int> { Value = entity.JobId },
                Location = new LocationModel { Id = entity.LocationId },
                ContentId = entity.ContentId,
                FamilyMembersAge = entity.FamilyMembersAge
            };

            if (entity.Location != null)
            {
                model.Location.Name = entity.Location.Name;
            }

            if (entity.Job != null)
            {
                model.Job.Text = entity.Job.Value;
            }

            if (entity.Content != null)
            {
                model.Content = entity.Content.ToModel(filesHelper, contentUrlFunction, width, height);
            }

            model.User = entity.User?.ToBaseUserModel();
            model.LastResponseUser = entity.LastResponseUser?.ToBaseUserModel();
            model.ResponseDate = entity.ReponseDate;

            return model;
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="filesHelper">The files helper.</param>
        /// <param name="contentUrlFunction">The content URL function.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>the return</returns>
        public static IList<AdoptionFormModel> ToModels(
            this ICollection<AdoptionForm> entities,
            IFilesHelper filesHelper,
            Func<string, string> contentUrlFunction = null,
            int width = 0,
            int height = 0)
        {
            return entities.Select(c => c.ToModel(filesHelper, contentUrlFunction, width, height)).ToList();
        }

        /// <summary>
        /// To the entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the entity</returns>
        public static AdoptionForm ToEntity(this AdoptionFormModel model)
        {
            var entity = new AdoptionForm
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                CreationDate = model.CreationDate,
                Address = model.Address,
                BirthDate = model.BirthDate.Value,
                FamilyMembers = model.FamilyMembers.Value,
                PhoneNumber = model.PhoneNumber,
                Town = model.Town,
                LastStatusEnum = model.Status,
                JobId = model.Job.Value,
                LocationId = model.Location.Id,
                ContentId = model.ContentId.Value,
                FamilyMembersAge = model.FamilyMembersAge
            };

            if (model.Attributes?.Count > 0)
            {
                entity.Attributes = model.Attributes.ToEntities();
            }

            return entity;
        }
    }
}