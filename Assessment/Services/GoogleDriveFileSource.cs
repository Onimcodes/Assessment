using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Assessment.Interfaces;

namespace Assessment.Services
{
    public class GoogleDriveFileSource : IFileSource
    {
        public Task<Dictionary<string,string>> GetFilesAsync(string folderpath, string searchstring)
        {
            throw new NotImplementedException();
        }
    }
}
