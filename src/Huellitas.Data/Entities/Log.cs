//-----------------------------------------------------------------------
// <copyright file="Log.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using Huellitas.Data.Entities.Enums;

    /// <summary>
    /// The class Log
    /// </summary>
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public class Log : BaseEntity
    {
        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public System.DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the full message.
        /// </summary>
        /// <value>
        /// The full message.
        /// </value>
        public string FullMessage { get; set; }

        /// <summary>
        /// Gets or sets the <c>ip</c> address.
        /// </summary>
        /// <value>
        /// The <c>ip</c> address.
        /// </value>
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the log level.
        /// </summary>
        /// <value>
        /// The log level.
        /// </value>
        [NotMapped]
        public virtual LogLevel LogLevel
        {
            get { return (LogLevel)this.LogLevelId; }
            set { this.LogLevelId = Convert.ToInt16(value); }
        }

        /// <summary>
        /// Gets or sets the log level identifier.
        /// </summary>
        /// <value>
        /// The log level identifier.
        /// </value>
        public short LogLevelId { get; set; }

        /// <summary>
        /// Gets or sets the page URL.
        /// </summary>
        /// <value>
        /// The page URL.
        /// </value>
        public string PageUrl { get; set; }

        /// <summary>
        /// Gets or sets the short message.
        /// </summary>
        /// <value>
        /// The short message.
        /// </value>
        public string ShortMessage { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int? UserId { get; set; }
    }
}