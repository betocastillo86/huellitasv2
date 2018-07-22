//-----------------------------------------------------------------------
// <copyright file="CommentFilterModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Huellitas.Business.Exceptions;
    using Huellitas.Data.Entities;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Comment Filter Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.BaseFilterNotFluentModel" />
    public class CommentFilterModel : BaseFilterNotFluentModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentFilterModel" /> class.
        /// </summary>
        public CommentFilterModel()
        {
            this.MaxPageSize = 30;
            this.ValidOrdersBy = new string[] { "mostcommented", "recent" };
        }

        /// <summary>
        /// Gets or sets the keyword.
        /// </summary>
        /// <value>
        /// The keyword.
        /// </value>
        [StringLength(80, MinimumLength = 5)]
        public string Keyword { get; set; }

        /// <summary>
        /// Gets or sets the order by enum.
        /// </summary>
        /// <value>
        /// The order by enum.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderByComment OrderByEnum { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the content identifier.
        /// </summary>
        /// <value>
        /// The content identifier.
        /// </value>
        public int? ContentId { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>
        public int? ParentId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [with children].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [with children]; otherwise, <c>false</c>.
        /// </value>
        public bool WithChildren { get; set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="keywordRequired">if set to <c>true</c> [keyword required].</param>
        /// <returns>
        ///   <c>true</c> if the specified keyword required is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid(bool keywordRequired)
        {
            if (keywordRequired && string.IsNullOrEmpty(this.Keyword))
            {
                this.AddError(HuellitasExceptionCode.BadArgument, "La búsqueda es invalida", "Keyword");
            }

            var orderEnum = OrderByComment.Recent;
            Enum.TryParse<OrderByComment>(this.OrderBy, true, out orderEnum);
            this.OrderByEnum = orderEnum;

            return base.IsValid();
        }
    }
}