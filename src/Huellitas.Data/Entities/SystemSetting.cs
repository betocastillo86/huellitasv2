using System;
using System.Collections.Generic;

namespace Huellitas.Data.Entities
{
    public partial class SystemSetting : BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
