using Huellitas.Business.Exceptions;
using Huellitas.Business.Extensions.Services;
using Huellitas.Business.Services.Contents;
using Huellitas.Data.Entities;
using Huellitas.Web.Extensions;
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
            ValidOrdersBy = new string[] { ContentOrderBy.CreatedDate.ToString(), ContentOrderBy.DisplayOrder.ToString(), ContentOrderBy.Name.ToString() };
        }

        public string Type { get; set; }

        public string Age { get; set; }

        public string Genre { get; set; }

        public string Size { get; set; }

        public string Shelter { get; set; }

        public string Keyword { get; set; }

        public ContentOrderBy OrderByEnum { get; set; }

        public override bool IsValid()
        {
            this.GeneralValidations();

            var orderEnum = ContentOrderBy.CreatedDate;
            Enum.TryParse<ContentOrderBy>(this.OrderBy, true, out orderEnum);
            OrderByEnum = orderEnum;
            return Errors == null || Errors.Count == 0;
        }

        public bool IsValid(out IList<FilterAttribute> selectedFilters)
        {
            if (this.IsValid())
            {
                selectedFilters = new List<FilterAttribute>();

                string currentFilterToConvert = "Age";
                try
                {
                    selectedFilters.AddRangeAttribute(ContentAttributeType.Age, this.Age);
                    currentFilterToConvert = "Genre";
                    selectedFilters.Add(ContentAttributeType.Genre, this.Genre);
                    currentFilterToConvert = "Size";
                    selectedFilters.Add(ContentAttributeType.Size, this.Size.ToStringList(), FilterAttributeType.Multiple);
                    currentFilterToConvert = "Shelter";
                    selectedFilters.Add(ContentAttributeType.Shelter, this.Shelter.ToStringList(), FilterAttributeType.Multiple);
                    currentFilterToConvert = "Subtype";
                    selectedFilters.Add(ContentAttributeType.Subtype, this.Type.ToStringList(), FilterAttributeType.Multiple);
                }
                catch (HuellitasException e)
                {
                    AddError(e.Code, e.Message, currentFilterToConvert);
                }
                catch (FormatException)
                {
                    AddError(HuellitasExceptionCode.BadArgument, $"Valores invalidos para el campo {currentFilterToConvert}", currentFilterToConvert);
                }

                return Errors == null || Errors.Count == 0;
            }
            else
            {
                selectedFilters = null;
                return false;
            }
        }
    }
}
