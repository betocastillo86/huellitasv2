//-----------------------------------------------------------------------
// <copyright file="ShelterFilterModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api
{
    using System;
    using Data.Entities;
    using Huellitas.Web.Models.Api;

    /// <summary>
    /// Shelter Filter Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.BaseFilterModel" />
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
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public StatusType? Status { get; set; }

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
        /// <param name="canGetUnpublished">if set to <c>true</c> [can get un published].</param>
        /// <returns>
        ///   <c>true</c> if the specified can get un published is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid(bool canGetUnpublished)
        {
            var orderEnum = ContentOrderBy.CreatedDate;
            Enum.TryParse<ContentOrderBy>(this.OrderBy, true, out orderEnum);
            this.OrderByEnum = orderEnum;

            if (!canGetUnpublished && this.Status != StatusType.Published)
            {
                this.AddError("Status", "No puede obtener contenidos sin publicar");
            }

            return base.IsValid();
        }
    }
}