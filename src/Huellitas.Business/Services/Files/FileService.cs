//-----------------------------------------------------------------------
// <copyright file="FileService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Beto.Core.Data.Files;
    using Beto.Core.EventPublisher;
    using Exceptions;
    using Huellitas.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// File Service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.IFileService" />
    public class FileService : IFileService
    {
        /// <summary>
        /// The content file repository
        /// </summary>
        private readonly IRepository<ContentFile> contentFileRepository;

        /// <summary>
        /// the content repository
        /// </summary>
        private readonly IRepository<Content> contentRepository;

        /// <summary>
        /// The file repository
        /// </summary>
        private readonly IRepository<File> fileRepository;

        /// <summary>
        /// The files helper
        /// </summary>
        private readonly IFilesHelper filesHelper;

        /// <summary>
        /// The publisher
        /// </summary>
        private readonly IPublisher publisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileService"/> class.
        /// </summary>
        /// <param name="fileRepository">The file repository.</param>
        /// <param name="filesHelper">The files helper.</param>
        /// <param name="contentFileRepository">The content file repository.</param>
        /// <param name="publisher">The publisher.</param>
        /// <param name="contentRepository">The content repository.</param>
        public FileService(
            IRepository<File> fileRepository,
            IFilesHelper filesHelper,
            IRepository<ContentFile> contentFileRepository,
            IPublisher publisher,
            IRepository<Content> contentRepository)
        {
            this.fileRepository = fileRepository;
            this.filesHelper = filesHelper;
            this.contentFileRepository = contentFileRepository;
            this.publisher = publisher;
            this.contentRepository = contentRepository;
        }

        /// <summary>
        /// Deletes the content file.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="removeFileIfDoesnotHaveRelationship">removes the file if does not have any relationship</param>
        /// <returns>
        /// the task
        /// </returns>
        /// <exception cref="HuellitasException">Row not found</exception>
        public async Task DeleteContentFile(int contentId, int fileId, bool removeFileIfDoesnotHaveRelationship = false)
        {
            var contentFile = this.contentFileRepository.Table
                .Include(c => c.File)
                .FirstOrDefault(c => c.ContentId == contentId && c.FileId == fileId);

            if (contentFile != null)
            {
                await this.contentFileRepository.DeleteAsync(contentFile);

                ////if does not have any relationship with contents deletes the file
                if (removeFileIfDoesnotHaveRelationship &&
                    !this.contentFileRepository.Table.Any(c => c.FileId == fileId) &&
                    !this.contentRepository.Table.Any(c => c.FileId == fileId))
                {
                    await this.fileRepository.DeleteAsync(contentFile.File);
                    await this.publisher.EntityDeleted(contentFile.File);
                }
            }
            else
            {
                throw new HuellitasException(HuellitasExceptionCode.RowNotFound);
            }
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// the file
        /// </returns>
        public File GetById(int id)
        {
            return this.fileRepository.Table
                .FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// Gets the by ids.
        /// </summary>
        /// <param name="ids">The identifiers</param>
        /// <returns>
        /// the files
        /// </returns>
        public IList<File> GetByIds(int[] ids)
        {
            return this.fileRepository.Table
                .Where(c => ids.Contains(c.Id))
                .ToList();
        }

        /// <summary>
        /// Inserts the file asynchronous.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileContent">the bytes of the file</param>
        /// <returns>the asynchronous result</returns>
        public async Task InsertAsync(File file, byte[] fileContent)
        {
            await this.fileRepository.InsertAsync(file);

            ////Save the file on database
            this.filesHelper.SaveFile(file, fileContent);
        }

        /// <summary>
        /// Inserts the content file asynchronous.
        /// </summary>
        /// <param name="contentFile">The content file.</param>
        /// <returns>
        /// the result
        /// </returns>
        public async Task InsertContentFileAsync(ContentFile contentFile)
        {
            try
            {
                ////Actualiza los display order de todos los contenidos
                var existentFiles = this.contentFileRepository.Table
                    .Where(c => c.ContentId == contentFile.ContentId)
                    .OrderByDescending(c => c.DisplayOrder)
                    .ToList();

                for (int i = 0; i < existentFiles.Count; i++)
                {
                    var existentFile = existentFiles[i];
                    existentFile.DisplayOrder = (existentFiles.Count - i) + 1;
                }

                contentFile.DisplayOrder = 1;

                await this.contentFileRepository.InsertAsync(contentFile);
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException is System.Data.SqlClient.SqlException)
                {
                    var inner = (System.Data.SqlClient.SqlException)e.InnerException;

                    if (inner.Number == 547)
                    {
                        string target = string.Empty;

                        if (inner.Message.IndexOf("FK_ContentFile_File") != -1)
                        {
                            target = "File";
                        }
                        else if (inner.Message.IndexOf("FK_ContentFile_Content") != -1 || inner.Message.IndexOf("FK_ContentFile_File") != -1)
                        {
                            target = "Content";
                        }
                        else
                        {
                            throw;
                        }

                        throw new HuellitasException(target, HuellitasExceptionCode.InvalidForeignKey);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
    }
}