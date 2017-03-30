//-----------------------------------------------------------------------
// <copyright file="ContentService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Contents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data.Entities.Enums;
    using EventPublisher;
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
        /// The content user repository
        /// </summary>
        private readonly IRepository<ContentUser> contentUserRepository;

        /// <summary>
        /// The context/
        /// </summary>
        private readonly HuellitasContext context;

        /// <summary>
        /// The publisher
        /// </summary>
        private readonly IPublisher publisher;

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
        /// <param name="contentUserRepository">The content user repository.</param>
        /// <param name="publisher">the publisher</param>
        public ContentService(
            IRepository<Content> contentRepository,
            IRepository<ContentAttribute> contentAttributeRepository,
            IRepository<ContentFile> contentFileRepository,
            ISeoService seoService,
            HuellitasContext context,
            IRepository<RelatedContent> relatedContentRepository,
            IRepository<ContentUser> contentUserRepository,
            IPublisher publisher)
        {
            this.contentRepository = contentRepository;
            this.contentAttributeRepository = contentAttributeRepository;
            this.context = context;
            this.seoService = seoService;
            this.contentFileRepository = contentFileRepository;
            this.relatedContentRepository = relatedContentRepository;
            this.contentUserRepository = contentUserRepository;
            this.publisher = publisher;
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
        /// Gets the content attribute.
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns>
        /// the value
        /// </returns>
        public T GetContentAttribute<T>(int contentId, ContentAttributeType attribute)
        {
            var attributeName = attribute.ToString();
            var attributeValue = this.contentAttributeRepository.Table
                .Where(c => c.Attribute.Equals(attributeName) && c.ContentId.Equals(contentId))
                .Select(c => c.Value)
                .FirstOrDefault();

            if (attributeValue != null)
            {
                var destinationConverter = TypeDescriptor.GetConverter(typeof(T));
                return (T)destinationConverter.ConvertFrom(null, System.Globalization.CultureInfo.InvariantCulture, attributeValue);
            }
            else
            {
                return default(T);
            }
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
                .Where(c => c.ContentId == id || c.RelatedContentId == id);

            if (relation.HasValue)
            {
                var relationId = Convert.ToInt16(relation);
                query = query.Where(c => c.RelationType == relationId);
            }

            var queryIds = query.Select(c => c.ContentId == id ? c.RelatedContentId : c.ContentId);

            var finalQuery = this.contentRepository.Table
                .Include(c => c.Location)
                .Include(c => c.ContentAttributes)
                .Where(c => queryIds.Contains(c.Id));

            return new PagedList<Content>(finalQuery, page, pageSize);
        }

        /// <summary>
        /// Gets the users by content identifier.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="relation">The relation.</param>
        /// <param name="loadUser">load user info</param>
        /// <param name="page">the page</param>
        /// <param name="pageSize">the page size</param>
        /// <returns>
        /// the list of users
        /// </returns>
        public IPagedList<ContentUser> GetUsersByContentId(
            int contentId,
            ContentUserRelationType? relation = null,
            bool loadUser = false,
            int page = 0,
            int pageSize = int.MaxValue)
        {
            var query = this.contentUserRepository.Table;

            if (loadUser)
            {
                query = query.Include(c => c.User);
            }
                
            query = query.Where(c => c.ContentId == contentId);

            if (relation.HasValue)
            {
                var relationId = Convert.ToDecimal(relation.Value);
                query = query.Where(c => c.RelationTypeId == relationId);
            }

            query = query.OrderBy(c => c.Id);

            return new PagedList<ContentUser>(query, page, pageSize);
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>the task</returns>
        /// <exception cref="HuellitasException">the exception</exception>
        public async Task InsertAsync(Content content)
        {
            content.CreatedDate = DateTime.Now;
            content.FriendlyName = this.seoService.GenerateFriendlyName(content.Name, this.contentRepository.TableNoTracking);

            try
            {
                await this.contentRepository.InsertAsync(content);
                await this.publisher.EntityInserted(content);
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
                        else if (inner.Message.IndexOf("FK_Content_User") != -1)
                        {
                            target = "User";
                        }
                        else if (inner.Message.IndexOf("FK_ContentUser_User") != -1)
                        {
                            ////TODO:Test again
                            target = "ContentUser";
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
        /// Gets the content user by user identifier and content identifier.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// the content user relationship
        /// </returns>
        public ContentUser GetContentUserById(int contentId, int userId)
        {
            return this.contentUserRepository.Table
                .Include(c => c.Content)
                .FirstOrDefault(c => c.ContentId == contentId && c.UserId == userId);
        }

        /// <summary>
        /// Deletes the content user.
        /// </summary>
        /// <param name="contentUser">The content user.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task DeleteContentUser(ContentUser contentUser)
        {
            await this.contentUserRepository.DeleteAsync(contentUser);

            await this.publisher.EntityDeleted(contentUser);
        }

        /// <summary>
        /// Inserts the user.
        /// </summary>
        /// <param name="contentUser">The content user.</param>
        /// <returns>the task</returns>
        public async Task InsertUser(ContentUser contentUser)
        {
            try
            {
                await this.contentUserRepository.InsertAsync(contentUser);
            }
            catch (DbUpdateException e)
            {
                if (e.ToString().Contains("FK_ContentUser_User"))
                {
                    throw new HuellitasException("ContentUser", HuellitasExceptionCode.InvalidForeignKey);
                }
                else
                {
                    throw;
                }
            }
            
            await this.publisher.EntityInserted(contentUser);
        }

        /// <summary>
        /// Determines whether [is user in content] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="relation">The relation.</param>
        /// <returns>
        ///   <c>true</c> if [is user in content] [the specified user identifier]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsUserInContent(int userId, int contentId, ContentUserRelationType? relation = null)
        {
            var query = this.contentUserRepository.Table.Where(c => c.UserId == userId && c.ContentId == contentId);

            if (relation.HasValue)
            {
                var relationId = Convert.ToInt16(relation);
                query = query.Where(c => c.RelationTypeId == relationId);
            }

            return query.Any();
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
        /// <param name="locationId">the location</param>
        /// <param name="status">the status</param>
        /// <param name="closingDateFrom">filter of closing date from</param>
        /// <param name="closingDateTo">filter of closing date to</param>
        /// <returns>
        /// the value
        /// </returns>
        public IPagedList<Content> Search(
            string keyword = null,
            ContentType? contentType = null,
            IList<FilterAttribute> attributesFilter = null,
            int pageSize = int.MaxValue,
            int page = 0,
            ContentOrderBy orderBy = ContentOrderBy.DisplayOrder,
            int? locationId = null,
            StatusType? status = null,
            DateTime? closingDateFrom = null,
            DateTime? closingDateTo = null)
        {
            var query = this.contentRepository.TableNoTracking
                .Include(c => c.ContentAttributes)
                .Include(c => c.Location)
                .Include(c => c.File)
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

            if (locationId.HasValue)
            {
                query = query.Where(c => c.LocationId == locationId || c.Location.ParentLocationId == locationId);
            }

            if (status.HasValue)
            {
                var statusId = Convert.ToInt16(status.Value);
                query = query.Where(c => c.Status == statusId);
            }

            if (closingDateFrom.HasValue)
            {
                query = query.Where(c => c.ClosingDate == null || c.ClosingDate > closingDateFrom.Value);
            }

            if (closingDateTo.HasValue)
            {
                query = query.Where(c => c.ClosingDate == null || c.ClosingDate > closingDateFrom.Value);
            }

            #region Attributes

            if (attributesFilter != null && attributesFilter.Count > 0)
            {
                var strQueryAttributes = new StringBuilder();
                ////queryAttributes.AppendLine("SELECT ContentId as Id, NULL as Name, NULL as Body, 0 as TypeId, 0 as Status, 0 as UserId, NULL as CreatedDate, 0 as DisplayOrder, 0 as CommentsCount, 0 as Featured, 0 as Deleted, NULL as Email, NULL as FileId, NULL as UpdatedDate, NULL as Views, NULL as LocationId  FROM (");
                strQueryAttributes.AppendLine("SELECT ContentId as Id, ContentId, NULL as Attribute, NULL as Value FROM (");
                strQueryAttributes.AppendLine("SELECT count(ContentId) as countContents, contentId");
                strQueryAttributes.AppendLine(" FROM ContentAttributes");
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

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task UpdateAsync(Content content)
        {
            try
            {
                await this.contentRepository.UpdateAsync(content);
                await this.publisher.EntityUpdated(content);
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
                        else if (inner.Message.IndexOf("FK_Content_User") != -1)
                        {
                            target = "User";
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
    }
}