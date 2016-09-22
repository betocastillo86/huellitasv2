using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace Huellitas.Business.Helpers
{
    public class HttpContextHelpers : IHttpContextHelpers
    {

        #region props
        private readonly IHttpContextAccessor _accesor;
        private HttpContext _httpContext { get { return _accesor.HttpContext; } }
        #endregion

        public HttpContextHelpers(IHttpContextAccessor accesor)
        {
            _accesor = accesor;
        }

        public virtual string GetCurrentIpAddress()
        {
            if (!IsRequestAvailable())
                return string.Empty;

            var result = "";
            if (_accesor.HttpContext.Request.Headers != null)
            {
                //////The X-Forwarded-For (XFF) HTTP header field is a de facto standard
                //////for identifying the originating IP address of a client
                //////connecting to a web server through an HTTP proxy or load balancer.
                var forwardedHttpHeader = "X-FORWARDED-FOR";

                //it's used for identifying the originating IP address of a client connecting to a web server
                //through an HTTP proxy or load balancer. 
                string xff = _httpContext.Request.Headers.Keys
                    .Where(x => forwardedHttpHeader.Equals(x, StringComparison.CurrentCultureIgnoreCase))
                    .Select(k => _httpContext.Request.Headers[k])
                    .FirstOrDefault();

                //if you want to exclude private IP addresses, then see http://stackoverflow.com/questions/2577496/how-can-i-get-the-clients-ip-address-in-asp-net-mvc
                if (!String.IsNullOrEmpty(xff))
                {
                    string lastIp = xff.Split(new[] { ',' }).FirstOrDefault();
                    result = lastIp;
                }
            }

            if (String.IsNullOrEmpty(result) && _httpContext.Request.Host.HasValue)
            {
                result = _httpContext.Request.Host.Value;
            }

            //some validation
            if (result == "::1")
                result = "127.0.0.1";
            //remove port
            if (!String.IsNullOrEmpty(result))
            {
                int index = result.IndexOf(":", StringComparison.CurrentCultureIgnoreCase);
                if (index > 0)
                    result = result.Substring(0, index);
            }
            return result;

        }

        public virtual string GetThisPageUrl(bool includeQueryString)
        {
            string url = string.Empty;
            if (!IsRequestAvailable())
                return url;

            if (includeQueryString)
            {
                url = _httpContext.Request.Path;
            }
            else
            {
                if (_httpContext.Request.PathBase != null)
                {
                    url = _httpContext.Request.PathBase;
                }
            }
            url = url.ToLowerInvariant();
            return url;
        }

        private string TryGetRefferUrl()
        {
            string referrerUrl = string.Empty;

            //URL referrer is null in some case (for example, in IE 8)
            if (IsRequestAvailable() && _httpContext.Request.Headers.ContainsKey("Referer"))
                referrerUrl = _httpContext.Request.Headers["Referer"];

            return referrerUrl;
        }

        protected virtual bool IsRequestAvailable()
        {
            try
            {
                if (_accesor?.HttpContext?.Request == null)
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public virtual bool IsStaticResource(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            string path = request.Path;
            string extension = System.IO.Path.GetExtension(request.Path);

            if (extension == null) return false;

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
    }
}
