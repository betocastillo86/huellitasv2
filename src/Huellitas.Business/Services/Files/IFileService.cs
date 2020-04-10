//-----------------------------------------------------------------------
// <copyright file="IFileService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
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
        /// Deletes the content file.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="removeFileIfDoesnotHaveRelationship">removes the file if does not have any relationship</param>
        /// <returns>the task</returns>
        Task DeleteContentFile(int contentId, int fileId, bool removeFileIfDoesnotHaveRelationship = false);

        Task DeleteFile(File file);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the file</returns>
        File GetById(int id);

        /// <summary>
        /// Gets the by ids.
        /// </summary>
        /// <param name="ids">The identifiers</param>
        /// <returns>the files</returns>
        IList<File> GetByIds(int[] ids);

        /// <summary>
        /// Inserts the file asynchronous.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileContent">the bytes of the file</param>
        /// <returns>the asynchronous result</returns>
        Task InsertAsync(File file, byte[] fileContent);

        /// <summary>
        /// Inserts the content file asynchronous.
        /// </summary>
        /// <param name="contentFile">The content file.</param>
        /// <returns>the result</returns>
        Task InsertContentFileAsync(ContentFile contentFile);

        Task<int> DeleteFilesWithContentDuplicated();

        IList<File> GetInactiveFiles(int daysPassedAfterClosingDate, int take);
    }
}