using Newtonsoft.Json;

namespace Huellitas.Web.Tests.Models
{
    public class AuthResponseModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}