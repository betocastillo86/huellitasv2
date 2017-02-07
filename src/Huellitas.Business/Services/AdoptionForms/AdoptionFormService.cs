﻿//-----------------------------------------------------------------------
// <copyright file="AdoptionFormService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.AdoptionForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Core;
    using Exceptions;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Infraestructure;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Adoption Form Service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.AdoptionForms.IAdoptionFormService" />
    public class AdoptionFormService : IAdoptionFormService
    {
        /// <summary>
        /// The adoption form repository
        /// </summary>
        private readonly IRepository<AdoptionForm> adoptionFormRepository;

        /// <summary>
        /// The adoption form attribute repository
        /// </summary>
        private readonly IRepository<AdoptionFormAttribute> adoptionFormAttributeRepository;

        /// <summary>
        /// The adoption form answer repository
        /// </summary>
        private readonly IRepository<AdoptionFormAnswer> adoptionFormAnswerRepository;

        /// <summary>
        /// The content attribute repository
        /// </summary>
        private readonly IRepository<ContentAttribute> contentAttributeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdoptionFormService"/> class.
        /// </summary>
        /// <param name="adoptionFormRepository">The adoption form repository.</param>
        /// <param name="contentAttributeRepository">The content attribute repository.</param>
        public AdoptionFormService(
            IRepository<AdoptionForm> adoptionFormRepository,
            IRepository<ContentAttribute> contentAttributeRepository,
            IRepository<AdoptionFormAttribute> adoptionFormAttributeRepository,
            IRepository<AdoptionFormAnswer> adoptionFormAnswerRepository)
        {
            this.adoptionFormRepository = adoptionFormRepository;
            this.contentAttributeRepository = contentAttributeRepository;
            this.adoptionFormAttributeRepository = adoptionFormAttributeRepository;
            this.adoptionFormAnswerRepository = adoptionFormAnswerRepository;
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
            AdoptionFormAnswerStatus? lastStatus = null,
            AdoptionFormOrderBy orderBy = AdoptionFormOrderBy.CreationDate,
            int page = 0,
            int pageSize = int.MaxValue)
        {
            var query = this.adoptionFormRepository.Table
                .Include(c => c.Content)
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

            if (shelterId.HasValue)
            {
                var attributeShelter = ContentAttributeType.Shelter.ToString();
                var queryAttributes = this.contentAttributeRepository.Table
                    .Where(c => c.Attribute.Equals(attributeShelter) && c.Value.Equals(shelterId.Value.ToString()))
                    .Select(c => c.ContentId);

                query = query.Where(c => queryAttributes.Contains(c.ContentId));
            }

            if (formUserId.HasValue)
            {
                query = query.Where(c => c.UserId == formUserId.Value);
            }

            if (contentUserId.HasValue)
            {
                query = query.Where(c => c.Content.UserId == contentUserId.Value);
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
    }
}