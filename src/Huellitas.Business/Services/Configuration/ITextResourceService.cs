//-----------------------------------------------------------------------
// <copyright file="ITextResourceService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Configuration
{
    using System.Threading.Tasks;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Entities.Enums;
    using Huellitas.Data.Infraestructure;

    /// <summary>
    /// Interface Text Resource Service
    /// </summary>
    public interface ITextResourceService
    {
        /// <summary>
        /// Gets all by language.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <param name="keyword">the key</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>the resources</returns>
        IPagedList<TextResource> GetAll(LanguageEnum? language = null, string keyword = null, int page = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the resource</returns>
        TextResource GetById(int id);

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the task</returns>
        Task Insert(TextResource entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the task</returns>
        Task Update(TextResource entity);
    }
}