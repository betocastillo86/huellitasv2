//-----------------------------------------------------------------------
// <copyright file="UserNotificationFilterModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api
{
    using Huellitas.Web.Models.Api;

    /// <summary>
    /// User Filter Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.BaseFilterModel" />
    public class UserNotificationFilterModel : BaseFilterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotificationFilterModel"/> class.
        /// </summary>
        public UserNotificationFilterModel()
        {
            this.MaxPageSize = 20;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [update seen].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [update seen]; otherwise, <c>false</c>.
        /// </value>
        public bool UpdateSeen { get; set; } = true;

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