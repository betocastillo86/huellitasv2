//-----------------------------------------------------------------------
// <copyright file="NotificationFilterModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api
{
    using Huellitas.Web.Models.Api;

    /// <summary>
    /// Notification Filter Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.BaseFilterNotFluentModel" />
    public class NotificationFilterModel : BaseFilterNotFluentModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationFilterModel"/> class.
        /// </summary>
        public NotificationFilterModel()
        {
            this.MaxPageSize = 20;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

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