//-----------------------------------------------------------------------
// <copyright file="IAdoptionFormService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System.Collections.Generic;
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
        /// <param name="sharedToUserId">Filter that search what forms have been share with it</param>
        /// <param name="parentUserId">the parent user identifier</param>
        /// <param name="allRelatedToUserId">all the forms related to an user</param>
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
            int? sharedToUserId = null,
            int? parentUserId = null,
            int? allRelatedToUserId = null,
            AdoptionFormAnswerStatus? lastStatus = null,
            AdoptionFormOrderBy orderBy = AdoptionFormOrderBy.CreationDate,
            int page = 0,
            int pageSize = int.MaxValue);

        /// <summary>
        /// Gets the answers.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the list</returns>
        IList<AdoptionFormAnswer> GetAnswers(int id);

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the list of attributes</returns>
        IList<AdoptionFormAttribute> GetAttributes(int id);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the entity</returns>
        AdoptionForm GetById(int id);

        /// <summary>
        /// Inserts the specified form.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>the task</returns>
        Task Insert(AdoptionForm form);

        /// <summary>
        /// Inserts the answer.
        /// </summary>
        /// <param name="answer">The answer.</param>
        /// <returns>the task</returns>
        Task InsertAnswer(AdoptionFormAnswer answer);

        /// <summary>
        /// Inserts the user.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the task</returns>
        Task InsertUser(AdoptionFormUser entity);

        /// <summary>
        /// Determines whether [is user in adoption form] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="adoptionFormId">The adoption form identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is user in adoption form] [the specified user identifier]; otherwise, <c>false</c>.
        /// </returns>
        bool IsUserInAdoptionForm(int userId, int adoptionFormId);

        /// <summary>
        /// Updates the specified form.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>the task</returns>
        Task Update(AdoptionForm form);

        IDictionary<int, int> CountAdoptionFormsByContents(int[] contentIds, AdoptionFormAnswerStatus? status = null);
    }
}