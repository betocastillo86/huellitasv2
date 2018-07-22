//-----------------------------------------------------------------------
// <copyright file="SystemSetting.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using Beto.Core.Data.Entities;

namespace Huellitas.Data.Entities
{
    /// <summary>
    /// System Setting
    /// </summary>
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public partial class SystemSetting : BaseEntity, ISettingEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }
    }
}