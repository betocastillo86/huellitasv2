//-----------------------------------------------------------------------
// <copyright file="RelatedContentFilterModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Entities;
    using Huellitas.Web.Models.Api;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Related Content Filter Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.BaseFilterNotFluentModel" />
    public class RelatedContentFilterModel : BaseFilterNotFluentModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedContentFilterModel"/> class.
        /// </summary>
        public RelatedContentFilterModel()
        {
            this.MaxPageSize = 20;
        }
        
        /// <summary>
        /// Gets or sets the type of the relation.
        /// </summary>
        /// <value>
        /// The type of the relation.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        [Required]
        public RelationType? RelationType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [as content type].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [as content type]; otherwise, <c>false</c>.
        /// </value>
        public bool AsContentType { get; set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid()
        {
            if (this.AsContentType && !this.RelationType.HasValue)
            {
                this.AddError(Business.Exceptions.HuellitasExceptionCode.BadArgument, "AsContentType no puede ser True si no tiene filtro de RelationType", "AsContentType");
            }

            if (this.RelationType.HasValue && !Enum.IsDefined(this.RelationType.Value.GetType(), this.RelationType.Value.ToString()))
            {
                this.AddError(Business.Exceptions.HuellitasExceptionCode.BadArgument, "Tipo de relación invalida", "RelationType");
            }

            return base.IsValid();
        }
    }
}