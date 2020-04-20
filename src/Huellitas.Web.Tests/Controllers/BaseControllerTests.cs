using Beto.Core.Helpers;
using Beto.Core.Web.Api;
using Huellitas.Data.Entities;
using Huellitas.Web.Models.Api;
using Huellitas.Web.Tests.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Huellitas.Web.Tests.Controllers
{
    public class BaseControllerTests
    {
        protected ApiWebApplicationFactory factory;

        protected HttpClient client;

        protected readonly int adminUserId = 1;

        protected readonly int publicUserId = 2;

        protected string currentEmailAuthenticated = string.Empty;

        public static int? CurrentUserAuthenticated = null;

        [OneTimeSetUp]
        public void InitiateServer()
        {
            this.factory = new ApiWebApplicationFactory();
            this.client = this.factory.CreateClient();
        }

        protected async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        protected async Task<BaseApiErrorModel> GetErrorResponseModel(HttpResponseMessage response)
        {
            return await this.GetResponseContent<BaseApiErrorModel>(response);
        }

        protected StringContent GetRequestContent(object obj) =>
            new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

        public async Task<TypedResponseModel<T>> PostAsync<T>(string requestUri, object model) where T : class
        {
            var content = this.GetRequestContent(model);

            var response = await this.client.PostAsync(requestUri, content);

            var result = typeof(T) != typeof(EmptyContentModel) ? await this.GetResponseContent<T>(response) : (T)null;

            return new TypedResponseModel<T> { Content = result, Response = response };
        }

        public BaseControllerTests CreateClient()
        {
            return this;
        }

        public BaseControllerTests AddAuthentication(int userId)
        {
            CurrentUserAuthenticated = userId;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
            return this;
        }

        public BaseControllerTests AddAuthenticationPublic()
        {
            return this.AddAuthentication(publicUserId);
        }

        public BaseControllerTests AddAuthenticationAdmin()
        {
            return this.AddAuthentication(adminUserId);
        }

        public BaseControllerTests RemoveAuthentication()
        {
            CurrentUserAuthenticated = null;
            client.DefaultRequestHeaders.Authorization = null;
            return this;
        }

        public async Task<TypedResponseModel<T>> PutAsync<T>(string requestUri, object model) where T : class
        {
            var content = this.GetRequestContent(model);

            var response = await this.client.PutAsync(requestUri, content);

            var result = typeof(T) != typeof(EmptyContentModel) ? await this.GetResponseContent<T>(response) : (T)null;

            return new TypedResponseModel<T> { Content = result, Response = response };
        }

        public async Task<TypedResponseModel<T>> GetAsync<T>(string requestUri) where T : class
        {
            var response = await this.client.GetAsync(requestUri);

            var result = typeof(T) != typeof(EmptyContentModel) ? await this.GetResponseContent<T>(response) : (T)null;

            return new TypedResponseModel<T> { Content = result, Response = response };
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            return await this.client.DeleteAsync(requestUri);
        }

        protected void Assert400(HttpResponseMessage result)
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        protected void Assert200(HttpResponseMessage result)
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        protected void Assert201(HttpResponseMessage result)
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        protected void Assert401(HttpResponseMessage result)
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        protected void Assert404(HttpResponseMessage result)
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        protected void Assert403(HttpResponseMessage result)
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
        }

        protected void AssertTargetError(BaseApiErrorModel error, string target)
        {
            Assert.IsTrue(error.Error.Details.Any(c => c.Target.Equals(target)));
        }

        protected void AssertErrorCode(BaseApiErrorModel error, string errorCode)
        {
            Assert.AreEqual(error.Error.Code, errorCode);
        }

        protected async Task<UserModel> InsertUserAsync(string email = null)
        {
            var model = new UserModel { Name = "name", Email = email ?? this.GetRandomEmail(), Password = "123456" };

            var response = await this.PostAsync<BaseModel>("/api/users", model);

            model.Id = response.Content.Id;

            return model;
        }

        protected string GetRandomEmail()
        {
            return $"{StringHelpers.GetRandomStringNoSpecialCharacters()}@{StringHelpers.GetRandomStringNoSpecialCharacters()}.com";
        }
    }
}
