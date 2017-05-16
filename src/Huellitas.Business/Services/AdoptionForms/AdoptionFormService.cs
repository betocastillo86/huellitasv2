//-----------------------------------------------------------------------
// <copyright file="AdoptionFormService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Core;
    using Data.Entities.Enums;
    using EventPublisher;
    using Exceptions;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Infraestructure;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Adoption Form Service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.IAdoptionFormService" />
    public class AdoptionFormService : IAdoptionFormService
    {
        /// <summary>
        /// The adoption form answer repository
        /// </summary>
        private readonly IRepository<AdoptionFormAnswer> adoptionFormAnswerRepository;

        /// <summary>
        /// The adoption form attribute repository
        /// </summary>
        private readonly IRepository<AdoptionFormAttribute> adoptionFormAttributeRepository;

        /// <summary>
        /// The adoption form repository
        /// </summary>
        private readonly IRepository<AdoptionForm> adoptionFormRepository;

        /// <summary>
        /// The adoption form user repository
        /// </summary>
        private readonly IRepository<AdoptionFormUser> adoptionFormUserRepository;

        /// <summary>
        /// The content attribute repository
        /// </summary>
        private readonly IRepository<ContentAttribute> contentAttributeRepository;

        /// <summary>
        /// The content user repository
        /// </summary>
        private readonly IRepository<ContentUser> contentUserRepository;

        /// <summary>
        /// The publisher
        /// </summary>
        private readonly IPublisher publisher;

        /// <summary>
        /// The content service
        /// </summary>
        private readonly IContentService contentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdoptionFormService"/> class.
        /// </summary>
        /// <param name="adoptionFormRepository">The adoption form repository.</param>
        /// <param name="contentAttributeRepository">The content attribute repository.</param>
        /// <param name="adoptionFormAttributeRepository">The adoption form attribute repository.</param>
        /// <param name="adoptionFormAnswerRepository">The adoption form answer repository.</param>
        /// <param name="adoptionFormUserRepository">The adoption form user repository.</param>
        /// <param name="publisher">The publisher.</param>
        /// <param name="contentUserRepository">The content user repository.</param>
        public AdoptionFormService(
            IRepository<AdoptionForm> adoptionFormRepository,
            IRepository<ContentAttribute> contentAttributeRepository,
            IRepository<AdoptionFormAttribute> adoptionFormAttributeRepository,
            IRepository<AdoptionFormAnswer> adoptionFormAnswerRepository,
            IRepository<AdoptionFormUser> adoptionFormUserRepository,
            IPublisher publisher,
            IRepository<ContentUser> contentUserRepository,
            IContentService contentService)
        {
            this.adoptionFormRepository = adoptionFormRepository;
            this.contentAttributeRepository = contentAttributeRepository;
            this.adoptionFormAttributeRepository = adoptionFormAttributeRepository;
            this.adoptionFormAnswerRepository = adoptionFormAnswerRepository;
            this.adoptionFormUserRepository = adoptionFormUserRepository;
            this.publisher = publisher;
            this.contentUserRepository = contentUserRepository;
            this.contentService = contentService;
        }

        /// <summary>
        /// Gets all the adoption forms.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="shelterId">The shelter identifier.</param>
        /// <param name="formUserId">The form user identifier.</param>
        /// <param name="contentUserId">The content user identifier.</param>
        /// <param name="sharedToUserId">Filter that search what forms have been share with it</param>
        /// <param name="parentUserId">the parent user identifier</param>
        /// <param name="allRelatedToUserId">all the forms related to an user</param>
        /// <param name="lastStatus">The last status.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// the list of adoption forms
        /// </returns>
        public IPagedList<AdoptionForm> GetAll(
            string user = null,
            int? contentId = null,
            int? locationId = null,
            int? shelterId = null,
            int? formUserId = null,
            int? contentUserId = null,
            int? sharedToUserId = null,
            int? parentUserId = null,
            int? allRelatedToUserId = null,
            AdoptionFormAnswerStatus? lastStatus = null,
            AdoptionFormOrderBy orderBy = AdoptionFormOrderBy.CreationDate,
            int page = 0,
            int pageSize = int.MaxValue)
        {
            var query = this.adoptionFormRepository.Table
                .Include(c => c.Content)
                .Include(c => c.Content.File)
                .AsQueryable();

            if (!string.IsNullOrEmpty(user))
            {
                query = query.Where(c => c.Name.Contains(user));
            }

            if (contentId.HasValue)
            {
                query = query.Where(c => c.ContentId == contentId.Value);
            }

            if (locationId.HasValue)
            {
                query = query.Where(c => c.LocationId == locationId.Value);
            }

            ////Funcion que se reutoliza en los dos bloques de codigo y trae todos los contenidos en los que un usuario es padrino
            Func<int, IQueryable<int>> getPetsByParent = (int userId) =>
            {
                var relationId = Convert.ToInt16(ContentUserRelationType.Parent);
                return this.contentUserRepository.Table
                    .Where(c => c.RelationTypeId == relationId && c.UserId == userId)
                    .Select(c => c.ContentId);
            };

            //// Si debe traer los asociados a un usuario omite los demás filtros
            if (allRelatedToUserId.HasValue)
            {
                ////var petsWhichIsParent = getPetsByParent(allRelatedToUserId.Value);

                ////////Consulta los refugios del usuario para posteriormente traer los formularios de esos refugios
                ////var relationId = Convert.ToInt16(ContentUserRelationType.Shelter);
                ////var sheltersOfUser = this.contentUserRepository.Table.Where(c => c.UserId == allRelatedToUserId.Value && c.RelationTypeId == relationId)
                ////    .Select(c => c.ContentId.ToString());

                ////var attributeShelter = ContentAttributeType.Shelter.ToString();
                ////var contentsOfShelter = this.contentAttributeRepository.Table
                ////    .Where(c => c.Attribute.Equals(attributeShelter) && sheltersOfUser.Contains(c.Value))
                ////    .Select(c => c.ContentId); 

                var myPets = this.contentService.Search(belongsToUserId: allRelatedToUserId.Value, contentType: ContentType.Pet).Select(c => c.Id).ToArray();

                query = query.Where(
                    c => c.Users.Any(x => x.UserId == allRelatedToUserId.Value) ||
                    myPets.Contains(c.ContentId)


                    //petsWhichIsParent.Contains(c.ContentId) ||
                    //contentsOfShelter.Contains(c.ContentId) 
                    //c.UserId == allRelatedToUserId.Value ||
                    //c.Content.UserId == allRelatedToUserId.Value
                    );
            }
            else
            {
                if (sharedToUserId.HasValue)
                {
                    query = query.Where(c => c.Users.Any(x => x.UserId == sharedToUserId.Value));
                }

                if (parentUserId.HasValue)
                {
                    var petsWhichIsParent = getPetsByParent(parentUserId.Value);
                    query = query.Where(c => petsWhichIsParent.Contains(c.ContentId));
                }

                if (formUserId.HasValue)
                {
                    query = query.Where(c => c.UserId == formUserId.Value);
                }

                if (contentUserId.HasValue)
                {
                    query = query.Where(c => c.Content.UserId == contentUserId.Value);
                }

                if (shelterId.HasValue)
                {
                    var attributeShelter = ContentAttributeType.Shelter.ToString();
                    var queryAttributes = this.contentAttributeRepository.Table
                        .Where(c => c.Attribute.Equals(attributeShelter) && c.Value.Equals(shelterId.Value.ToString()))
                        .Select(c => c.ContentId);

                    query = query.Where(c => queryAttributes.Contains(c.ContentId));
                }
            }

            if (lastStatus.HasValue)
            {
                short lastStatusNumber = Convert.ToInt16(lastStatus.Value);
                query = query.Where(c => c.LastStatus == lastStatusNumber);
            }

            switch (orderBy)
            {
                case AdoptionFormOrderBy.Pet:
                    query = query.OrderBy(c => c.Content.Name);
                    break;

                case AdoptionFormOrderBy.Name:
                    query = query.OrderBy(c => c.Name);
                    break;

                case AdoptionFormOrderBy.CreationDate:
                default:
                    query = query.OrderByDescending(c => c.CreationDate);
                    break;
            }

            return new PagedList<AdoptionForm>(query, page, pageSize);
        }

        /// <summary>
        /// Gets the answers.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// the list
        /// </returns>
        public IList<AdoptionFormAnswer> GetAnswers(int id)
        {
            return this.adoptionFormAnswerRepository.Table
                .Include(c => c.User)
                .Where(c => c.AdoptionFormId == id)
                .ToList();
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// the list of attributes
        /// </returns>
        public IList<AdoptionFormAttribute> GetAttributes(int id)
        {
            return this.adoptionFormAttributeRepository.Table
                .Include(c => c.Attribute)
                .Where(c => c.AdoptionFormId == id)
                .ToList();
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// the entity
        /// </returns>
        public AdoptionForm GetById(int id)
        {
            return this.adoptionFormRepository.Table
                .Include(c => c.User)
                .Include(c => c.Content)
                .Include(c => c.Content.User)
                .Include(c => c.Content.File)
                .Include(c => c.Job)
                .Include(c => c.Location)
                .FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// Inserts the specified form.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task Insert(AdoptionForm form)
        {
            try
            {
                form.CreationDate = DateTime.Now;
                await this.adoptionFormRepository.InsertAsync(form);
                await this.publisher.EntityInserted(form);
            }
            catch (DbUpdateException e)
            {
                if (e.ToString().Contains("FK_AdoptionForm_Content1"))
                {
                    throw new HuellitasException("Content", HuellitasExceptionCode.InvalidForeignKey);
                }
                else if (e.ToString().Contains("FK_AdoptionForm_CustomTableRow"))
                {
                    throw new HuellitasException("Job", HuellitasExceptionCode.InvalidForeignKey);
                }
                else if (e.ToString().Contains("FK_AdoptionForm_Location"))
                {
                    throw new HuellitasException("Location", HuellitasExceptionCode.InvalidForeignKey);
                }
                else if (e.ToString().Contains("FK_AdoptionForms_Users_UserId"))
                {
                    throw new HuellitasException("User", HuellitasExceptionCode.InvalidForeignKey);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Inserts the answer.
        /// </summary>
        /// <param name="answer">The answer.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task InsertAnswer(AdoptionFormAnswer answer)
        {
            try
            {
                answer.CreationDate = DateTime.Now;

                await this.adoptionFormAnswerRepository.InsertAsync(answer);

                // updates the last status form
                var form = this.GetById(answer.AdoptionFormId);
                form.LastStatusEnum = answer.StatusEnum;
                await this.Update(form);

                await this.publisher.EntityInserted(answer);
            }
            catch (DbUpdateException e)
            {
                if (e.ToString().Contains("FK_AdoptionFormAnswer_AdoptionForm"))
                {
                    throw new HuellitasException("AdoptionFormId", HuellitasExceptionCode.InvalidForeignKey);
                }
                else if (e.ToString().Contains("FK_AdoptionFormAnswer_User"))
                {
                    throw new HuellitasException("UserId", HuellitasExceptionCode.InvalidForeignKey);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Inserts the user.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the user</returns>
        public async Task InsertUser(AdoptionFormUser entity)
        {
            try
            {
                await this.adoptionFormUserRepository.InsertAsync(entity);

                await this.publisher.EntityInserted(entity);
            }
            catch (DbUpdateException e)
            {
                if (e.ToString().Contains("FK_AdoptionFormUser_User"))
                {
                    throw new HuellitasException("UserId", HuellitasExceptionCode.InvalidForeignKey);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Determines whether [is user in adoption form] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="adoptionFormId">The adoption form identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is user in adoption form] [the specified user identifier]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsUserInAdoptionForm(int userId, int adoptionFormId)
        {
            return this.adoptionFormUserRepository.Table.Any(c => c.UserId == userId && c.AdoptionFormId == adoptionFormId);
        }

        /// <summary>
        /// Updates the specified form.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>the task</returns>
        public async Task Update(AdoptionForm form)
        {
            try
            {
                await this.adoptionFormRepository.UpdateAsync(form);

                await this.publisher.EntityUpdated(form);
            }
            catch (DbUpdateException e)
            {
                if (e.ToString().Contains("FK_AdoptionFormAnswer_AdoptionForm"))
                {
                    throw new HuellitasException("AdoptionFormId", HuellitasExceptionCode.InvalidForeignKey);
                }
                else if (e.ToString().Contains("FK_AdoptionFormAnswer_User"))
                {
                    throw new HuellitasException("UserId", HuellitasExceptionCode.InvalidForeignKey);
                }
                else
                {
                    throw;
                }
            }
        }

        public IDictionary<int, int> CountAdoptionFormsByContents(int[] contentIds, AdoptionFormAnswerStatus? status = null)
        {
            var query = this.adoptionFormRepository.Table
                .Where(c => contentIds.Contains(c.ContentId));

            if (status.HasValue)
            {
                var statusId = Convert.ToInt16(status);
                query = query.Where(c => c.LastStatus == statusId);
            }

            return query.GroupBy(c => c.ContentId)
                    .Select(c => new { Key = c.Key, Value = c.Count() })
                    .ToDictionary(c => c.Key, c => c.Value);
        }
    }
}