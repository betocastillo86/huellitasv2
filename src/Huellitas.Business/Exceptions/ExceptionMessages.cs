//-----------------------------------------------------------------------
// <copyright file="ExceptionMessages.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Exceptions
{
    /// <summary>
    /// Gets the exception messages
    /// </summary>
    public static class ExceptionMessages
    {
        /// <summary>
        /// Gets the exception message.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>the message</returns>
        public static string GetMessage(HuellitasExceptionCode code)
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
    }
}
