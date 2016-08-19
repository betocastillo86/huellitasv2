using Huellitas.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Business.Services.Contents
{
    public class FilterAttribute
    {
        public ContentAttributeType Attribute { get; set; }

        public object Value { get; set; }

        public object ValueTo { get; set; }

        public FilterAttributeType FilterType { get; set; }
    }
}
