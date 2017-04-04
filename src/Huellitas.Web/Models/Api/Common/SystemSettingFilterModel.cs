//-----------------------------------------------------------------------
// <copyright file="SystemSettingFilterModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.Common
{
    /// <summary>
    /// System Setting Filter Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.Common.BaseFilterModel" />
    public class SystemSettingFilterModel : BaseFilterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemSettingFilterModel"/> class.
        /// </summary>
        public SystemSettingFilterModel()
        {
            this.MaxPageSize = 20;
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