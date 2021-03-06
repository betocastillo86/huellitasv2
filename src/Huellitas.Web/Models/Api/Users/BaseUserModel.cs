﻿//-----------------------------------------------------------------------
// <copyright file="BaseUserModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api
{
    using System.ComponentModel.DataAnnotations;
    using Huellitas.Web.Models.Api;

    /// <summary>
    /// Base User Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.BaseModel" />
    public class BaseUserModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [StringLength(100, MinimumLength = 4)]
        public string Name { get; set; }
    }
}