//-----------------------------------------------------------------------
// <copyright file="BaseFilterModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Business.Exceptions;
    using Huellitas.Web.Infraestructure.WebApi;
   
    /// <summary>
    /// Base Model for filter
    /// </summary>
    public abstract class BaseFilterModel
    {
        /// <summary>
        /// The errors
        /// </summary>
        private IList<ApiError> errors;

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public IList<ApiError> Errors
        {
            get
            {
                return this.errors ?? (this.errors = new List<ApiError>());
            }
        }

        /// <summary>
        /// Gets or sets the order by.
        /// </summary>
        /// <value>
        /// The order by.
        /// </value>
        public string OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        public int Page { get; set; } = 0;

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Gets or sets the maximum size of the page.
        /// </summary>
        /// <value>
        /// The maximum size of the page.
        /// </value>
        protected int MaxPageSize { get; set; } = 50;

        /// <summary>
        /// Gets or sets the valid orders by.
        /// </summary>
        /// <value>
        /// The valid orders by.
        /// </value>
        protected string[] ValidOrdersBy { get; set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsValid();

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        /// <param name="target">The target.</param>
        protected void AddError(HuellitasExceptionCode code, string message, string target = null)
        {
            this.AddError(code.ToString(), message, target);
        }

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        /// <param name="target">The target.</param>
        protected void AddError(string code, string message, string target = null)
        {
            this.Errors.Add(new ApiError()
            {
                Code = code,
                Target = target,
                Message = message
            });
        }

        /// <summary>
        /// General the validations.
        /// </summary>
        protected void GeneralValidations()
        {
            if (this.PageSize > this.MaxPageSize)
            {
                this.AddError(HuellitasExceptionCode.BadArgument.ToString(), $"Tamaño máximo de paginación excedido. El máximo es {this.MaxPageSize}", "PageSize");
            }

            if (this.Page < 0)
            {
                this.AddError(HuellitasExceptionCode.BadArgument.ToString(), "La pagina debe ser mayor a 0", "Page");
            }

            if (!string.IsNullOrEmpty(this.OrderBy) && !this.ValidOrdersBy.Select(c => c.ToLower()).Contains(this.OrderBy.ToLower()))
            {
                this.AddError(HuellitasExceptionCode.BadArgument.ToString(), $"El parametro orderBy no es valido. Las opciones son: {string.Join(",", this.ValidOrdersBy)}", "OrderBy");
            }
        }
    }
}