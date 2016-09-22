using Huellitas.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities
{
    public class Log : BaseEntity
    {
        public short LogLevelId { get; set; }
        public string ShortMessage { get; set; }
        public string FullMessage { get; set; }
        public string IpAddress { get; set; }
        public int? UserId { get; set; }
        public string PageUrl { get; set; }
        public System.DateTime CreationDate { get; set; }
        public virtual User User { get; set; }

        [NotMapped]
        public virtual LogLevel LogLevel {
            get { return (LogLevel)LogLevelId; }
            set { LogLevelId = Convert.ToInt16(value); }
        }
    }
}
