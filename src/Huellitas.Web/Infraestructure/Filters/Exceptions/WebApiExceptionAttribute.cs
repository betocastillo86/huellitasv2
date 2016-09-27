//-----------------------------------------------------------------------
// <copyright file="WebApiExceptionAttribute.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Filters.Exceptions
{
    using System.Collections.Generic;
    using Huellitas.Business.Extensions.Services;
    using Huellitas.Business.Services.Common;
    using Huellitas.Web.Infraestructure.WebApi;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Attribute for web <c>api</c> exceptions
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ExceptionFilterAttribute" />
    public class WebApiExceptionAttribute : ExceptionFilterAttribute
    {
        #region props

        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// The log service
        /// </summary>
        private readonly ILogService logService;

        #endregion props

        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="WebApiExceptionAttribute"/> class.
        /// </summary>
        /// <param name="logService">The log service.</param>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        public WebApiExceptionAttribute(ILogService logService, IHostingEnvironment hostingEnvironment)
        {
            this.logService = logService;
            this.hostingEnvironment = hostingEnvironment;
        }

        #endregion ctor

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <inheritdoc />
        public override void OnException(ExceptionContext context)
        {
            ////Este Filtro solo es ejectutado por controladores de tipo Api
            if (!((ControllerActionDescriptor)context.ActionDescriptor).ControllerTypeInfo.BaseType.Name.Equals("BaseApiController"))
            {
                return;
            }

            var error = new ApiError();

            ////var logService = (ILogService)context.HttpContext.RequestServices.GetService(typeof(ILogService));
            ////TODO:Enviar el usuario autenticado si existe
            if (this.hostingEnvironment.IsDevelopment())
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

            this.logService.Error(context.Exception, null);
            context.Result = new ObjectResult(new { Error = error });
        }
    }
}