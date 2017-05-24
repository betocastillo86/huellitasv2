//-----------------------------------------------------------------------
// <copyright file="TextResourceService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using Huellitas.Business.EventPublisher;
    using Huellitas.Business.Exceptions;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Infraestructure;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Text resources service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.ITextResourceService" />
    public class TextResourceService : ITextResourceService
    {
        /// <summary>
        /// The text resource repository
        /// </summary>
        private readonly IRepository<TextResource> textResourceRepository;

        /// <summary>
        /// The publisher
        /// </summary>
        private readonly IPublisher publisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextResourceService"/> class.
        /// </summary>
        /// <param name="textResourceRepository">The text resource repository.</param>
        /// <param name="publisher">The publisher.</param>
        public TextResourceService(
            IRepository<TextResource> textResourceRepository,
            IPublisher publisher)
        {
            this.textResourceRepository = textResourceRepository;
            this.publisher = publisher;
        }

        /// <summary>
        /// Gets all by language.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <param name="keyword">the keyword</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// the resources
        /// </returns>
        public IPagedList<TextResource> GetAll(
            LanguageEnum? language = null,
            string keyword = null,
            int page = 0,
            int pageSize = int.MaxValue)
        {
            var query = this.textResourceRepository.Table;

            if (language.HasValue)
            {
                var languageId = Convert.ToInt16(language);
                query = query.Where(c => c.LanguageId == languageId);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => c.Name.Contains(keyword));
            }

            query = query.OrderBy(c => c.Name);

            return new PagedList<TextResource>(query, page, pageSize);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// the resource
        /// </returns>
        public TextResource GetById(int id)
        {
            return this.textResourceRepository.Table.FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// the task
        /// </returns>
        /// <exception cref="HuellitasException">same key</exception>
        public async Task Insert(TextResource entity)
        {
            try
            {
                await this.textResourceRepository.InsertAsync(entity);
            }
            catch (DbUpdateException e)
            {
                if (e.ToString().Contains("IX_TextResources"))
                {
                    throw new HuellitasException("Name", HuellitasExceptionCode.InvalidForeignKey);
                }
                else
                {
                    throw;
                }
            }

            ////publica el evento
            await this.publisher.EntityInserted(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// the task
        /// </returns>
        /// <exception cref="HuellitasException">same key</exception>
        public async Task Update(TextResource entity)
        {
            try
            {
                await this.textResourceRepository.UpdateAsync(entity);
            }
            catch (DbUpdateException e)
            {
                if (e.ToString().Contains("IX_TextResources"))
                {
                    throw new HuellitasException("Name", HuellitasExceptionCode.InvalidIndex);
                }
                else
                {
                    throw;
                }
            }

            ////publica el evento
            await this.publisher.EntityUpdated(entity);
        }
    }
}