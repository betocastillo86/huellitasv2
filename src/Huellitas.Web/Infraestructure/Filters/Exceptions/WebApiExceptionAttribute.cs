using Huellitas.Business.Exceptions;
using Huellitas.Business.Extensions.Services;
using Huellitas.Business.Services.Common;
using Huellitas.Web.Infraestructure.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Web.Infraestructure.Filters.Exceptions
{
    public class WebApiExceptionAttribute : ExceptionFilterAttribute
    {

        #region props
        private readonly ILogService _logService;
        private readonly IHostingEnvironment _hostingEnvironment;
        #endregion

        #region ctor
        public WebApiExceptionAttribute(ILogService logService, IHostingEnvironment hostingEnvironment)
        {
            _logService = logService;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        public override void OnException(ExceptionContext context)
        {
            //Este Filtro solo es ejectutado por controladores de tipo Api
            if (!((ControllerActionDescriptor)context.ActionDescriptor).ControllerTypeInfo.BaseType.Name.Equals("BaseApiController"))
                return;

            var error = new ApiError();

            //var logService = (ILogService)context.HttpContext.RequestServices.GetService(typeof(ILogService));
            //TODO:Enviar el usuario autenticado si existe
            if (_hostingEnvironment.IsDevelopment())
            {
                error.Code = "ServerError";
                error.Message = context.Exception.Message;
                error.Details = new List<ApiError>() { new ApiError() { Message = context.Exception.ToString() } };
            }
            else
            {
                error.Code = "ServerError";
                error.Message = "Ocurrió un error inesperado";
            }

            _logService.Error(context.Exception, null);
            context.Result = new ObjectResult(new { Error = error });
        }
    }
}
