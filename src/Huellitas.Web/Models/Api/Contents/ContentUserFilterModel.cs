//-----------------------------------------------------------------------
// <copyright file="ContentUserFilterModel.cs" company="Huellitas sin hogar">
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
    /// Content User Filter Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.BaseFilterModel" />
    public class ContentUserFilterModel : BaseFilterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentUserFilterModel"/> class.
        /// </summary>
        public ContentUserFilterModel()
        {
            this.MaxPageSize = 20;
        }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the type of the relation.
        /// </summary>
        /// <value>
        /// The type of the relation.
        /// </value>
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public ContentUserRelationType? RelationType { get; set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid()
        {
            if (!this.RelationType.HasValue)
            {
                this.AddError("RelationType", "El tipo de relación es obligatorio");
            }
            else if (!Enum.IsDefined(typeof(ContentUserRelationType), this.RelationType))
            {
                this.AddError("RelationType", "El tipo de relación no existe");
            }

            return base.IsValid();
        }
    }
}