using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Huellitas.Web.Models.Api;
using NUnit.Framework;

namespace Huellitas.Web.Tests.Controllers.Pets
{
    [TestFixture]
    public class PetsControllerTests : BaseControllerTests
    {
        [Test]
        public async Task Post_RescuerUser_OnYearForClosingDate()
        {
            var model = this.GetNewPet();

            var response = await this.CreateClient()
                .AddAuthenticationRescuer()
                .PostAsync<BaseModel>($"/api/pets", model);

            var pet = await this.GetPetById(response.Content.Id);

            var diffDays = (pet.ClosingDate - DateTime.UtcNow).Value.Days;

            Assert.AreEqual(359, diffDays);
        }

        [Test]
        public async Task Post_RescuerUser_OneMonthForClosingDate()
        {
            var model = this.GetNewPet();

            var response = await this.CreateClient()
                .AddAuthenticationPublic()
                .PostAsync<BaseModel>($"/api/pets", model);

            var pet = await this.GetPetById(response.Content.Id);

            var diffDays = (pet.ClosingDate - DateTime.UtcNow).Value.Days;

            Assert.AreEqual(29, diffDays);
        }

        [Test]
        public async Task Post_RescuerUser_NullClosingDate()
        {
            var model = this.GetNewPet();

            var response = await this.CreateClient()
                .AddAuthenticationAdmin()
                .PostAsync<BaseModel>($"/api/pets", model);

            var pet = await this.GetPetById(response.Content.Id);

            Assert.Null(pet.ClosingDate);
        }

        private async Task<PetModel> GetPetById(int id)
        {
            return (await this.CreateClient()
                .AddAuthenticationAdmin()
                .GetAsync<PetModel>($"/api/pets/{id}")).Content;
        }

        private PetModel GetNewPet()
        {
            return new PetModel
            { 
                Name = "the name",
                Body = "the complete descriptionthe complete descriptionthe complete descriptionthe complete descriptionthe complete descriptionthe complete description",
                Location = new LocationModel { Id = 1 },
                Months = 5,
                Size = new ContentAttributeModel<int> { Value = 3 },
                Type = Data.Entities.ContentType.Pet,
                Files = new List<FileModel> { new FileModel { Id = 1 }, new FileModel { Id = 2 } } ,
                Genre = new ContentAttributeModel<int> { Value = 8 },
                Subtype = new ContentAttributeModel<int> { Value = 1 }
            };
        }
    }
}
