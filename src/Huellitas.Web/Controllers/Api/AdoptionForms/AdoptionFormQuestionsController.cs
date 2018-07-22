//-----------------------------------------------------------------------
// <copyright file="AdoptionFormQuestionsController.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Controllers.Api.AdoptionForms
{
    using Beto.Core.Caching;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Services;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Adoption Form Questions Controller
    /// </summary>
    /// <seealso cref="Huellitas.Web.Infraestructure.WebApi.BaseApiController" />
    [Route("api/adoptionforms/questions")]
    public class AdoptionFormQuestionsController : BaseApiController
    {
        /// <summary>
        /// The custom table row service
        /// </summary>
        private readonly ICustomTableService customTableRowService;

        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager cacheManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdoptionFormQuestionsController"/> class.
        /// </summary>
        /// <param name="customTableRowService">The custom table row service.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public AdoptionFormQuestionsController(
            ICustomTableService customTableRowService,
            ICacheManager cacheManager,
            IMessageExceptionFinder messageExceptionFinder) : base(messageExceptionFinder)
        {
            this.customTableRowService = customTableRowService;
            this.cacheManager = cacheManager;
        }

        /// <summary>
        /// Gets the questions of adoption forms.
        /// </summary>
        /// <returns>the questions</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var questions = this.customTableRowService.GetAdoptionFormQuestions(this.cacheManager);
            return this.Ok(questions);
        }
    }
}