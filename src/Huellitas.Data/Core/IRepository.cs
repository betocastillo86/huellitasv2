//-----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Interface of Repository
    /// </summary>
    /// <typeparam name="T">The class base entity</typeparam>
    public partial interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Gets the table.
        /// </summary>
        /// <value>
        /// The table.
        /// </value>
        IQueryable<T> Table { get; }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the value</returns>
        int Delete(T entity);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the entities modified</returns>
        Task<int> DeleteAsync(T entity);

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">The Entities</param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">the Identifier</param>
        /// <returns>the entity</returns>
        T GetById(int id);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">The entity</param>
        void Insert(T entity);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the task</returns>
        Task InsertAsync(T entity);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">The Entities</param>
        void Insert(IEnumerable<T> entities);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>the task</returns>
        Task InsertAsync(IEnumerable<T> entities);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the value</returns>
        int Update(T entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the value</returns>
        Task<int> UpdateAsync(T entity);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>the value</returns>
        Task UpdateAsync(ICollection<T> entities);
    }
}