//-----------------------------------------------------------------------
// <copyright file="CustomTable.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Custom Table
    /// </summary>
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public partial class CustomTable : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTable"/> class.
        /// </summary>
        public CustomTable()
        {
            this.CustomTableRow = new HashSet<CustomTableRow>();
        }

        /// <summary>
        /// Gets or sets the custom table row.
        /// </summary>
        /// <value>
        /// The custom table row.
        /// </value>
        public virtual ICollection<CustomTableRow> CustomTableRow { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}