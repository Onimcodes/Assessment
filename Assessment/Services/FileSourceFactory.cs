using System;
using System.Collections.Generic;
using System.Text;
using Assessment.Interfaces;
using Google.Apis.Drive.v3;

namespace Assessment.Services
{
    public static class FileSourceFactory
    {
        public static IFileSource CreateFileSource(FileSourceType sourceType, string connectionString = null)
        {
            return sourceType switch
            {
                FileSourceType.Local => new LocalFileSource(),
                //FileSourceType.GoogleDrive => new GoogleDriveFileSource(new DriveService()), // Pass configured DriveService
                _ => throw new NotImplementedException($"File source type '{sourceType}' is not implemented.")
            };
        }
    }
}
