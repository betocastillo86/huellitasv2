using System;
using System.Collections.Generic;

namespace Huellitas.Data.Entities
{
    public partial class File
    {
        public File()
        {
            Content = new HashSet<Content>();
            ContentFile = new HashSet<ContentFile>();
        }

        public int Id { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }

        public virtual ICollection<Content> Content { get; set; }
        public virtual ICollection<ContentFile> ContentFile { get; set; }
    }
}
