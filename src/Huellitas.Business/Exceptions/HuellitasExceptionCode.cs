using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Business.Exceptions
{
    public enum HuellitasExceptionCode
    {
        [Description("La fila no existe")]
        RowNotFound = 2000,

        [Description("Parametro mal enviado")]
        BadArgument = 2001
    }
}
