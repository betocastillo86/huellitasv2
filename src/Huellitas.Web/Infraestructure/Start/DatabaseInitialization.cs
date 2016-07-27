using Huellitas.Data.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Web.Infraestructure.Start
{
    public static class DatabaseInitialization
    {
        public static void InitDatabase(this IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                using (var context = (HuellitasContext)app.ApplicationServices.GetService(typeof(HuellitasContext)))
                {
                    context.Database.EnsureCreated();
                }
            }
        }
    }
}
