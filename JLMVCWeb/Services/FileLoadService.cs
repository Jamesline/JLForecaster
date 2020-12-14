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
using Microsoft.AspNetCore.Http;

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
        public async Task<string> AZFileStorer(IFormFile loadedFile, string fileType)
        {
            string dirName = fileType;
            string fileName = loadedFile.FileName;
            ShareFileClient shareFile = StorageSetup(loadedFile, fileType);

            try
            {
                using (var stream = new MemoryStream())
                {
                    await loadedFile.CopyToAsync(stream);
                    shareFile.Create(stream.Length);
                }
                return "Success";
            }
            catch (Exception ex)
            {

                return "Fail: Error: " + ex.Message;
            }
        }
        private ShareFileClient StorageSetup(IFormFile loadedFile, string fileType)
        {
            string dirName = fileType;
            string fileName = loadedFile.FileName;

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
            ShareFileClient shareFile = directory.GetFileClient(fileName);
            return shareFile;
        }
    }
}

