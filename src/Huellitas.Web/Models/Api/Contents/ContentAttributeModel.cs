using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Web.Models.Api.Contents
{
    public class ContentAttributeModel<T>
    {
        public T Value { get; set; }

        public string Text { get; set; }
    }
}
