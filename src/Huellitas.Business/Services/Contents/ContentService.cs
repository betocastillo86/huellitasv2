using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Huellitas.Data.Entities;
using Huellitas.Data.Infraestructure;
using Huellitas.Data.Core;

namespace Huellitas.Business.Services.Contents
{
    public class ContentService : IContentService
    {
        #region props
        private readonly IRepository<Content> _contentRepository;
        #endregion

        #region ctor
        public ContentService(IRepository<Content> contentRepository)
        {
            _contentRepository = contentRepository;
        }
        #endregion

        public IPagedList<Content> Search(string keyword = null,
            ContentType? contentType = null, 
            IList<FilterAttribute> attributesFilter = null, 
            int pageSize = int.MaxValue, 
            int page = 0)
        {

            var query = _contentRepository.TableNoTracking
                .Where(c => !c.Deleted);

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => c.Name.Contains(keyword) || c.Body.Contains(keyword));
            }

            return new PagedList<Content>(query, page, pageSize);
        }
    }
}
