//-----------------------------------------------------------------------
// <copyright file="EfRepository.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Huellitas.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The Repository
    /// </summary>
    /// <typeparam name="T">The Entity</typeparam>
    /// <seealso cref="Huellitas.Data.Core.IRepository{T}" />
    public partial class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly HuellitasContext context;

        /// <summary>
        /// The entities
        /// </summary>
        private DbSet<T> entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="EfRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EfRepository(HuellitasContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return this.Entities.AsNoTracking();
            }
        }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        protected virtual DbSet<T> Entities
        {
            get
            {
                if (this.entities == null)
                {
                    this.entities = this.context.Set<T>();
                }

                return this.entities;
            }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>the value</returns>
        /// <exception cref="System.ArgumentNullException">the entity</exception>
        public virtual int Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.Entities.Remove(entity);

            return this.context.SaveChanges();
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the number of rows</returns>
        /// <exception cref="System.ArgumentNullException">the entity</exception>
        public async Task<int> DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.Entities.Remove(entity);

            return await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">The Entities</param>
        /// <exception cref="System.ArgumentNullException">the entities</exception>
        public virtual void Delete(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            foreach (var entity in entities)
            {
                this.Entities.Remove(entity);
            }

            this.context.SaveChanges();
        }

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">the Identifier</param>
        /// <returns>
        /// The Entity
        /// </returns>
        public virtual T GetById(int id)
        {
            ////see some suggested performance optimization (not tested)
            ////http://stackoverflow.com/questions/11686225/dbset-find-method-ridiculously-slow-compared-to-singleordefault-on-id/11688189#comment34876113_11688189
            return this.Entities.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <exception cref="System.ArgumentNullException">the entity</exception>
        public virtual void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.Entities.Add(entity);

            this.context.SaveChanges();
        }

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">The Entities</param>
        /// <exception cref="System.ArgumentNullException">the entities</exception>
        public virtual void Insert(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            foreach (var entity in entities)
            {
                this.Entities.Add(entity);
            }

            this.context.SaveChanges();
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>
        /// the task
        /// </returns>
        /// <exception cref="System.ArgumentNullException">null entities</exception>
        public virtual async Task InsertAsync(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            foreach (var entity in entities)
            {
                this.Entities.Add(entity);
            }

            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>the task</returns>
        /// <exception cref="System.ArgumentNullException">the entities</exception>
        public async Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.Entities.Add(entity);

            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>the value</returns>
        /// <exception cref="System.ArgumentNullException">the entity</exception>
        public virtual int Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.context.SaveChanges();
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// the value
        /// </returns>
        /// <exception cref="System.ArgumentNullException">the entity</exception>
        public async Task<int> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            return await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>
        /// the value
        /// </returns>
        /// <exception cref="System.ArgumentNullException">null entities</exception>
        public virtual async Task UpdateAsync(ICollection<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            await this.context.SaveChangesAsync();
        }
    }
}