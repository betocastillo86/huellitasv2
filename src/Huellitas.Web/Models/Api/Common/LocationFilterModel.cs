﻿//-----------------------------------------------------------------------
// <copyright file="LocationFilterModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.Common
{
    /// <summary>
    /// Location Filter Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.Common.BaseFilterModel" />
    public class LocationFilterModel : BaseFilterModel
    {
        public LocationFilterModel()
        {
            this.MaxPageSize = 50;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>
        public int? ParentId { get; set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid()
        {
            this.GeneralValidations();

            return this.Errors.Count == 0;
        }
    }
}