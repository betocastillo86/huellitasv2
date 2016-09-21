using Huellitas.Data.Entities;
using Huellitas.Web.Models.Api.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Web.Models.Extensions
{
    public static class PetExtensions
    {
        public static PetModel ToModel(this Content entity, Func<string, string> contentUrlFunction = null)
        {
            var model = new PetModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Body = entity.Body,
                CommentsCount = entity.CommentsCount,
                DisplayOrder = entity.DisplayOrder,
                Status = entity.StatusType,
                TypeId = entity.Type,
                Views = entity.Views
            };

            foreach (var attribute in entity.ContentAttributes)
            {
                switch (attribute.AttributeType)
                {
                    case ContentAttributeType.Subtype:
                        model.Subtype = new ContentAttributeModel<int>() { Text = "s", Value = Convert.ToInt32(attribute.Value) };
                        break;
                    case ContentAttributeType.Genre:
                        model.Genre = new ContentAttributeModel<int>() { Text = "a", Value = Convert.ToInt32(attribute.Value) };
                        break;
                    case ContentAttributeType.Age:
                        model.Moths = Convert.ToInt32(attribute.Value);
                        break;
                    case ContentAttributeType.Size:
                        model.Size = new ContentAttributeModel<int>() { Text = "a", Value = Convert.ToInt32(attribute.Value) };
                        break;
                    case ContentAttributeType.Shelter:
                        //model.Shelter = new ContentAttributeModel<int>() { Text = "a", Value = Convert.ToInt32(attribute.Value) };
                        break;
                    default:
                        break;
                }
            }

            //if (contentUrlFunction != null && entity.File != null)
            //{
            //    model.Image = entity.File.FileName
            //}

            return model;
        }

        public static IList<PetModel> ToModels(this IList<Content> entities, Func<string, string> contentUrlFunction = null)
        {
            var models = new List<PetModel>();
            foreach (var entity in entities)
            {
                models.Add(entity.ToModel(contentUrlFunction: contentUrlFunction));
            }
            return models;
        }
    }
}
