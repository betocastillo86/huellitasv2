//-----------------------------------------------------------------------
// <copyright file="SystemSettingModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// System Setting Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.BaseModel" />
    public class SystemSettingModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the keyword.
        /// </summary>
        /// <value>
        /// The keyword.
        /// </value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }
    }
}