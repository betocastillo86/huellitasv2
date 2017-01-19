//-----------------------------------------------------------------------
// <copyright file="PagedList.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Infraestructure
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Paged list
    /// </summary>
    /// <typeparam name="T">The Entity</typeparam>
    /// <seealso cref="System.Collections.Generic.List{T}" />
    /// <seealso cref="Huellitas.Data.Infraestructure.IPagedList{T}" />
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        public PagedList(IQueryable<T> query, int page, int pageSize)
        {
            this.PageIndex = page;
            this.PageSize = pageSize;

            ////counts all rows
            this.TotalCount = query.Count();
            this.TotalPages = this.TotalCount / this.PageSize;

            if (this.TotalPages % this.PageSize > 0)
            {
                this.TotalPages++;
            }

            this.AddRange(query.Skip(page * pageSize).Take(pageSize).ToList());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
        /// </summary>
        public PagedList()
        {
        }

        /// <summary>
        /// Gets a value indicating whether this instance has next page.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has next page; otherwise, <c>false</c>.
        /// </value>
        public bool HasNextPage
        {
            get
            {
                return this.TotalPages > this.PageIndex + 1;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has previous page.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has previous page; otherwise, <c>false</c>.
        /// </value>
        public bool HasPreviousPage
        {
            get
            {
                return this.PageIndex > 0;
            }
        }

        /// <summary>
        /// Gets the index of the page.
        /// </summary>
        /// <value>
        /// The index of the page.
        /// </value>
        public int PageIndex
        {
            get; private set;
        }

        /// <summary>
        /// Gets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int PageSize
        {
            get; private set;
        }

        /// <summary>
        /// Gets the total count.
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        public int TotalCount
        {
            get; private set;
        }

        /// <summary>
        /// Gets the total pages.
        /// </summary>
        /// <value>
        /// The total pages.
        /// </value>
        public int TotalPages
        {
            get; private set;
        }

        /// <summary>
        /// Make Asynchronous the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>The paged list</returns>
        public async Task<PagedList<T>> Async(IQueryable<T> query, int page, int pageSize)
        {
            ////counts all rows
            this.TotalCount = await query.CountAsync();

            var list = await query.Skip(page * pageSize).Take(pageSize).ToListAsync();
            this.AddRange(list);

            this.CalculateValues(page, pageSize);

            return this;
        }

        /// <summary>
        /// Calculates the values.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        private void CalculateValues(int page, int pageSize)
        {
            this.PageIndex = page;
            this.PageSize = pageSize;
            this.TotalPages = this.TotalCount / this.PageSize;

            if (this.TotalPages % this.PageSize > 0)
            {
                this.TotalPages++;
            }
        }
    }
}