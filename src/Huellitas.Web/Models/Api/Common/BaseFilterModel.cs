using Huellitas.Business.Exceptions;
using Huellitas.Web.Infraestructure.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Web.Models.Api.Common
{
    public abstract class BaseFilterModel 
    {
        public string orderBy { get; set; }

        public int page { get; set; } = 0;

        public int pageSize { get; set; } = 10;

        protected int MaxPageSize { get; set; } = 50;

        protected string[] ValidOrdersBy { get; set; }

        #region Methods
        public abstract bool IsValid();


        private IList<ApiError> _errors;
        public IList<ApiError> Errors
        {
            get
            {
                return _errors ?? (_errors = new List<ApiError>());
            }
        }

        /// <summary>
        /// Convierte una lista separada por comas a una lista de int
        /// </summary>
        /// <param name="property"></param>
        /// <param name="valueStr"></param>
        /// <returns></returns>
        protected int[] ToIntList(string property, string valueStr)
        {
            try
            {
                if (string.IsNullOrEmpty(valueStr))
                    return new int[0];

                return valueStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToArray();
            }
            catch (Exception)
            {
                AddError(HuellitasExceptionCode.BadArgument.ToString(), $"La lista de valores para el campo {property} es invalida", property);
                return new int[0];
            }
        }

        protected void AddError(string code, string message, string target = null)
        {
            Errors.Add(new ApiError()
            {
                Code = HuellitasExceptionCode.BadArgument.ToString(),
                Target = target,
                Message = message
            });
        }

        /// <summary>
        /// Realiza validaciones genericas del modelo
        /// </summary>
        protected void GeneralValidations()
        {
            if (pageSize > MaxPageSize)
                AddError(HuellitasExceptionCode.BadArgument.ToString(), $"Tamaño máximo de paginación excedido. El máximo es {MaxPageSize}", "PageSize");

            if (page < 0)
                AddError(HuellitasExceptionCode.BadArgument.ToString(), "La pagina debe ser mayor a 0", "Page");

            if (!string.IsNullOrEmpty(orderBy) && !ValidOrdersBy.Contains(orderBy.ToLower()))
                AddError(HuellitasExceptionCode.BadArgument.ToString(), $"El parametro orderBy no es valido. Las opciones son: {string.Join(",", ValidOrdersBy)}");
        }
        #endregion
    }
}
