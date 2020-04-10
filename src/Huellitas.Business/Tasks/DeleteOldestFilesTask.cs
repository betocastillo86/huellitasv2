using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Threading.Tasks;
using Beto.Core.Data.Files;
using Huellitas.Business.Configuration;
using Huellitas.Business.Extensions;
using Huellitas.Business.Services;
using Huellitas.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Huellitas.Business.Tasks
{
    public class DeleteOldestFilesTask : ITask
    {
        private readonly IContentService contentService;

        private readonly IFilesHelper filesHelper;

        private readonly IContentSettings contentSettings;

        private readonly IHostingEnvironment hostingEnvironment;

        private readonly IFileService fileService;

        private readonly ILogService logger;

        public DeleteOldestFilesTask(
            IContentService contentService,
            IFilesHelper filesHelper,
            IContentSettings contentSettings,
            IHostingEnvironment hostingEnvironment,
            IFileService fileService,
            ILogService logger)
        {
            this.contentService = contentService;
            this.filesHelper = filesHelper;
            this.contentSettings = contentSettings;
            this.hostingEnvironment = hostingEnvironment;
            this.fileService = fileService;
            this.logger = logger;
        }

        public async Task DeleteFilesAsync()
        {
            this.logger.Debug($"{DateTime.Now} Starting delete oldest files process -----------------------------");

            var duplicatedDeleted = await this.fileService.DeleteFilesWithContentDuplicated();

            this.logger.Debug($"{DateTime.Now} Deletes files with content duplicated {duplicatedDeleted}");

            var files = this.fileService.GetInactiveFiles(20, 1000);

            this.logger.Debug($"{DateTime.Now} Files records to mark as deleted {files.Count}");

            if (files.Count > 0)
            {
                var backupFolder = System.IO.Path.Combine(this.hostingEnvironment.ContentRootPath, "backups", DateTime.Now.ToString("yyyyMMddHHmmss"));

                var filesTuple = CopyFilesToTempFolder(files, backupFolder);
                var filesToMove = filesTuple.Item1;
                var foldersToValidate = filesTuple.Item2;

                this.logger.Debug($"{DateTime.Now} Physical files to delete {filesToMove.Count}, folders to Validate {foldersToValidate.Count}");

                if (filesToMove.Count > 0)
                {
                    this.logger.Debug($"{DateTime.Now} Before creating zip file");

                    ZipFile.CreateFromDirectory(backupFolder, string.Concat(backupFolder, ".zip"));

                    System.IO.Directory.Delete(backupFolder, true);

                    this.logger.Debug($"{DateTime.Now} Zip created and backup foler deleted");

                    foreach (var file in files)
                    {
                        await this.fileService.DeleteFile(file);
                    }

                    this.logger.Debug($"{DateTime.Now} Files maked as deleted");

                    foreach (var fileToDelete in filesToMove)
                    {
                        System.IO.File.Delete(fileToDelete);
                    }

                    this.logger.Debug($"{DateTime.Now} Physical files deleted");

                    var deletedFolders = new List<string>();

                    foreach (var folder in foldersToValidate)
                    {
                        var totalFiles = System.IO.Directory.GetFiles(folder).Length;

                        if (totalFiles == 0)
                        {
                            System.IO.Directory.Delete(folder);
                            deletedFolders.Add(folder);
                        }
                    }

                    this.logger.Debug($"{DateTime.Now} Empty folders deleted total: {deletedFolders.Count} -> {string.Join(',', deletedFolders)}");
                }
                else
                {
                    foreach (var file in files)
                    {
                        await this.fileService.DeleteFile(file);
                    }

                    this.logger.Debug($"{DateTime.Now} Files maked as deleted");

                    System.IO.Directory.Delete(backupFolder, true);

                    this.logger.Debug($"{DateTime.Now} Temp folder deleted");
                }
            }
            
            Console.WriteLine($"{DateTime.Now} Process finished");
        }

        private (IList<string>, IList<string>) CopyFilesToTempFolder(IList<File> files, string tempFolder)
        {
            var filePathsToDelete = new List<string>();
            var folders = new List<string>();

            System.IO.Directory.CreateDirectory(tempFolder);

            foreach (var file in files)
            {
                this.TryAddFileToMove(file, 0, 0, tempFolder, filePathsToDelete, folders);

                this.TryAddFileToMove(file, this.contentSettings.PictureSizeWidthList, this.contentSettings.PictureSizeHeightList, tempFolder, filePathsToDelete, folders);

                this.TryAddFileToMove(file, this.contentSettings.PictureSizeWidthDetail, this.contentSettings.PictureSizeHeightDetail, tempFolder, filePathsToDelete, folders);

                this.TryAddFileToMove(file, 200, 200, tempFolder, filePathsToDelete, folders);
            }

            return (filePathsToDelete, folders);
        }

        public void TryAddFileToMove(File file, int width, int height, string tempFolder, IList<string> filePathsToDelete, IList<string> folderList)
        {
            var filepath = this.filesHelper.GetPhysicalPath(file, width, height);
            if (System.IO.File.Exists(filepath))
            {
                var folderName = System.IO.Path.Combine(tempFolder, System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(filepath)));

                filePathsToDelete.Add(filepath);

                if (!System.IO.Directory.Exists(folderName))
                {
                    System.IO.Directory.CreateDirectory(folderName);
                }

                System.IO.File.Copy(filepath, System.IO.Path.Combine(folderName, System.IO.Path.GetFileName(filepath)), true);

                var folder = System.IO.Path.GetDirectoryName(filepath);
                if (!folderList.Contains(folder))
                {
                    folderList.Add(folder);
                }
            }
        }
    }
}