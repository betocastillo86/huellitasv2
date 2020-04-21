using System.Net.Http;

namespace Huellitas.Web.Tests.Models
{
    public class TypedResponseModel<T>
    {
        public T Content { get; set; }

        public HttpResponseMessage Response { get; set; }
    }
}