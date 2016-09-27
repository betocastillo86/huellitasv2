//-----------------------------------------------------------------------
// <copyright file="PetsFilterModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.Contents
{
    using System;
    using System.Collections.Generic;
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Extensions.Services;
    using Huellitas.Business.Services.Contents;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Extensions;
    using Huellitas.Web.Models.Api.Common;
    
    /// <summary>
    /// Pet Filter Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.Common.BaseFilterModel" />
    public class PetsFilterModel : BaseFilterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PetsFilterModel"/> class.
        /// </summary>
        public PetsFilterModel()
        {
            this.MaxPageSize = 20;
            this.ValidOrdersBy = new string[] { ContentOrderBy.CreatedDate.ToString(), ContentOrderBy.DisplayOrder.ToString(), ContentOrderBy.Name.ToString() };
        }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        public string Age { get; set; }

        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        /// <value>
        /// The genre.
        /// </value>
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets the keyword.
        /// </summary>
        /// <value>
        /// The keyword.
        /// </value>
        public string Keyword { get; set; }

        /// <summary>
        /// Gets or sets the order by enum.
        /// </summary>
        /// <value>
        /// The order by enum.
        /// </value>
        public ContentOrderBy OrderByEnum { get; set; }

        /// <summary>
        /// Gets or sets the shelter.
        /// </summary>
        /// <value>
        /// The shelter.
        /// </value>
        public string Shelter { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public string Size { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid()
        {
            this.GeneralValidations();

            var orderEnum = ContentOrderBy.CreatedDate;
            Enum.TryParse<ContentOrderBy>(this.OrderBy, true, out orderEnum);
            this.OrderByEnum = orderEnum;
            return this.Errors == null || this.Errors.Count == 0;
        }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="selectedFilters">The selected filters.</param>
        /// <returns>
        ///   <c>true</c> if the specified selected filters is valid; otherwise, <c>false</c>.
        /// </returns>
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
                    this.AddError(e.Code, e.Message, currentFilterToConvert);
                }
                catch (FormatException)
                {
                    this.AddError(HuellitasExceptionCode.BadArgument, $"Valores invalidos para el campo {currentFilterToConvert}", currentFilterToConvert);
                }

                return this.Errors == null || this.Errors.Count == 0;
            }
            else
            {
                selectedFilters = null;
                return false;
            }
        }
    }
}