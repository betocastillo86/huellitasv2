﻿//-----------------------------------------------------------------------
// <copyright file="PetModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.Contents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Huellitas.Web.Models.Api.Files;

    /// <summary>
    /// Pet Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.Contents.ContentBaseModel" />
    public class PetModel : ContentBaseModel
    {
        /// <summary>
        /// The related pets
        /// </summary>
        private IList<PetModel> relatedPets;

        /// <summary>
        /// Initializes a new instance of the <see cref="PetModel"/> class.
        /// </summary>
        public PetModel()
        {
            this.TypeId = Data.Entities.ContentType.Pet;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [automatic reply].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic reply]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoReply { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PetModel"/> is castrated.
        /// </summary>
        /// <value>
        ///   <c>true</c> if castrated; otherwise, <c>false</c>.
        /// </value>
        public bool Castrated { get; set; }

        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        /// <value>
        /// The genre.
        /// </value>
        [Required]
        public ContentAttributeModel<int> Genre { get; set; }

        /// <summary>
        /// Gets or sets the moths.
        /// </summary>
        /// <value>
        /// The moths.
        /// </value>
        [Required]
        [Range(1, int.MaxValue)]
        public int Months { get; set; }

        /// <summary>
        /// Gets or sets the related pets.
        /// </summary>
        /// <value>
        /// The related pets.
        /// </value>
        public IList<PetModel> RelatedPets
        {
            get { return this.relatedPets ?? (this.relatedPets = new List<PetModel>()); }

            set { this.relatedPets = value; }
        }

        /// <summary>
        /// Gets or sets the shelter.
        /// </summary>
        /// <value>
        /// The shelter.
        /// </value>
        public ContentBaseModel Shelter { get; set; }

        /// <summary>
        /// Gets or sets the parents.
        /// </summary>
        /// <value>
        /// The parents.
        /// </value>
        public IList<ContentUserModel> Parents { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        [Required]
        public ContentAttributeModel<int> Size { get; set; }

        /// <summary>
        /// Gets or sets the subtype.
        /// </summary>
        /// <value>
        /// The subtype.
        /// </value>
        [Required]
        public ContentAttributeModel<int> Subtype { get; set; }

        /// <summary>
        /// Gets or sets the closing date.
        /// </summary>
        /// <value>
        /// The closing date.
        /// </value>
        public DateTime? ClosingDate { get; set; }
    }
}