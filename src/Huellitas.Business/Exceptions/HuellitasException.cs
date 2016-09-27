//-----------------------------------------------------------------------
// <copyright file="HuellitasException.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Exceptions
{
    using System;
    using Huellitas.Business.Helpers;

    /// <summary>
    /// Specific exception of <![CDATA[Huellitas]]>
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class HuellitasException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HuellitasException"/> class.
        /// </summary>
        /// <param name="error">The error.</param>
        public HuellitasException(string error) : base(error)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HuellitasException"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        public HuellitasException(HuellitasExceptionCode code) : base(EnumHelpers.GetDescription(code))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HuellitasException"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="error">The error.</param>
        public HuellitasException(HuellitasExceptionCode code, string error) : base(error)
        {
            this.Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HuellitasException"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="code">The code.</param>
        public HuellitasException(string target, HuellitasExceptionCode code) : base(EnumHelpers.GetDescription(code))
        {
            this.Target = target;
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public HuellitasExceptionCode Code { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        public string Target { get; set; }
    }
}