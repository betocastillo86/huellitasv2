//-----------------------------------------------------------------------
// <copyright file="HuellitasException.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Exceptions
{
    using System;
    using Beto.Core.Exceptions;

    /// <summary>
    /// Specific exception of <![CDATA[Huellitas]]>
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class HuellitasException : CoreException<HuellitasExceptionCode>
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
        public HuellitasException(HuellitasExceptionCode code) : base(MessageExceptionFinder.GetErrorMessage(code))
        {
            this.Code = code;
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
        public HuellitasException(string target, HuellitasExceptionCode code) : base(MessageExceptionFinder.GetErrorMessage(code))
        {
            this.Target = target;
            this.Code = code;
        }
    }
}