using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Huellitas.Business.Exceptions;
using Huellitas.Business.Extensions;
using Huellitas.Business.Services;
using Huellitas.Web.Infraestructure.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Huellitas.Web.Infraestructure.Middleware
{
    public sealed class HuellitasExceptionMiddleware
    {
        private readonly RequestDelegate next;

        private readonly ILogService logService;

        private readonly IHostingEnvironment env;

        public HuellitasExceptionMiddleware(
            RequestDelegate next,
            ILogService logService,
            IHostingEnvironment env)
        {
            this.next = next;
            this.logService = logService;
            this.env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                // this.logService.Error(ex);

                var jsonResponse = new ApiError()
                {
                    Code = HuellitasExceptionCode.ServerError.ToString(),
                    Message = this.env.IsDevelopment() ? ex.ToString() : "Error inesperado, intene de nuevo"
                };

                string jsonString = JsonConvert.SerializeObject(jsonResponse, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(jsonString, System.Text.Encoding.UTF8);                
            }
        }
    }
}
