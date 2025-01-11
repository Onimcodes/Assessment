using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Interfaces
{
    public interface IFileSource
    {
        Task<Dictionary<string, string>> GetFilesAsync(string folderpath, string searchstring);   
    }
}
