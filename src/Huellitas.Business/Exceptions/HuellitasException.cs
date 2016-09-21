using Huellitas.Business.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Business.Exceptions
{
    public class HuellitasException : Exception
    {
        public HuellitasExceptionCode Code { get; set; }

        public string Target { get; set; }

        public HuellitasException(string error) : base(error)
        {

        }

        public HuellitasException(HuellitasExceptionCode code) : base(EnumHelpers.GetDescription(code))
        {
        }

        public HuellitasException(HuellitasExceptionCode code, string error) : base(error)
        {
            this.Code = code;
        }

        public HuellitasException(string target, HuellitasExceptionCode code) : base(EnumHelpers.GetDescription(code))
        {
            this.Target = target;
        }

    }
}
