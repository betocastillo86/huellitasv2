//-----------------------------------------------------------------------
// <copyright file="FileService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Files
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Exceptions;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;
    using Microsoft.EntityFrameworkCore;

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
        /// The content file repository
        /// </summary>
        private readonly IRepository<ContentFile> contentFileRepository;

        /// <summary>
        /// The files helper
        /// </summary>
        private readonly IFilesHelper filesHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileService"/> class.
        /// </summary>
        /// <param name="fileRepository">The file repository.</param>
        /// <param name="filesHelper">The file helper</param>
        /// <param name="contentFileRepository">content file repository</param>
        public FileService(
            IRepository<File> fileRepository,
            IFilesHelper filesHelper,
            IRepository<ContentFile> contentFileRepository)
        {
            this.fileRepository = fileRepository;
            this.filesHelper = filesHelper;
            this.contentFileRepository = contentFileRepository;
        }

        /// <summary>
        /// Gets the files by content identifier.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <returns>
        /// the list of files
        /// </returns>
        public IList<File> GetByContentId(int contentId)
        {
            return this.contentFileRepository.Table
                .Where(c => c.ContentId == contentId)
                .Select(c => c.File)
                .ToList();
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

        /// <summary>
        /// Inserts the content file asynchronous.
        /// </summary>
        /// <param name="contentFile">The content file.</param>
        /// <returns>
        /// the result
        /// </returns>
        public async Task InsertContentFileAsync(ContentFile contentFile)
        {
            ////TODO:Test
            try
            {
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