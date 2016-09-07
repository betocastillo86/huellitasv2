using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Infraestructure
{
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList(IQueryable<T> query, int page, int pageSize)
        {
            PageIndex = page;
            PageSize = pageSize;

            //counts all rows
            TotalCount = query.Count();
            TotalPages = TotalCount / PageSize;

            if (TotalPages % PageSize > 0)
                TotalPages++;

            this.AddRange(query.Skip(page * pageSize).Take(pageSize).ToList());
        }

        /// <summary>
        /// Crea una lista vacia
        /// </summary>
        public PagedList()
        {
            
        }

        #region 
        public bool HasNextPage
        {
            get { return TotalPages > PageIndex + 1; }
        }

        public bool HasPreviousPage
        {
            get { return PageIndex > 0; }
        }

        public int PageIndex
        {
            get; private set;
        }

        public int PageSize
        {
            get; private set;
        }

        public int TotalCount
        {
            get; private set;
        }

        public int TotalPages
        {
            get; private set;
        }
        #endregion

    }
}
