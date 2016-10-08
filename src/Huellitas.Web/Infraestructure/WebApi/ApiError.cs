//-----------------------------------------------------------------------
// <copyright file="ApiError.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.WebApi
{
    using System.Collections.Generic;

    /// <summary>
    /// Error model for <![CDATA[Api]]>
    /// </summary>
    public class ApiError
    {
        /// <summary>
        /// The details
        /// </summary>
        private IList<ApiError> details;

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        public IList<ApiError> Details
        {
            get
            {
                return this.details ?? (this.details = new List<ApiError>());
            }

            set
            {
                this.details = value;
            }
        }
    }
}