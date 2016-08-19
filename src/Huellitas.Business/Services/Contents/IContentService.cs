using Huellitas.Data.Entities;
using Huellitas.Data.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Business.Services.Contents
{
    public interface IContentService
    {
        IPagedList<Content> Search(string keyword = null, 
            ContentType? contentType = null, 
            IList<FilterAttribute> attributesFilter = null, 
            int pageSize = int.MaxValue, 
            int page = 0);
    }
}
