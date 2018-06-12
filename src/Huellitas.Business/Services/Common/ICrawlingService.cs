namespace Huellitas.Business.Services
{
    using System.Threading.Tasks;
    using Huellitas.Data;

    /// <summary>
    /// Interface of crawling service
    /// </summary>
    public interface ICrawlingService
    {
        /// <summary>
        /// Gets the by URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>the crawling</returns>
        SeoCrawling GetByUrl(string url);

        /// <summary>
        /// Gets the by URL asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>the entity</returns>
        Task<SeoCrawling> GetByUrlAsync(string url);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="crawling">The crawling.</param>
        /// <returns>the task</returns>
        Task InsertAsync(SeoCrawling crawling);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="crawling">The crawling.</param>
        /// <returns>the task</returns>
        Task UpdateAsync(SeoCrawling crawling);
    }
}