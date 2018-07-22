//-----------------------------------------------------------------------
// <copyright file="CrawlingService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Huellitas.Data;
    using Huellitas.Data.Core;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Crawling Service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.ICrawlingService" />
    public class CrawlingService : ICrawlingService
    {
        /// <summary>
        /// The crawling repository
        /// </summary>
        private readonly IRepository<SeoCrawling> crawlingRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrawlingService" /> class.
        /// </summary>
        /// <param name="crawlingRepository">The crawling repository.</param>
        public CrawlingService(IRepository<SeoCrawling> crawlingRepository)
        {
            this.crawlingRepository = crawlingRepository;
        }

        /// <summary>
        /// Gets the by URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>
        /// the crawling
        /// </returns>
        public SeoCrawling GetByUrl(string url)
        {
            return this.crawlingRepository.Table.FirstOrDefault(c => c.Url.Equals(url));
        }

        /// <summary>
        /// Gets the by URL asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>
        /// the entity
        /// </returns>
        public async Task<SeoCrawling> GetByUrlAsync(string url)
        {
            return await this.crawlingRepository.Table.FirstOrDefaultAsync(c => c.Url.Equals(url));
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="crawling">The crawling.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task InsertAsync(SeoCrawling crawling)
        {
            crawling.CreationDate = DateTime.Now;
            await this.crawlingRepository.InsertAsync(crawling);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="crawling">The crawling.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task UpdateAsync(SeoCrawling crawling)
        {
            crawling.ModifiedDate = DateTime.Now;
            await this.crawlingRepository.UpdateAsync(crawling);
        }
    }
}