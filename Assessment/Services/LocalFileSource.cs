using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Assessment.Interfaces;

namespace Assessment.Services
{
    public class LocalFileSource : IFileSource
    {
        public async Task<Dictionary<string, string>> GetFilesAsync(string folderPath, string searchString)
        {
            var matchingFiles = new Dictionary<string, string>();

            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"The folder '{folderPath}' does not exist.");

            string[] files = Directory.GetFiles(folderPath);

            foreach (var file in files)
            {
                try
                {
                    using (var reader = new StreamReader(file))
                    {
                        char[] buffer = new char[8192]; // Chunk size
                        int bytesRead;
                        string leftover = string.Empty; // Handle cross-chunk boundaries

                        while ((bytesRead = await reader.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            string content = leftover + new string(buffer, 0, bytesRead);

                            // Check for the search string in the current chunk
                            if (content.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                            {

                                matchingFiles[file] = "Present";
                                break; // Match found, proceed to the next file
                            }
                            else
                            {
                                matchingFiles[file] = "Absent";
                            }

                            // Keep the trailing characters for the next chunk
                            leftover = content[^Math.Min(searchString.Length - 1, content.Length)..];
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading file '{file}': {ex.Message}");
                }
            }

            return matchingFiles;
        }
    }


}
