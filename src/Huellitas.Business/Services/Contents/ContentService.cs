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
    using Data.Entities.Enums;
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
        /// The content file repository
        /// </summary>
        private readonly IRepository<ContentFile> contentFileRepository;

        /// <summary>
        /// The content repository
        /// </summary>
        private readonly IRepository<Content> contentRepository;

        /// <summary>
        /// The context/
        /// </summary>
        private readonly HuellitasContext context;

        /// <summary>
        /// The related content repository
        /// </summary>
        private readonly IRepository<RelatedContent> relatedContentRepository;

        /// <summary>
        /// The <c>seo</c> service
        /// </summary>
        private readonly ISeoService seoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentService"/> class.
        /// </summary>
        /// <param name="contentRepository">The content repository.</param>
        /// <param name="contentAttributeRepository">The content attribute repository.</param>
        /// <param name="contentFileRepository">The content file repository.</param>
        /// <param name="seoService">The SEO service.</param>
        /// <param name="context">The context.</param>
        /// <param name="relatedContentRepository">The related content repository.</param>
        public ContentService(
            IRepository<Content> contentRepository,
            IRepository<ContentAttribute> contentAttributeRepository,
            IRepository<ContentFile> contentFileRepository,
            ISeoService seoService,
            HuellitasContext context,
            IRepository<RelatedContent> relatedContentRepository)
        {
            this.contentRepository = contentRepository;
            this.contentAttributeRepository = contentAttributeRepository;
            this.context = context;
            this.seoService = seoService;
            this.contentFileRepository = contentFileRepository;
            this.relatedContentRepository = relatedContentRepository;
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeLocation">Includes the location in the query</param>
        /// <returns>
        /// the value
        /// </returns>
        public Content GetById(int id, bool includeLocation = false)
        {
            var query = this.contentRepository.Table
                .Include(c => c.ContentAttributes)
                .Include(c => c.File)
                .Include(c => c.User)
                .Where(c => c.Id == id && !c.Deleted);

            if (includeLocation)
            {
                query = query.Include(c => c.Location);
            }

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Gets the files of a content
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <returns>
        /// the files
        /// </returns>
        public IList<ContentFile> GetFiles(int contentId)
        {
            return this.contentFileRepository.TableNoTracking
                .Include(c => c.File)
                .Where(c => c.ContentId == contentId)
                .ToList();
        }

        /// <summary>
        /// Gets the related contents by type optional
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="relation">The relation.</param>
        /// <param name="page">the page</param>
        /// <param name="pageSize">the page size</param>
        /// <returns>
        /// List of related contents by type
        /// </returns>
        public IPagedList<Content> GetRelated(
            int id,
            RelationType? relation = null,
            int page = 0,
            int pageSize = int.MaxValue)
        {
            var query = this.relatedContentRepository.Table
                .Include(c => c.RelatedContentNavigation)
                .Where(c => c.ContentId == id || c.RelatedContentId == id);

            if (relation.HasValue)
            {
                var relationId = Convert.ToInt16(relation);
                query = query.Where(c => c.RelationType == relationId);
            }

            var querySelect = query.Select(c => c.ContentId == id ? c.RelatedContentNavigation : c.Content);

            return new PagedList<Content>(querySelect, page, pageSize);
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
                .Include(c => c.Location)
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