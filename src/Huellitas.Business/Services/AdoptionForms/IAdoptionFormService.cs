//-----------------------------------------------------------------------
// <copyright file="IAdoptionFormService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.AdoptionForms
{
    using System.Threading.Tasks;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Infraestructure;

    /// <summary>
    /// Interface of adoption form service
    /// </summary>
    public interface IAdoptionFormService
    {
        /// <summary>
        /// Gets all the adoption forms.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="shelterId">The shelter identifier.</param>
        /// <param name="formUserId">The form user identifier.</param>
        /// <param name="contentUserId">The content user identifier.</param>
        /// <param name="lastStatus">The last status.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>the list of adoption forms</returns>
        IPagedList<AdoptionForm> GetAll(
            string user = null,
            int? contentId = null,
            int? locationId = null,
            int? shelterId = null,
            int? formUserId = null,
            int? contentUserId = null,
            AdoptionFormAnswerStatus? lastStatus = null,
            AdoptionFormOrderBy orderBy = AdoptionFormOrderBy.CreationDate,
            int page = 0,
            int pageSize = int.MaxValue);
    }
}