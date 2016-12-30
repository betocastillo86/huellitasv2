//-----------------------------------------------------------------------
// <copyright file="ShelterFilterModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.Contents
{
    using System;
    using Data.Entities;
    using Huellitas.Web.Models.Api.Common;

    /// <summary>
    /// Shelter Filter Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.Common.BaseFilterModel" />
    public class ShelterFilterModel : BaseFilterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShelterFilterModel"/> class.
        /// </summary>
        public ShelterFilterModel()
        {
            this.MaxPageSize = 20;
            this.ValidOrdersBy = new string[] { ContentOrderBy.CreatedDate.ToString(), ContentOrderBy.DisplayOrder.ToString(), ContentOrderBy.Name.ToString() };
        }

        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int? LocationId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
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
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid()
        {
            var orderEnum = ContentOrderBy.CreatedDate;
            Enum.TryParse<ContentOrderBy>(this.OrderBy, true, out orderEnum);
            this.OrderByEnum = orderEnum;

            return base.IsValid();
        }
    }
}