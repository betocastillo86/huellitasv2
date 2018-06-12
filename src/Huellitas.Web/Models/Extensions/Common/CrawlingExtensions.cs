namespace Huellitas.Web.Models.Extensions
{
    using Huellitas.Data;

    /// <summary>
    /// Crawling Extensions
    /// </summary>
    public static class CrawlingExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the model</returns>
        public static CrawlingModel ToModel(this SeoCrawling entity)
        {
            return new CrawlingModel { Html = entity.Html, Url = entity.Html };
        }
    }
}