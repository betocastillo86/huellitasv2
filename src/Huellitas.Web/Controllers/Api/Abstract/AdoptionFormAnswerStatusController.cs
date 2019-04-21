//-----------------------------------------------------------------------
// <copyright file="AdoptionFormAnswerStatusController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Huellitas.Data.Entities;
   
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Adoption Form Answer Status Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/[controller]")]
    public class AdoptionFormAnswerStatusController : BaseApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdoptionFormAnswerStatusController"/> class.
        /// </summary>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        public AdoptionFormAnswerStatusController(IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>the list of status</returns>
        public IActionResult Get()
        {
            var list = new List<object>();
            list.Add(new { Id = Convert.ToInt32(AdoptionFormAnswerStatus.None), Name = "Sin revisar", Enum = AdoptionFormAnswerStatus.None.ToString() });
            list.Add(new { Id = Convert.ToInt32(AdoptionFormAnswerStatus.Approved), Name = "Aprobado", Enum = AdoptionFormAnswerStatus.Approved.ToString() });
            list.Add(new { Id = Convert.ToInt32(AdoptionFormAnswerStatus.Denied), Name = "Negado", Enum = AdoptionFormAnswerStatus.Denied.ToString() });
            list.Add(new { Id = Convert.ToInt32(AdoptionFormAnswerStatus.AlreadyAdopted), Name = "Adoptado previamente", Enum = AdoptionFormAnswerStatus.AlreadyAdopted.ToString() });

            return this.Ok(list);
        }
    }
}