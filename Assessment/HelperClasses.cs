using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Assessment
{
    public class HelperClasses
    {
        public static async Task<bool> SearchInStreamAsync(Stream stream, string searchString)
        {
            char[] buffer = new char[8192]; // Chunk size
            int bytesRead;
            string leftover = string.Empty; // To handle boundary cases

            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                while ((bytesRead = await reader.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    string content = leftover + new string(buffer, 0, bytesRead);

                    if (content.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                        return true;

                    // Keep the trailing part of the content for the next chunk
                    leftover = content[^Math.Min(searchString.Length - 1, content.Length)..];
                }
            }

            return false;
        }

    }
}
