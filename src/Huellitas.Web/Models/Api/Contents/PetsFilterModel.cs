﻿//-----------------------------------------------------------------------
// <copyright file="PetsFilterModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api
{
    using System;
    using System.Collections.Generic;
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api;

    /// <summary>
    /// Pet Filter Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.BaseFilterModel" />
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
        public string SubType { get; set; }

        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int? LocationId { get; set; }

        /// <summary>
        /// Gets or sets the within closing date.
        /// </summary>
        /// <value>
        /// The within closing date.
        /// </value>
        public bool? WithinClosingDate { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public StatusType? Status { get; set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="canGetUnpublished">if set to <c>true</c> [can get un published].</param>
        /// <param name="selectedFilters">The selected filters.</param>
        /// <returns>
        ///   <c>true</c> if the specified can get unpublished is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid(bool canGetUnpublished, out IList<FilterAttribute> selectedFilters)
        {
            if (this.IsValid())
            {
                var orderEnum = ContentOrderBy.CreatedDate;
                Enum.TryParse<ContentOrderBy>(this.OrderBy, true, out orderEnum);
                this.OrderByEnum = orderEnum;

                if (!canGetUnpublished && this.Status != StatusType.Published)
                {
                    this.AddError("Status", "No puede obtener contenidos sin publicar");
                }

                selectedFilters = new List<FilterAttribute>();

                string currentFilterToConvert = "Age";
                try
                {
                    selectedFilters.AddRangeAttribute(ContentAttributeType.Age, this.Age);

                    currentFilterToConvert = "Genre";
                    selectedFilters.AddInt(ContentAttributeType.Genre, this.Genre);

                    currentFilterToConvert = "Size";
                    selectedFilters.AddIntList(ContentAttributeType.Size, this.Size);

                    currentFilterToConvert = "Shelter";
                    selectedFilters.AddIntList(ContentAttributeType.Shelter, this.Shelter);

                    currentFilterToConvert = "Subtype";
                    selectedFilters.AddIntList(ContentAttributeType.Subtype, this.SubType);
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