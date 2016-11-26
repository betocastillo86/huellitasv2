//-----------------------------------------------------------------------
// <copyright file="FileService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Files
{
    using System.Threading.Tasks;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;

    /// <summary>
    /// File Service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.Files.IFileService" />
    public class FileService : IFileService
    {
        /// <summary>
        /// The file repository
        /// </summary>
        private readonly IRepository<File> fileRepository;

        /// <summary>
        /// The files helper
        /// </summary>
        private readonly IFilesHelper filesHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileService"/> class.
        /// </summary>
        /// <param name="fileRepository">The file repository.</param>
        /// <param name="filesHelper">The file helper</param>
        public FileService(
            IRepository<File> fileRepository,
            IFilesHelper filesHelper)
        {
            this.fileRepository = fileRepository;
            this.filesHelper = filesHelper;
        }

        /// <summary>
        /// Inserts the file asynchronous.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileContent">the bytes of the file</param>
        /// <returns>the asynchronous result</returns>
        public async Task InsertAsync(File file, byte[] fileContent)
        {
            this.fileRepository.Insert(file);

            ////Save the file on database
            await this.filesHelper.SaveFileAsync(file, fileContent);
        }
    }
}