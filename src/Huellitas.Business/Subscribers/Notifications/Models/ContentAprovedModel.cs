//-----------------------------------------------------------------------
// <copyright file="ContentAprovedModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Subscribers
{
    using Huellitas.Data.Entities;

    /// <summary>
    /// Content Aproved Model for subscribers
    /// </summary>
    public class ContentAprovedModel
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public Content Content { get; set; }
    }
}