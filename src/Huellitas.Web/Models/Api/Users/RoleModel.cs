//-----------------------------------------------------------------------
// <copyright file="RoleModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api
{
    using System;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Role Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.BaseModel" />
    public class RoleModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the enum.
        /// </summary>
        /// <value>
        /// The enum.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public RoleEnum Enum
        {
            get
            {
                return (RoleEnum)this.Id;
            }

            set
            {
                this.Id = Convert.ToInt32(value);
            }
        }
    }
}