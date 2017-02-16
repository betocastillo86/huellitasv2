//-----------------------------------------------------------------------
// <copyright file="UsersFilterModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.Users
{
    using Huellitas.Data.Entities.Enums;
    using Huellitas.Web.Models.Api.Common;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Users Filter Model
    /// </summary>
    public class UsersFilterModel : BaseFilterModel
    {
        /// <summary>
        /// Gets or sets the keyword.
        /// </summary>
        /// <value>
        /// The keyword.
        /// </value>
        public string Keyword { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [full object].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [full object]; otherwise, <c>false</c>.
        /// </value>
        public bool FullObject { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public RoleEnum? Role { get; set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="canShowSensitiveInfo">if set to <c>true</c> [can show sensitive information].</param>
        /// <returns>
        ///   <c>true</c> if the specified can show sensitive information is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid(bool canShowSensitiveInfo)
        {
            if (!canShowSensitiveInfo)
            {
                if (this.FullObject)
                {
                    this.AddError("FullObject", "No puede ver el objeto entero");
                }

                if (this.Role.HasValue && this.Role.Value != RoleEnum.Public)
                {
                    this.AddError("Role", "No puede ver otros roles");
                }
            }

            return base.IsValid();
        }
    }
}