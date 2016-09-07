using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Huellitas.Data.Entities;
using Huellitas.Data.Infraestructure;
using Huellitas.Data.Core;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Huellitas.Business.Services.Contents
{
    public class ContentService : IContentService
    {
        #region props
        private readonly HuellitasContext _context;
        private readonly IRepository<Content> _contentRepository;
        private readonly IRepository<ContentAttribute> _contentAttributeRepository;
        #endregion

        #region ctor
        public ContentService(IRepository<Content> contentRepository,
            IRepository<ContentAttribute> contentAttributeRepository,
            HuellitasContext context)
        {
            _contentRepository = contentRepository;
            _contentAttributeRepository = contentAttributeRepository;
            _context = context;
        }
        #endregion

        public IPagedList<Content> Search(string keyword = null,
            ContentType? contentType = null, 
            IList<FilterAttribute> attributesFilter = null, 
            int pageSize = int.MaxValue, 
            int page = 0)
        {

            var query = _contentRepository.TableNoTracking
                .Where(c => !c.Deleted);

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => c.Name.Contains(keyword) || c.Body.Contains(keyword));
            }

            if (contentType.HasValue)
            {
                var typeId = Convert.ToInt16(contentType);
                query = query.Where(c => c.TypeId == typeId);
            }

            #region Attributes
            if (attributesFilter != null && attributesFilter.Count > 0)
            {
                var strQueryAttributes = new StringBuilder();
                //queryAttributes.AppendLine("SELECT ContentId as Id, NULL as Name, NULL as Body, 0 as TypeId, 0 as Status, 0 as UserId, NULL as CreatedDate, 0 as DisplayOrder, 0 as CommentsCount, 0 as Featured, 0 as Deleted, NULL as Email, NULL as FileId, NULL as UpdatedDate, NULL as Views, NULL as LocationId  FROM (");
                strQueryAttributes.AppendLine("SELECT ContentId as Id, ContentId, NULL as Attribute, NULL as Value FROM (");
                strQueryAttributes.AppendLine("SELECT count(ContentId) as countContents, contentId");
                strQueryAttributes.AppendLine(" FROM ContentAttribute");
                strQueryAttributes.AppendLine("WHERE");

                //REV:int countAdditionalOptions = 0;
                for (int iAttribute = 0; iAttribute < attributesFilter.Count; iAttribute++)
                {
                    var attribute = attributesFilter.ElementAt(iAttribute);

                    if (iAttribute > 0)
                        strQueryAttributes.Append(" or ");

                    switch (attribute.FilterType)
                    {
                        case FilterAttributeType.Equals:
                            strQueryAttributes.Append($" (Attribute = '{attribute.Attribute}' and Value = '{attribute.Value}')");
                            //queryAttributes =  queryAttributes.Union(_contentAttributeRepository.TableNoTracking.Where(ca => ca.Attribute.Equals(attribute.Attribute.ToString()) && ca.Value.Equals(attribute.Value)));
                            break;
                        case FilterAttributeType.Range:
                            strQueryAttributes.Append($" (Attribute = '{attribute.Attribute}' and Value between '{attribute.Value}' and '{attribute.ValueTo}')");
                            //int valueFrom = Convert.ToInt32(attribute.Value);
                            //int valueTo = Convert.ToInt32(attribute.ValueTo);
                            //queryAttributes = queryAttributes.Union(_contentAttributeRepository.TableNoTracking.Where(ca => ca.Attribute.Equals(attribute.Attribute.ToString()) && (Convert.ToInt32(ca.Value) >= valueFrom && Convert.ToInt32(ca.Value) <= valueTo) ));
                            break;
                        case FilterAttributeType.Multiple:

                            int countOptions = 0;

                            var values = (string[])attribute.Value;

                            strQueryAttributes.Append("(");
                            //Toma cada una de las opciones y las agrega al filtro
                            foreach (var optionValue in values)
                            {
                                if (countOptions > 0) strQueryAttributes.Append(" or ");
                                strQueryAttributes.Append($" (Attribute = '{attribute.Attribute}' and Value = '{optionValue}')");
                                countOptions++;
                            }
                            //Suma la cantidad de opciones adicionales para poder coincidir el final de busquedas con el mismo numero de criterios
                            //REV:countAdditionalOptions += countOptions - 1;
                            strQueryAttributes.Append(")");

                            //queryAttributes = queryAttributes.Union(_contentAttributeRepository.TableNoTracking.Where(ca => ca.Attribute.Equals(attribute.Attribute.ToString()) && values.Contains(ca.Value)));
                            break;
                        default:
                            break;
                    }
                }

                strQueryAttributes.AppendLine(" GROUP BY ContentId");

                strQueryAttributes.Append($") as f where	f.countContents = {attributesFilter.Count }");



                var queryAttributes = _context.ContentAttributes.FromSql(strQueryAttributes.ToString());
                var contentsFromAttributes = queryAttributes.Select(c => c.ContentId).ToList();

                
                if (contentsFromAttributes.Count > 0)
                {
                    query = query.Where(c => contentsFromAttributes.Contains(c.Id));
                }
                else
                {
                    //Si no se encuentra ninguna coincidencia es porque no hay resultados
                    return new PagedList<Content>();
                }

                //var y = x.ToList();
                //var x = queryAttributes.ToList();
                //query = query.Where(c => queryAttributes.Select(ca => ca.ContentId).Contains(c.Id));
            }
            #endregion


            return new PagedList<Content>(query, page, pageSize);
        }
    }
}
