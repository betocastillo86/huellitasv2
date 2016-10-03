//-----------------------------------------------------------------------
// <copyright file="ContentService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Contents
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;
    using Exceptions;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Infraestructure;
    using Microsoft.EntityFrameworkCore;
    using Seo;

    /// <summary>
    /// Content Service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.Contents.IContentService" />
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1123:DoNotPlaceRegionsWithinElements", Justification = "Reviewed.")]
    public class ContentService : IContentService
    {
        /// <summary>
        /// The content attribute repository
        /// </summary>
        private readonly IRepository<ContentAttribute> contentAttributeRepository;

        /// <summary>
        /// The content repository
        /// </summary>
        private readonly IRepository<Content> contentRepository;

        /// <summary>
        /// The <c>seo</c> service
        /// </summary>
        private readonly ISeoService seoService;

        /// <summary>
        /// The context/
        /// </summary>
        private readonly HuellitasContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentService"/> class.
        /// </summary>
        /// <param name="contentRepository">The content repository.</param>
        /// <param name="contentAttributeRepository">The content attribute repository.</param>
        /// <param name="seoService">The <c>seo</c> service.</param>
        /// <param name="context">The context.</param>
        public ContentService(
            IRepository<Content> contentRepository,
            IRepository<ContentAttribute> contentAttributeRepository,
            ISeoService seoService,
            HuellitasContext context)
        {
            this.contentRepository = contentRepository;
            this.contentAttributeRepository = contentAttributeRepository;
            this.context = context;
            this.seoService = seoService;
        }
        
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the value</returns>
        public Content GetById(int id)
        {
            return this.contentRepository.GetById(id);
        }

        /// <summary>
        /// Inserts the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        public void Insert(Content content)
        {
            content.CreatedDate = DateTime.Now;
            content.FriendlyName = this.seoService.GenerateFriendlyName(content.Name, this.contentRepository.TableNoTracking);

            try
            {
                this.contentRepository.Insert(content);
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException is System.Data.SqlClient.SqlException)
                {
                    var inner = (System.Data.SqlClient.SqlException)e.InnerException;

                    if (inner.Number == 547)
                    {
                        string target = string.Empty;

                        if (inner.Message.IndexOf("FK_Content_Location") != -1)
                        {
                            target = "Location";
                        }
                        else if (inner.Message.IndexOf("FK_Content_File") != -1 || inner.Message.IndexOf("FK_ContentFile_File") != -1)
                        {
                            target = "File";
                        }
                        else
                        {
                            throw;
                        }

                        throw new HuellitasException(target, HuellitasExceptionCode.InvalidForeignKey);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Searches the specified keyword.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="attributesFilter">The attributes filter.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="page">The page.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>the value</returns>
        public IPagedList<Content> Search(
            string keyword = null,
            ContentType? contentType = null,
            IList<FilterAttribute> attributesFilter = null,
            int pageSize = int.MaxValue,
            int page = 0,
            ContentOrderBy orderBy = ContentOrderBy.DisplayOrder)
        {
            var query = this.contentRepository.TableNoTracking
                .Include(c => c.ContentAttributes)
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
                ////queryAttributes.AppendLine("SELECT ContentId as Id, NULL as Name, NULL as Body, 0 as TypeId, 0 as Status, 0 as UserId, NULL as CreatedDate, 0 as DisplayOrder, 0 as CommentsCount, 0 as Featured, 0 as Deleted, NULL as Email, NULL as FileId, NULL as UpdatedDate, NULL as Views, NULL as LocationId  FROM (");
                strQueryAttributes.AppendLine("SELECT ContentId as Id, ContentId, NULL as Attribute, NULL as Value FROM (");
                strQueryAttributes.AppendLine("SELECT count(ContentId) as countContents, contentId");
                strQueryAttributes.AppendLine(" FROM ContentAttribute");
                strQueryAttributes.AppendLine("WHERE");

                ////REV:int countAdditionalOptions = 0;
                for (int i = 0; i < attributesFilter.Count; i++)
                {
                    var attribute = attributesFilter.ElementAt(i);

                    if (i > 0)
                    {
                        strQueryAttributes.Append(" or ");
                    }

                    switch (attribute.FilterType)
                    {
                        case FilterAttributeType.Equals:
                            strQueryAttributes.Append($" (Attribute = '{attribute.Attribute}' and Value = '{attribute.Value}')");
                            break;

                        case FilterAttributeType.Range:
                            strQueryAttributes.Append($" (Attribute = '{attribute.Attribute}' and Value between '{attribute.Value}' and '{attribute.ValueTo}')");
                            break;

                        case FilterAttributeType.Multiple:

                            int countOptions = 0;

                            var values = (string[])attribute.Value;

                            strQueryAttributes.Append("(");

                            ////Toma cada una de las opciones y las agrega al filtro
                            foreach (var optionValue in values)
                            {
                                if (countOptions > 0)
                                {
                                    strQueryAttributes.Append(" or ");
                                }

                                strQueryAttributes.Append($" (Attribute = '{attribute.Attribute}' and Value = '{optionValue}')");
                                countOptions++;
                            }

                            ////Suma la cantidad de opciones adicionales para poder coincidir el final de busquedas con el mismo numero de criterios
                            ////REV:countAdditionalOptions += countOptions - 1;
                            strQueryAttributes.Append(")");
                            break;

                        default:
                            break;
                    }
                }

                strQueryAttributes.AppendLine(" GROUP BY ContentId");

                strQueryAttributes.Append($") as f where	f.countContents = {attributesFilter.Count }");

                var queryAttributes = this.context.ContentAttributes.FromSql(strQueryAttributes.ToString());
                var contentsFromAttributes = queryAttributes.Select(c => c.ContentId).ToList();

                if (contentsFromAttributes.Count > 0)
                {
                    query = query.Where(c => contentsFromAttributes.Contains(c.Id));
                }
                else
                {
                    ////Si no se encuentra ninguna coincidencia es porque no hay resultados
                    return new PagedList<Content>();
                }
            }

            #endregion Attributes

            switch (orderBy)
            {
                case ContentOrderBy.Name:
                    query = query.OrderBy(c => c.DisplayOrder);
                    break;

                case ContentOrderBy.CreatedDate:
                    query = query.OrderByDescending(c => c.CreatedDate);
                    break;

                case ContentOrderBy.DisplayOrder:
                default:
                    query = query.OrderBy(c => c.DisplayOrder);
                    break;
            }

            return new PagedList<Content>(query, page, pageSize);
        }
    }
}