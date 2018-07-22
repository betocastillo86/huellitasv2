//-----------------------------------------------------------------------
// <copyright file="CustomTableRowServiceExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Extensions
{
    using System;
    using System.Collections.Generic;
    using Huellitas.Business.Models;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;
    using Huellitas.Business.Caching;
    using Beto.Core.Caching;

    /// <summary>
    /// Custom table row service extensions
    /// </summary>
    public static class CustomTableRowServiceExtensions
    {
        /// <summary>
        /// Gets the adoption form questions.
        /// </summary>
        /// <param name="customTableRowService">The custom table row service.</param>
        /// <returns>list of questions</returns>
        public static IList<AdoptionFormQuestionModel> GetAdoptionFormQuestions(this ICustomTableService customTableRowService, ICacheManager cacheManager)
        {
            return cacheManager.Get(CacheKeys.CUSTOMTABLEROWS_ADOPTIONFORM_QUESTIONS, () =>
            {
                var rows = customTableRowService.GetRowsByTableId(Convert.ToInt32(CustomTableType.QuestionAdoptionForm));

                var models = new List<AdoptionFormQuestionModel>();

                foreach (var row in rows)
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
                    model.DisplayOrder = row.DisplayOrder;

                    models.Add(model);
                }

                return models;
            });
        }
    }
}