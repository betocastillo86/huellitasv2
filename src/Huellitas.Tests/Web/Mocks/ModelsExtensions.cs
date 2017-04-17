//-----------------------------------------------------------------------
// <copyright file="ModelsExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.Mocks
{
    using System.Collections.Generic;
    using Huellitas.Web.Models.Api;
    using Huellitas.Web.Models.Api;

    /// <summary>
    /// Model extension for testing
    /// </summary>
    public static class ModelsExtensions
    {
        /// <summary>
        /// Mocks the new.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the model</returns>
        public static PetModel MockNew(this PetModel model)
        {
            model.Name = "Name";
            model.Body = "Body";
            model.Subtype = new ContentAttributeModel<int>() { Value = 1 };
            model.Genre = new ContentAttributeModel<int>() { Value = 1 };
            model.Size = new ContentAttributeModel<int>() { Value = 1 };
            model.Months = 5;
            model.Files = new List<FileModel>() { new FileModel() { FileName = "a" } };
            model.Location = new Huellitas.Web.Models.Api.LocationModel() { Id = 1 };
            return model;
        }

        /// <summary>
        /// Mocks the new.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the model</returns>
        public static ShelterModel MockNew(this ShelterModel model)
        {
            model.Name = "Name";
            model.Body = "Body";
            model.Address = "cr 10 10 10";
            model.Email = "aaa@aaa.com";
            model.Facebook = "facebook.com";
            model.Files = new List<FileModel>() { new FileModel() { Id = 789, Name = "a", FileName = "a" }, new FileModel() { Id = 1112, Name = "b", FileName = "b" } };
            model.Instagram = "instagram.com";
            model.Lat = 123;
            model.Lng = 456;
            model.Owner = "Owner";
            model.Phone = "123456";
            model.Phone2 = "789123";
            model.Twitter = "twitter.com";
            model.Video = "youtube.com";
            model.AutoReply = true;
            model.Location = new Huellitas.Web.Models.Api.LocationModel() { Id = 1 };
            return model;
        }
    }
}
