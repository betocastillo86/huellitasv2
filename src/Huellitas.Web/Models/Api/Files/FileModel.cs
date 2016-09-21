using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Web.Models.Api.Files
{
    public class FileModel
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string Name { get; set; }

        public string MimeType { get; set; }
    }
}
