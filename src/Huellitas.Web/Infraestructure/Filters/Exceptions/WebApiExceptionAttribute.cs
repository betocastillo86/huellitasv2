//-----------------------------------------------------------------------
// <copyright file="WebApiExceptionAttribute.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Filters
{
    using Beto.Core.Web.Api;
    using Business.Security;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Services;
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
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

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
        /// <param name="workContext">the work context</param>
        public WebApiExceptionAttribute(
            ILogService logService,
            IHostingEnvironment hostingEnvironment,
            IWorkContext workContext)
        {
            this.logService = logService;
            this.hostingEnvironment = hostingEnvironment;
            this.workContext = workContext;
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

            var error = new ApiErrorModel();
            error.Code = "ServerError";

            if (this.hostingEnvironment.IsDevelopment())
            {
                error.Message = context.Exception.ToString();
            }
            else
            {
                error.Message = "Ocurrió un error inesperado";
            }

            this.logService.Error(context.Exception, this.workContext.CurrentUser);
            context.Result = new ObjectResult(error);
            context.HttpContext.Response.StatusCode = 500;
        }
    }
}