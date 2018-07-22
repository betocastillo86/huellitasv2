using System;
using Beto.Core.Exceptions;

namespace Huellitas.Business.Exceptions
{
    public class MessageExceptionFinder : IMessageExceptionFinder
    {
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