using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huellitas.Data
{
    public class SeoCrawling : IEntity
    {
        public string Url { get; set; }

        public string Html { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
