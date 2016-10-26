//-----------------------------------------------------------------------
// <copyright file="ModelsExtensions.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.Mocks
{
    using System.Collections.Generic;
    using Huellitas.Web.Models.Api.Contents;
    using Huellitas.Web.Models.Api.Files;

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
            model.Moths = 5;
            model.Files = new List<FileModel>() { new FileModel() { FileName = "a" } };
            model.Location = new Huellitas.Web.Models.Api.Common.LocationModel() { Id = 1 };
            return model;
        }
    }
}
