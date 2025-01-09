using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;




 internal class Program
{
    public static async Task Main(string[] args)
    {
        //First Test
        
        string folderPath = "C:\\Users";
        string searchString = "user";
        await FileScanner.ScanFilesAsync(folderPath, searchString);


        //Second Test
        List<int> collectionA = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        List<int> collectionS = new List<int> { 5, 15, 3, 18, 37, 50, 2, 0 };

     

        // Identify duplicates
        Dictionary<int, bool> results = DuplicateIdentifier<int>.IdentifyDuplicates(collectionA, collectionS);

        // Print the results
        foreach (var result in results)
        {
            Console.WriteLine($"{result.Key}:{result.Value.ToString().ToLower()}");
        }
    }

} 
public static class FileScanner
{
   


    public static async Task ScanFilesAsync(string folderPath, string searchString)
    {
        if (string.IsNullOrWhiteSpace(folderPath))
            throw new ArgumentException("Folder path cannot be null or empty.", nameof(folderPath));

        if (string.IsNullOrWhiteSpace(searchString))
            throw new ArgumentException("Search string cannot be null or empty.", nameof(searchString));

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"The folder '{folderPath}' does not exist.");
            return;
        }

        string[] files = Directory.GetFiles(folderPath);

        foreach (var file in files)
        {
            try
            {
                using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true))
                using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    char[] buffer = new char[8192];
                    int bytesRead;
                    bool isPresent = false;

                    while ((bytesRead = await reader.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        string content = new string(buffer, 0, bytesRead);
                        if (content.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                        {
                            isPresent = true;
                            break;
                        }
                    }

                    Console.WriteLine(isPresent
                        ? $"Present: {Path.GetFileName(file)}"
                        : $"Absent: {Path.GetFileName(file)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file '{file}': {ex.Message}");
            }
        }
    }

  
}


public static class DuplicateIdentifier<T> where T : IEquatable<T>
{
    // Function to identify duplicates between two collections
    public static Dictionary<T, bool> IdentifyDuplicates(List<T> collectionA, List<T> collectionS)
    {
        // Create a HashSet for collectionA for faster lookups
        HashSet<T> setA = new HashSet<T>(collectionA);

        // Create a dictionary to store the results
        Dictionary<T, bool> results = new Dictionary<T, bool>();

        // Iterate through collectionS and check presence in setA
        foreach (var item in collectionS)
        {
            results[item] = setA.Contains(item);
        }

        return results;
    }
}