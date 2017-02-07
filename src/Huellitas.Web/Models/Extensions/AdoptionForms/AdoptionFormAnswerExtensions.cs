//-----------------------------------------------------------------------
// <copyright file="AdoptionFormAnswerExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Extensions.AdoptionForms
{
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api.AdoptionForms;

    /// <summary>
    /// Adoption Form Attribute Extensions
    /// </summary>
    public static class AdoptionFormAnswerExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the model</returns>
        public static AdoptionFormAnswerModel ToModel(this AdoptionFormAnswer entity)
        {
            return new AdoptionFormAnswerModel
            {
                Id = entity.Id,
                AdditionalInfo = entity.AdditionalInfo,
                AdoptionFormId = entity.AdoptionFormId,
                CreationDate = entity.CreationDate,
                Notes = entity.Notes,
                Status = entity.StatusEnum,
                User = entity.User.ToBaseUserModel()
            };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>the models</returns>
        public static IList<AdoptionFormAnswerModel> ToModels(this ICollection<AdoptionFormAnswer> entities)
        {
            return entities.Select(ToModel).ToList();
        }
    }
}