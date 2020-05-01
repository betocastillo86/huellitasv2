//-----------------------------------------------------------------------
// <copyright file="CurrentDateMiddleware.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.Middleware
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Middleware for adding the current server date
    /// </summary>
    public class CurrentDateMiddleware
    {
        /// <summary>
        /// The next call
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentDateMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public CurrentDateMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>the value</returns>
        public async Task Invoke(HttpContext context)
        {
            if (context.Response != null)
            {
                context.Response.Headers.Add("x-current-date", DateTime.UtcNow.ToString());
            }

            await this.next(context);
        }
    }
}