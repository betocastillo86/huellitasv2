//-----------------------------------------------------------------------
// <copyright file="ContentModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api.Contents
{
    using System.Collections.Generic;

    /// <summary>
    /// Content Model
    /// </summary>
    /// <seealso cref="Huellitas.Web.Models.Api.Contents.ContentBaseModel" />
    public class ContentModel : ContentBaseModel
    {
        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        public IList<ContentAttributeModel> Attributes { get; set; }
    }
}