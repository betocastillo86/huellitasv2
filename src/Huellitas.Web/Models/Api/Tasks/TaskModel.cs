using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huellitas.Web.Models.Api.Tasks
{
    public class TaskModel
    {
        public int TaskId { get; set; }

        public int[] Options { get; set; }
    }
}
