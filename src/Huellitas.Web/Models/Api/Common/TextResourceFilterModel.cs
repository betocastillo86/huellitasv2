//-----------------------------------------------------------------------
// <copyright file="TextResourceFilterModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api
{
    /// <summary>
    /// Text Resource Filter Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.BaseFilterModel" />
    public class TextResourceFilterModel : BaseFilterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextResourceFilterModel"/> class.
        /// </summary>
        public TextResourceFilterModel()
        {
            this.MaxPageSize = 50;
        }

        /// <summary>
        /// Gets or sets the keyword.
        /// </summary>
        /// <value>
        /// The keyword.
        /// </value>
        public string Keyword { get; set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}