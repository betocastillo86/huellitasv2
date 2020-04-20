using Huellitas.Data.Entities;
using Huellitas.Web.Models.Api;
using Huellitas.Web.Tests.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huellitas.Web.Tests.Controllers.AdoptionForms
{
    [TestFixture]
    public class AdoptionFormsControllerTests : BaseControllerTests
    {
        [Test]
        public async Task PostMarkAsRead_NoSession_401()
        {
            var response = await this.CreateClient()
                .RemoveAuthentication()
                .PostAsync<EmptyContentModel>($"/api/adoptionforms/1/markAsRead", new EmptyContentModel { });

            this.Assert401(response.Response);
        }

        [Test]
        public async Task PostMarkAsRead_PublicUser_404()
        {
            var response = await this.CreateClient()
                .AddAuthenticationPublic()
                .PostAsync<EmptyContentModel>($"/api/adoptionforms/0/markAsRead", new EmptyContentModel { });

            this.Assert404(response.Response);
        }

        [Test]
        public async Task PostMarkAsRead_OtherUser_403()
        {
            var user = await this.InsertUserAsync();

            var response = await this.CreateClient()
                .AddAuthentication(user.Id)
                .PostAsync<EmptyContentModel>($"/api/adoptionforms/1/markAsRead", new EmptyContentModel { });

            this.Assert403(response.Response);
        }

        [Test]
        public async Task PostMarkAsRead_FormMakedAsRead_200()
        {
            var oldForm = await this.GetForm(1);

            var response = await this.CreateClient()
                .AddAuthenticationPublic()
                .PostAsync<EmptyContentModel>($"/api/adoptionforms/1/markAsRead", new EmptyContentModel { });

            var newForm = await this.GetForm(1);

            this.Assert200(response.Response);
            Assert.IsFalse(oldForm.AlreadyOpened);
            Assert.IsTrue(newForm.AlreadyOpened);
        }

        private async Task<AdoptionFormModel> GetForm(int id)
        {
            return (await this.CreateClient()
                    .AddAuthenticationAdmin()
                    .GetAsync<AdoptionFormModel>($"/api/adoptionforms/{id}")).Content;
        }


    }
}
