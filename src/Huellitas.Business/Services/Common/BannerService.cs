//-----------------------------------------------------------------------
// <copyright file="BannerService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Beto.Core.EventPublisher;
    using Huellitas.Business.Exceptions;
    using Huellitas.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// the banner service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.IBannerService" />
    public class BannerService : IBannerService
    {
        /// <summary>
        /// The banner repository
        /// </summary>
        private readonly IRepository<Banner> bannerRepository;

        /// <summary>
        /// The publisher
        /// </summary>
        private readonly IPublisher publisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="BannerService"/> class.
        /// </summary>
        /// <param name="bannerRepository">The banner repository.</param>
        /// <param name="publisher">The publisher.</param>
        public BannerService(
            IRepository<Banner> bannerRepository,
            IPublisher publisher)
        {
            this.bannerRepository = bannerRepository;
            this.publisher = publisher;
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="banner">The identifier.</param>
        /// <exception cref="Dasigno.NosUne.Business.Exceptions.NosUneException">the exception</exception>
        /// <returns>the task</returns>
        public async Task Delete(Banner banner)
        {
            banner.Deleted = true;
            banner.ModifiedDate = DateTime.UtcNow;
            await this.bannerRepository.UpdateAsync(banner);

            ////publica evento de actualizacion
            await this.publisher.EntityDeleted(banner);
        }

        /// <summary>
        /// Gets all banners
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="active">The active.</param>
        /// <param name="keyword">searches by name and body with the keyword</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="orderby">the order</param>
        /// <returns>
        /// the list of banners
        /// </returns>
        public IPagedList<Banner> GetAll(
            BannerSection? section = null,
            bool? active = null,
            string keyword = null,
            int page = 0,
            int pageSize = int.MaxValue,
            OrderByBanner orderby = OrderByBanner.DisplayOrder)
        {
            var query = this.bannerRepository.Table
                .Include(b => b.File)
                .Where(b => !b.Deleted);

            if (section.HasValue)
            {
                var sectionId = Convert.ToInt16(section);
                query = query.Where(b => b.SectionId == sectionId);
            }

            if (active.HasValue)
            {
                query = query.Where(b => b.Active == active.Value);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(b => b.Name.Contains(keyword) || b.Body.Contains(keyword));
            }

            switch (orderby)
            {
                case OrderByBanner.Recent:
                    query = query.OrderByDescending(c => c.CreationDate);
                    break;

                case OrderByBanner.DisplayOrder:
                    query = query.OrderBy(c => c.DisplayOrder);
                    break;

                case OrderByBanner.Name:
                    query = query.OrderBy(c => c.Name);
                    break;
            }

            return new PagedList<Banner>(query, page, pageSize);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeFile">if set to <c>true</c> [include file].</param>
        /// <returns>
        /// the banner
        /// </returns>
        public Banner GetById(int id, bool includeFile = true)
        {
            var query = this.bannerRepository.Table;

            if (includeFile)
            {
                query = query.Include(c => c.File);
            }

            return query.FirstOrDefault(c => c.Id == id && !c.Deleted);
        }

        /// <summary>
        /// Inserts the specified banner.
        /// </summary>
        /// <param name="banner">The banner.</param>
        /// <exception cref="System.ArgumentNullException">
        /// banner
        /// or
        /// file
        /// </exception>
        /// <returns>the task</returns>
        public async Task Insert(Banner banner)
        {
            if (banner == null)
            {
                throw new ArgumentNullException("banner");
            }

            banner.CreationDate = DateTime.UtcNow;

            try
            {
                await this.bannerRepository.InsertAsync(banner);
            }
            catch (DbUpdateException e)
            {
                if (e.ToString().Contains("FK_Banner_File"))
                {
                    throw new HuellitasException("FileId", HuellitasExceptionCode.InvalidForeignKey);
                }
                else
                {
                    throw;
                }
            }

            ////publica evento de actualizacion
            await this.publisher.EntityInserted(banner);
        }

        /// <summary>
        /// Updates the specified banner.
        /// </summary>
        /// <param name="banner">The banner.</param>
        /// <exception cref="Dasigno.NosUne.Business.Exceptions.NosUneException">the exception</exception>
        /// <returns>the task</returns>
        public async Task Update(Banner banner)
        {
            banner.ModifiedDate = DateTime.UtcNow;

            await this.bannerRepository.UpdateAsync(banner);

            ////publica evento de actualizacion
            await this.publisher.EntityUpdated(banner);
        }
    }
}