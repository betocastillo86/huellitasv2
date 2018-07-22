//-----------------------------------------------------------------------
// <copyright file="MessageExceptionFinder.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Exceptions
{
    using System;
    using Beto.Core.Exceptions;

    /// <summary>
    /// Message Exception Finder
    /// </summary>
    /// <seealso cref="Beto.Core.Exceptions.IMessageExceptionFinder" />
    public class MessageExceptionFinder : IMessageExceptionFinder
    {
        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>the return</returns>
        public static string GetErrorMessage(HuellitasExceptionCode code)
        {
            switch (code)
            {
                case HuellitasExceptionCode.RowNotFound:
                    return "El registro no se ha encontrado";

                case HuellitasExceptionCode.BadArgument:
                    return "Argumento invalido";

                case HuellitasExceptionCode.ShelterNotFound:
                    return "El refugio relacionado no existe";

                case HuellitasExceptionCode.InvalidForeignKey:
                    return "El registro que se desea relacionar no existe";

                case HuellitasExceptionCode.UserEmailAlreadyUsed:
                    return "El correo electrónico ya se encuentra registrado";

                case HuellitasExceptionCode.InvalidIndex:
                    return "Esta llave se encuentra duplicada";

                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the error message depending of the exception code
        /// </summary>
        /// <typeparam name="T">the type of errors</typeparam>
        /// <param name="exceptionCode">The exception code.</param>
        /// <returns>
        /// The text of exception
        /// </returns>
        public string GetErrorMessage<T>(T exceptionCode)
        {
            if (exceptionCode is HuellitasExceptionCode)
            {
                return MessageExceptionFinder.GetErrorMessage((HuellitasExceptionCode)Enum.Parse(typeof(HuellitasExceptionCode), exceptionCode.ToString()));
            }
            else
            {
                return string.Empty;
            }
        }
    }
}