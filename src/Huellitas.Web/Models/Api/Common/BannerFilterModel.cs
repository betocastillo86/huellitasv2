//-----------------------------------------------------------------------
// <copyright file="BannerFilterModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api
{
    using System;
    using Huellitas.Business.Exceptions;
    using Huellitas.Data.Entities;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Banner Filter Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.BaseFilterNotFluentModel" />
    public class BannerFilterModel : BaseFilterNotFluentModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BannerFilterModel"/> class.
        /// </summary>
        public BannerFilterModel()
        {
            this.MaxPageSize = 10;
            this.ValidOrdersBy = new string[] { OrderByBanner.DisplayOrder.ToString().ToLower(), OrderByBanner.Recent.ToString().ToLower() };
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BannerFilterModel"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool? Active { get; set; }

        /// <summary>
        /// Gets or sets the keyword.
        /// </summary>
        /// <value>
        /// The keyword.
        /// </value>
        public string Keyword { get; set; }

        /// <summary>
        /// Gets or sets the order by enum.
        /// </summary>
        /// <value>
        /// The order by enum.
        /// </value>
        public OrderByBanner OrderByEnum { get; set; }

        /// <summary>
        /// Gets or sets the section.
        /// </summary>
        /// <value>
        /// The section.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public BannerSection? Section { get; set; }

        /// <summary>
        /// Returns true if the model is valid.
        /// </summary>
        /// <param name="canSelectInactive">if set to <c>true</c> [can select inactive].</param>
        /// <returns>
        ///   <c>true</c> if the specified can select inactive is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid(bool canSelectInactive)
        {
            ////TODO:Test
            if ((!this.Active.HasValue || !this.Active.Value) && !canSelectInactive)
            {
                this.AddError(HuellitasExceptionCode.BadArgument, "No tiene permisos para seleccionar inactivos", "Active");
            }

            var orderEnum = OrderByBanner.DisplayOrder;
            Enum.TryParse<OrderByBanner>(this.OrderBy, true, out orderEnum);
            this.OrderByEnum = orderEnum;

            return base.IsValid();
        }
    }
}