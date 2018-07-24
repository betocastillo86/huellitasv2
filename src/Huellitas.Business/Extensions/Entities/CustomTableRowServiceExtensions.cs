//-----------------------------------------------------------------------
// <copyright file="CustomTableRowServiceExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Beto.Core.Caching;
    using Huellitas.Business.Caching;
    using Huellitas.Business.Models;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Custom table row service extensions
    /// </summary>
    public static class CustomTableRowServiceExtensions
    {
        /// <summary>
        /// Gets the adoption form questions.
        /// </summary>
        /// <param name="customTableRowService">The custom table row service.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <returns>the return</returns>
        public static IList<AdoptionFormQuestionModel> GetAdoptionFormQuestions(this ICustomTableService customTableRowService, ICacheManager cacheManager)
        {
            return cacheManager.Get(
                CacheKeys.CUSTOMTABLEROWS_ADOPTIONFORM_QUESTIONS,
                () =>
            {
                return customTableRowService
                    .GetRowsByTableId(Convert.ToInt32(CustomTableType.QuestionAdoptionForm))
                    .ToAdoptionFormQuestionModels();
            });
        }

        /// <summary>
        /// To the adoption form question model.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns>the model</returns>
        public static AdoptionFormQuestionModel ToAdoptionFormQuestionModel(this CustomTableRow row)
        {
            var model = new AdoptionFormQuestionModel()
            {
                Id = row.Id,
                Question = row.Value,
                QuestionParentId = row.ParentCustomTableRowId
            };

            var additionalInfo = row.AdditionalInfo.Split(new char[] { '|' });
            model.QuestionType = (AdoptionFormQuestionType)Enum.Parse(typeof(AdoptionFormQuestionType), additionalInfo[0]);

            if (!string.IsNullOrEmpty(additionalInfo[1]))
            {
                model.Options = additionalInfo[1].Split(new char[] { ',' });
            }
            else
            {
                model.Options = new string[0];
            }

            model.Required = Convert.ToBoolean(additionalInfo[2]);
            model.Recommendations = additionalInfo[3];
            model.DisplayOrder = row.DisplayOrder;

            return model;
        }

        /// <summary>
        /// To the adoption form question models.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <returns>the models</returns>
        public static IList<AdoptionFormQuestionModel> ToAdoptionFormQuestionModels(this ICollection<CustomTableRow> rows)
        {
            return rows.Select(ToAdoptionFormQuestionModel).ToList();
        }
    }
}