using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using Microsoft.AspNetCore.Hosting;

namespace JLForecasterWeb.Services
{
    public class FileService: IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void AZFileStore(string FileName, string LocalFilePath, string FileType)
        {
            // using an environment variable.
            // ToDo: Remove hardcoding
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=ajamesstorage;AccountKey=VFN0KNkXukbU+zXkXSEw82+J7L7jso/A6la/eG0MJFu9hFPLF7W5NZd09QsPcytDKDc/aA4BbBdWqv6nG+4Ziw==;EndpointSuffix=core.windows.net";

            // Name of the share, directory, and file we'll create
            string shareName = "finance";
            string dirName = FileType;
            string fileName = FileName;
            // Path to the local file to upload
            string localFilePath = LocalFilePath;

            // Get a reference to a share and then create it
            ShareClient share = new ShareClient(connectionString, shareName);
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
            //share.Create();

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
    }
}
