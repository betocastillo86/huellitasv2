//-----------------------------------------------------------------------
// <copyright file="IFileService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Files
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Interface of File Service
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Inserts the file asynchronous.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileContent">the bytes of the file</param>
        /// <returns>the asynchronous result</returns>
        Task InsertAsync(File file, byte[] fileContent);

        /// <summary>
        /// Gets the by ids.
        /// </summary>
        /// <param name="ids">The identifiers</param>
        /// <returns>the files</returns>
        IList<File> GetByIds(int[] ids);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the file</returns>
        File GetById(int id);

        /// <summary>
        /// Gets the files by content identifier.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <returns>the list of files</returns>
        IList<File> GetByContentId(int contentId);

        /// <summary>
        /// Inserts the content file asynchronous.
        /// </summary>
        /// <param name="contentFile">The content file.</param>
        /// <returns>the result</returns>
        Task InsertContentFileAsync(ContentFile contentFile);
    }
}