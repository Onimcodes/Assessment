using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Assessment.Interfaces;
using Assessment.Services;

public enum FileSourceType
{
    Local,
    AzureBlob,
    GoogleDrive,
    S3, // AWS S3 or other sources can be added here
    Http
}

 internal class Program
{
    public static async Task Main(string[] args)
    {
        //First Test
        
        string folderPath = "C:\\Users\\User\\Music";
        string searchString = "Alab";
        //Test Local system
        await FileScanner.ScanFilesAsync(FileSourceType.Local, searchString:searchString, folderPath);


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
   
    public static async Task ScanFilesAsync(FileSourceType sourceType, string searchString, string pathOrConnection = null)
    {
        IFileSource fileSource = FileSourceFactory.CreateFileSource(sourceType, pathOrConnection);

        try
        {
            var matchingFiles = await fileSource.GetFilesAsync(pathOrConnection, searchString);

            foreach (var file in matchingFiles)
            {
                Console.WriteLine($"{file.Value} --- {file.Key}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
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

