using System;
using System.Collections.Generic;

namespace Huellitas.Data.Entities
{
    public partial class CustomTable
    {
        public CustomTable()
        {
            CustomTableRow = new HashSet<CustomTableRow>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CustomTableRow> CustomTableRow { get; set; }
    }
}
