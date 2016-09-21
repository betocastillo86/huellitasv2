using Huellitas.Business.Exceptions;
using Huellitas.Business.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Web.Infraestructure.WebApi
{
    public class BaseApiController : Controller
    {
        #region BadRequest
        protected IActionResult BadRequest(HuellitasException ex, string message = null)
        {
            var error = new ApiError();
            error.Code = ex.Code.ToString();
            error.Message = message ?? ex.Message;
            return StatusCode(400, new { Error = error });
        }

        /// <summary>
        /// Responde un bad request con errores anidados
        /// </summary>
        /// <returns></returns>
        protected IActionResult BadRequest(HuellitasExceptionCode code, IList<ApiError> errors, string target = null)
        {
            object objResponse = null;

            if (errors == null || errors.Count == 0)
            {
                objResponse = new ApiError()
                {
                    Code = code.ToString(),
                    Message = EnumHelpers.GetDescription(code),
                    Target = target
                };
            }
            else
            {
                objResponse = new ApiError()
                {
                    Code = code.ToString(),
                    Message = EnumHelpers.GetDescription(code),
                    Details = errors,
                    Target = target
                };
            }

            return StatusCode(400, new { Error = objResponse });
        }

        #endregion

        #region Ok
        /// <summary>
        /// Retorna un 200 con los headers de la paginación
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pagedList"></param>
        /// <returns></returns>
        protected IActionResult Ok<T>(IList<T> list, bool hasNextPage, int totalCount)
        {
            this.Response.Headers.Add(ApiHeadersList.PAGINATION_HASNEXTPAGE, hasNextPage.ToString());
            this.Response.Headers.Add(ApiHeadersList.PAGINATION_TOTALCOUNT, totalCount.ToString());
            this.Response.Headers.Add(ApiHeadersList.PAGINATION_COUNT, list.Count.ToString());

            return StatusCode(200, list);
        }
        #endregion
    }
}
