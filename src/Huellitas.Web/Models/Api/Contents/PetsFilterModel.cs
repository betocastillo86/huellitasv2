using Huellitas.Data.Entities;
using Huellitas.Web.Models.Api.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Web.Models.Api.Contents
{
    public class PetsFilterModel : BaseFilterModel
    {
        public PetsFilterModel()
        {
            MaxPageSize = 20;
            ValidOrdersBy = new string[] { ContentOrderBy.CreationDate.ToString(), ContentOrderBy.DisplayOrder.ToString(), ContentOrderBy.Name.ToString() };
        }

        public string type { get; set; }

        public string age { get; set; }

        public string genre { get; set; }

        public string size { get; set; }

        public string shelter { get; set; }

        public string keyword { get; set; }

        public ContentOrderBy OrderByEnum { get; set; }

        public override bool IsValid()
        {
            this.GeneralValidations();

            var orderEnum = ContentOrderBy.CreationDate;
            Enum.TryParse<ContentOrderBy>(this.orderBy, true, out orderEnum);
            OrderByEnum = orderEnum;

            return Errors == null || Errors.Count == 0;
        }
    }
}
