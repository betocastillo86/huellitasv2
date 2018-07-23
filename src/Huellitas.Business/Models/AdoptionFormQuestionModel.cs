//-----------------------------------------------------------------------
// <copyright file="AdoptionFormQuestionModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Models
{
    using Huellitas.Data.Entities;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Adoption Form Question Model
    /// </summary>
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public class AdoptionFormQuestionModel : BaseEntity
    {
        /// <summary>
        /// Gets or sets the question.
        /// </summary>
        /// <value>
        /// The question.
        /// </value>
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets the type of the question.
        /// </summary>
        /// <value>
        /// The type of the question.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public AdoptionFormQuestionType QuestionType { get; set; }

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        public string[] Options { get; set; }

        /// <summary>
        /// Gets or sets the question parent identifier.
        /// </summary>
        /// <value>
        /// The question parent identifier.
        /// </value>
        public int? QuestionParentId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AdoptionFormQuestionModel"/> is required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if required; otherwise, <c>false</c>.
        /// </value>
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets the recommendations.
        /// </summary>
        /// <value>
        /// The recommendations.
        /// </value>
        public string Recommendations { get; set; }

        /// <summary>
        /// Gets or sets the display order.
        /// </summary>
        /// <value>
        /// The display order.
        /// </value>
        public int DisplayOrder { get; set; }
    }
}