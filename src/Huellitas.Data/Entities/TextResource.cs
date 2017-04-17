//-----------------------------------------------------------------------
// <copyright file="TextResource.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using Huellitas.Data.Entities.Enums;

    /// <summary>
    /// Text resources
    /// </summary>
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public class TextResource : BaseEntity
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

        /// <summary>
        /// Gets or sets the language identifier.
        /// </summary>
        /// <value>
        /// The language identifier.
        /// </value>
        public short LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        [NotMapped]
        public virtual LanguageEnum Language
        {
            get
            {
                return (LanguageEnum)this.LanguageId;
            }

            set
            {
                this.LanguageId = Convert.ToInt16(value);
            }
        }
    }
}