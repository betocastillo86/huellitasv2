//-----------------------------------------------------------------------
// <copyright file="IFileService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Files
{
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
    }
}