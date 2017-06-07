//-----------------------------------------------------------------------
// <copyright file="HttpContextHelpers.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Helpers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Helpers for HttpContext
    /// </summary>
    /// <seealso cref="Huellitas.Business.Helpers.IHttpContextHelpers" />
    public class HttpContextHelpers : IHttpContextHelpers
    {
        /// <summary>
        /// The accessor
        /// </summary>
        private readonly IHttpContextAccessor accessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpContextHelpers"/> class.
        /// </summary>
        /// <param name="accessor">The accessor.</param>
        public HttpContextHelpers(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        /// <summary>
        /// Gets the HTTP context.
        /// </summary>
        /// <value>
        /// The HTTP context.
        /// </value>
        private HttpContext HttpContext
        {
            get
            {
                return this.accessor.HttpContext;
            }
        }

        /// <summary>
        /// Gets the current <c>ip</c> address.
        /// </summary>
        /// <returns>the value</returns>
        public virtual string GetCurrentIpAddress()
        {
            if (!this.IsRequestAvailable())
            {
                return string.Empty;
            }

            return this.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        /// <summary>
        /// Gets the this page URL.
        /// </summary>
        /// <param name="includeQueryString">if set to <c>true</c> [include query string].</param>
        /// <returns>the value</returns>
        public virtual string GetThisPageUrl(bool includeQueryString)
        {
            string url = string.Empty;
            if (!this.IsRequestAvailable())
            {
                return url;
            }

            if (includeQueryString)
            {
                url = this.HttpContext.Request.Path;
            }
            else
            {
                if (this.HttpContext.Request.PathBase != null)
                {
                    url = this.HttpContext.Request.PathBase;
                }
            }

            url = url.ToLowerInvariant();
            return url;
        }

        /// <summary>
        /// Determines whether [is static resource] [the specified request].
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        ///   <c>true</c> if [is static resource] [the specified request]; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">request parameter</exception>
        public virtual bool IsStaticResource(HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            string path = request.Path;
            string extension = System.IO.Path.GetExtension(request.Path);

            if (extension == null)
            {
                return false;
            }

            switch (extension.ToLower())
            {
                case ".axd":
                case ".ashx":
                case ".bmp":
                case ".css":
                case ".gif":
                case ".htm":
                case ".html":
                case ".ico":
                case ".jpeg":
                case ".jpg":
                case ".js":
                case ".png":
                case ".rar":
                case ".zip":
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether [is request available].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is request available]; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool IsRequestAvailable()
        {
            try
            {
                if (this.accessor?.HttpContext?.Request == null)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Tries the get <c>refferer</c> URL.
        /// </summary>
        /// <returns>the value</returns>
        private string TryGetRefferUrl()
        {
            string referrerUrl = string.Empty;

            ////URL referrer is null in some case (for example, in IE 8)
            if (this.IsRequestAvailable() && this.HttpContext.Request.Headers.ContainsKey("Referer"))
            {
                referrerUrl = this.HttpContext.Request.Headers["Referer"];
            }

            return referrerUrl;
        }
    }
}