//-----------------------------------------------------------------------
// <copyright file="PetModelTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Models
{
    using Huellitas.Tests.Web.Mocks;
    using Huellitas.Web.Models.Api.Contents;
    using Huellitas.Web.Models.Extensions;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// Pet Model Test
    /// </summary>
    [TestFixture]
    public class PetModelTest
    {
        [Test]
        public void PetModelStateInvalid()
        {
            var model = new PetModel().MockNew();
            model.Name = null;

            var validationErrors = new List<ValidationResult>();
            Assert.IsFalse(Validator.TryValidateObject(model, new ValidationContext(model), validationErrors));
            Assert.AreEqual("Name", validationErrors.FirstOrDefault().MemberNames.FirstOrDefault());

            model = model.MockNew();
            model.Body = null;
            validationErrors = new List<ValidationResult>();
            Assert.IsFalse(Validator.TryValidateObject(model, new ValidationContext(model), validationErrors));
            Assert.AreEqual("Body", validationErrors.FirstOrDefault().MemberNames.FirstOrDefault());

            model = model.MockNew();
            model.Subtype = null;
            validationErrors = new List<ValidationResult>();
            Assert.IsFalse(Validator.TryValidateObject(model, new ValidationContext(model), validationErrors));
            Assert.AreEqual("Subtype", validationErrors.FirstOrDefault().MemberNames.FirstOrDefault());

            model = model.MockNew();
            model.Genre = null;
            validationErrors = new List<ValidationResult>();
            Assert.IsFalse(Validator.TryValidateObject(model, new ValidationContext(model), validationErrors));
            Assert.AreEqual("Genre", validationErrors.FirstOrDefault().MemberNames.FirstOrDefault());

            model = model.MockNew();
            model.Size = null;
            validationErrors = new List<ValidationResult>();
            Assert.IsFalse(Validator.TryValidateObject(model, new ValidationContext(model), validationErrors));
            Assert.AreEqual("Size", validationErrors.FirstOrDefault().MemberNames.FirstOrDefault());

            model = model.MockNew();
            model.Files = null;
            var modelState = new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary();
            Assert.IsFalse(model.IsValid(modelState));          
            Assert.AreEqual("Images", modelState.FirstOrDefault().Key);

            model = model.MockNew();
            model.Location = null;
            modelState = new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary();
            Assert.IsFalse(model.IsValid(modelState));
            Assert.IsNotNull(modelState["Location"]);
            Assert.IsNotNull(modelState["Shelter"]);

            ////model = model.MockNew();
            ////model.Moths = 0;
            ////validationErrors = new List<ValidationResult>();
            ////Assert.IsFalse(Validator.TryValidateObject(model, new ValidationContext(model), validationErrors));
            ////Assert.AreEqual("Moths", validationErrors.FirstOrDefault().MemberNames.FirstOrDefault());
        }

        /// <summary>
        /// Pets the model state valid.
        /// </summary>
        [Test]
        public void PetModelStateValid()
        {
            var model = new PetModel().MockNew();
            var validationErrors = new List<ValidationResult>();
            Assert.IsTrue(Validator.TryValidateObject(model, new ValidationContext(model), validationErrors));
        }
    }
}