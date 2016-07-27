using System;
using System.Collections.Generic;

namespace Huellitas.Data.Entities
{
    public partial class Category
    {
        public Category()
        {
            ContentCategory = new HashSet<ContentCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ContentCategory> ContentCategory { get; set; }
    }
}
