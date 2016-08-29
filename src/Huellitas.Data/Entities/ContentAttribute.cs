using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Huellitas.Data.Entities
{
    public partial class ContentAttribute : BaseEntity
    {
        public int ContentId { get; set; }
        public string Value { get; set; }
        public string Attribute { get; set; }

        public virtual Content Content { get; set; }

        [NotMapped]
        public virtual ContentAttributeType AttributeType
        {
            get
            {
                return (ContentAttributeType)Enum.Parse(typeof(ContentAttributeType), this.Attribute);
            }
            set
            {
                this.Attribute = value.ToString();
            }
        }
    }
}
