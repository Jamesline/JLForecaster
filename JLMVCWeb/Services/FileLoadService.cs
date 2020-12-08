using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace JLForecasterWeb.Services
{
    public class FileLoadService
    {
        private readonly ILogger<FileLoadService> _logger;
        private readonly IConfiguration _config;
        private readonly string _storagekey;
        private readonly string _storageshare;
        public FileLoadService(ILogger<FileLoadService> logger,
                              IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _storagekey= _config.GetSection("StorageString").Value;
            _storageshare = _config.GetSection("StorageLocation").Value;
        }

        public void AZFileStore(string FileName, string LocalFilePath, string FileType)
        {
            _logger.LogInformation("AZFilestore: Called");
            // Name of the share, directory, and file we'll create
            string dirName = FileType;
            string fileName = FileName;
            // Path to the local file to upload
            string localFilePath = LocalFilePath;

            // Get a reference to a share and then create it
            ShareClient share = new ShareClient(_storagekey, _storageshare);
            try
            {
                // Try to create the share again
                share.Create();
            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == ShareErrorCode.ShareAlreadyExists)
            {
                // Ignore any errors if the share already exists
            }
            // Get a reference to a directory and create it
            ShareDirectoryClient directory = share.GetDirectoryClient(dirName);
            try
            {
                // Try to create the share again
                directory.Create();
            }
            catch (RequestFailedException ex)
            {
                // Ignore any errors if the share already exists
            }
            // directory.Create();

            // Get a reference to a file and upload it
            ShareFileClient file = directory.GetFileClient(fileName);
            using (FileStream stream = File.OpenRead(localFilePath))
            {
                file.Create(stream.Length);
                file.UploadRange(
                    new HttpRange(0, stream.Length),
                    stream);
            }
        }
        private void StoreFile()
        {

        }
    }
}
