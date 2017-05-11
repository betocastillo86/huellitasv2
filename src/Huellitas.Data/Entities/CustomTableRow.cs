//-----------------------------------------------------------------------
// <copyright file="CustomTableRow.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Custom Table Row
    /// </summary>
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public partial class CustomTableRow : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTableRow"/> class.
        /// </summary>
        public CustomTableRow()
        {
            this.AdoptionForm = new HashSet<AdoptionForm>();
            this.AdoptionFormAttribute = new HashSet<AdoptionFormAttribute>();
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the additional information.
        /// </summary>
        /// <value>
        /// The additional information.
        /// </value>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Gets or sets the custom table identifier.
        /// </summary>
        /// <value>
        /// The custom table identifier.
        /// </value>
        public int CustomTableId { get; set; }

        /// <summary>
        /// Gets or sets the display order.
        /// </summary>
        /// <value>
        /// The display order.
        /// </value>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CustomTableRow"/> is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the parent custom table row identifier.
        /// </summary>
        /// <value>
        /// The parent custom table row identifier.
        /// </value>
        public int? ParentCustomTableRowId { get; set; }

        /// <summary>
        /// Gets or sets the custom table.
        /// </summary>
        /// <value>
        /// The custom table.
        /// </value>
        public virtual CustomTable CustomTable { get; set; }

        /// <summary>
        /// Gets or sets the parent custom table row.
        /// </summary>
        /// <value>
        /// The parent custom table row.
        /// </value>
        public virtual CustomTableRow ParentCustomTableRow { get; set; }

        /// <summary>
        /// Gets or sets the adoption form.
        /// </summary>
        /// <value>
        /// The adoption form.
        /// </value>
        public virtual ICollection<AdoptionForm> AdoptionForm { get; set; }

        /// <summary>
        /// Gets or sets the adoption form attribute.
        /// </summary>
        /// <value>
        /// The adoption form attribute.
        /// </value>
        public virtual ICollection<AdoptionFormAttribute> AdoptionFormAttribute { get; set; }
    }
}