//-----------------------------------------------------------------------
// <copyright file="ContentService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Beto.Core.EventPublisher;
    using Data.Entities;
    using Exceptions;
    using Huellitas.Data.Core;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Content Service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.IContentService" />
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
        /// Initializes a new instance of the <see cref="ContentService" /> class.
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
        /// Get by friendly name
        /// </summary>
        /// <param name="friendlyName">friendly name</param>
        /// <param name="includeLocation">includes location</param>
        /// <returns>
        /// the content
        /// </returns>
        public Content GetByFriendlyName(string friendlyName, bool includeLocation = false)
        {
            var query = this.contentRepository.Table
                .Include(c => c.ContentAttributes)
                .Include(c => c.File)
                .Include(c => c.User)
                .Where(c => c.FriendlyName.Equals(friendlyName) && !c.Deleted);

            if (includeLocation)
            {
                query = query.Include(c => c.Location);
            }

            return query.FirstOrDefault();
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
        /// Gets the contents by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="relation">The relation.</param>
        /// <param name="includeContent">if set to <c>true</c> [include content].</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// the contents
        /// </returns>
        public IPagedList<ContentUser> GetContentsByUserId(int userId, ContentUserRelationType? relation = null, bool includeContent = false, int page = 0, int pageSize = int.MaxValue)
        {
            var query = this.contentUserRepository.Table;

            if (includeContent)
            {
                query = query
                    .Include(c => c.Content)
                    .Include(c => c.Content.Location);
            }

            query = query.Where(c => c.UserId == userId);

            if (relation.HasValue)
            {
                var relationId = Convert.ToDecimal(relation.Value);
                query = query.Where(c => c.RelationTypeId == relationId);
            }

            query = query.OrderBy(c => c.Id);

            return new PagedList<ContentUser>(query, page, pageSize);
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
        /// Gets the files of a content
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <returns>
        /// the files
        /// </returns>
        public IList<ContentFile> GetFiles(int contentId)
        {
            return this.contentFileRepository.Table
                .Include(c => c.File)
                .Where(c => c.ContentId == contentId && !c.File.Deleted)
                .OrderByDescending(c => c.DisplayOrder)
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
        /// <returns>
        /// the task
        /// </returns>
        /// <exception cref="HuellitasException">the exception</exception>
        public async Task InsertAsync(Content content)
        {
            content.CreatedDate = DateTime.UtcNow;
            if (string.IsNullOrEmpty(content.FriendlyName))
            {
                content.FriendlyName = this.seoService.GenerateFriendlyName(content.Name, this.contentRepository.Table);
            }

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
        /// Inserts the user.
        /// </summary>
        /// <param name="contentUser">The content user.</param>
        /// <returns>
        /// the task
        /// </returns>
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
        /// <param name="locationId">The location identifier.</param>
        /// <param name="status">The status.</param>
        /// <param name="closingDateFrom">The closing date from.</param>
        /// <param name="closingDateTo">The closing date to.</param>
        /// <param name="startingDateFrom">filters starting date from this</param>
        /// <param name="startingDateTo">filters starting date until this</param>
        /// <param name="belongsToUserId">filter by user owner inside. User identifier and parents</param>
        /// <param name="excludeContentId">excludes the search of a content</param>
        /// <param name="onlyFeatured">only featured</param>
        /// <param name="onlyRescuers">only with rescuers</param>
        /// <returns>
        /// the list
        /// </returns>
        /// <exception cref="HuellitasException">No se puede filtrar por usuario ya que el filtro tipo de contenido es obligatorio</exception>
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
            DateTime? closingDateTo = null,
            DateTime? startingDateFrom = null,
            DateTime? startingDateTo = null,
            int? belongsToUserId = null,
            int? excludeContentId = null,
            bool? onlyFeatured = null,
            bool? onlyRescuers = null)
        {
            var query = this.contentRepository.Table
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

            if (belongsToUserId.HasValue)
            {
                if (contentType.HasValue)
                {
                    switch (contentType.Value)
                    {
                        /***
                         * Para determinar cuales son los pets de un usuario es necesario:
                         *  - Todos los tipo pet que tengan en userid = a belongstouserid
                         *  - Todos los pets que pertenezcan a los shelters a los que pertenece el usuario
                         *  - Todos los pets de los que el usuario sea padrino
                         * ***/
                        case ContentType.Pet:

                            ////Consulta los contenidos de donde es padrino
                            var parentRelation = Convert.ToInt16(ContentUserRelationType.Parent);
                            var parentsQuery = this.contentUserRepository.Table
                                .Where(c => c.UserId == belongsToUserId && c.RelationTypeId == parentRelation)
                                .Select(c => c.ContentId);

                            if (attributesFilter == null || !attributesFilter.Any(c => c.Attribute == ContentAttributeType.Shelter))
                            {
                                var shelterRelation = Convert.ToInt16(ContentUserRelationType.Shelter);
                                var shelterContentType = Convert.ToInt16(ContentType.Shelter);

                                //// Consulta los shelters a los que puede pertenecer el usuario
                                ////var myShelters = (from c in this.contentRepository.Table
                                ////          from cu in this.contentUserRepository.Table.Where(x => x.ContentId == c.Id).DefaultIfEmpty()
                                ////          where (c.UserId == belongsToUserId && c.TypeId == shelterContentType) || (cu.UserId == belongsToUserId && cu.RelationTypeId == shelterRelation)
                                ////          select c.Id.ToString()).ToList();

                                var myShelters = this.contentUserRepository.Table
                                    .Where(c => c.UserId == belongsToUserId && c.RelationTypeId == shelterRelation)
                                    .Select(c => c.ContentId.ToString())
                                    .ToList();

                                //// Consulta los pets de ese shelter
                                var attribute = ContentAttributeType.Shelter.ToString();
                                var sheltersQuery = this.contentAttributeRepository.Table
                                    .Where(c => myShelters.Contains(c.Value) && c.Attribute.Equals(attribute))
                                    .Select(c => c.ContentId)
                                    .ToList();

                                //// Saca el listado de todos los pets que le pertenecen
                                var contentsOfUserInShelter = sheltersQuery.Union(parentsQuery).ToList();

                                query = query.Where(c => contentsOfUserInShelter.Contains(c.Id) || c.UserId == belongsToUserId.Value);
                            }
                            else
                            {
                                query = query.Where(c => parentsQuery.Contains(c.Id) || c.UserId == belongsToUserId.Value);
                            }

                            break;

                        case ContentType.Shelter:
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    throw new HuellitasException(HuellitasExceptionCode.BadArgument, "No se puede filtrar por usuario ya que el filtro tipo de contenido es obligatorio");
                }
            }

            if (locationId.HasValue)
            {
                query = query.Where(c => c.LocationId == locationId || c.Location.ParentLocationId == locationId);
            }

            if (onlyRescuers.HasValue)
            {
                var rescuerRoleId = Convert.ToInt32(RoleEnum.Rescuer);
                if (onlyRescuers.Value)
                {
                    query = query.Where(c => c.User.RoleId == rescuerRoleId);
                }
                else
                {
                    query = query.Where(c => c.User.RoleId != rescuerRoleId);
                }
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

            if (startingDateFrom.HasValue)
            {
                query = query.Where(c => c.StartingDate == null || c.StartingDate > startingDateFrom.Value);
            }

            if (onlyFeatured.HasValue)
            {
                query = query.Where(c => c.Featured == onlyFeatured.Value);
            }

            if (startingDateTo.HasValue)
            {
                query = query.Where(c => c.StartingDate == null || c.StartingDate > startingDateTo.Value);
            }

            if (excludeContentId.HasValue)
            {
                query = query.Where(c => c.Id != excludeContentId.Value);
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
                            strQueryAttributes.Append($" (Attribute = '{attribute.Attribute}' and  convert(int, (case when Attribute = '{attribute.Attribute}' and ISNUMERIC(Value) = 1 and charindex('.', value) = 0 then Value else '-1' end)) between '{attribute.Value}' and '{attribute.ValueTo}')");
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

                case ContentOrderBy.Featured:
                    query = query.OrderByDescending(c => c.Featured)
                                 .ThenByDescending(c => c.CreatedDate);
                    break;

                case ContentOrderBy.Random:
                    query = query.OrderBy(x => Guid.NewGuid());
                    break;

                case ContentOrderBy.DisplayOrder:
                default:
                    query = query.OrderBy(c => c.DisplayOrder);
                    break;
            }

            var contents = new PagedList<Content>(query, page, pageSize);

            var contentIds = contents.Select(c => c.Id).ToArray();
            var attributes = this.contentAttributeRepository.TableNoTracking
                .Where(c => contentIds.Contains(c.ContentId))
                .ToList();

            foreach (var content in contents)
            {
                content.ContentAttributes = attributes.Where(c => c.ContentId == content.Id).ToArray();
            }

            return contents;
        }

        /// <summary>
        /// Sorts the files.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="fileIdFrom">The file identifier from.</param>
        /// <param name="fileIdTo">The file identifier to.</param>
        /// <returns>the return</returns>
        public async Task SortFiles(int contentId, int fileIdFrom, int fileIdTo)
        {
            var files = this.contentFileRepository.Table.Where(c => c.ContentId == contentId && !c.File.Deleted)
                .ToList();

            var fileFrom = files.FirstOrDefault(c => c.FileId == fileIdFrom);
            var fileTo = files.FirstOrDefault(c => c.FileId == fileIdTo);
            var newDisplayOrder = fileTo.DisplayOrder;

            ////if the old position is lower than new's substracts 1
            if (fileFrom.DisplayOrder < fileTo.DisplayOrder)
            {
                var filesToUpdate = files
                    .Where(c => c.DisplayOrder <= fileTo.DisplayOrder && c.DisplayOrder > fileFrom.DisplayOrder)
                    .ToList();

                foreach (var file in filesToUpdate)
                {
                    file.DisplayOrder--;
                }

                await this.contentFileRepository.UpdateAsync(filesToUpdate);
            }
            else
            {
                var filesToUpdate = files.Where(c => c.DisplayOrder >= fileTo.DisplayOrder && c.DisplayOrder < fileFrom.DisplayOrder)
                    .ToList();

                foreach (var file in filesToUpdate)
                {
                    file.DisplayOrder++;
                }

                await this.contentFileRepository.UpdateAsync(filesToUpdate);
            }

            ////Actualiza la nueva posición
            fileFrom.DisplayOrder = newDisplayOrder;
            await this.contentFileRepository.UpdateAsync(fileFrom);

            ////Actualiza la imagen principal
            if (newDisplayOrder == files.Count)
            {
                var content = this.contentRepository.Table.FirstOrDefault(c => c.Id == contentId);
                content.FileId = fileFrom.FileId;
                await this.contentRepository.UpdateAsync(content);
            }
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