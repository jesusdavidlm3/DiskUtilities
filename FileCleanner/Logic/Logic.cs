using System.IO;

namespace FileCleanner.Logic;

public static class Logic
{
    public static List<string> excludedFiles { get; set;}

    static Logic()
    {
        excludedFiles = new List<string>();
    }

    public static void DeleteFile(string file, Action<string> onReport = null)
    {
        string fileExtension = Path.GetExtension(file);
        if (excludedFiles.Contains(fileExtension))
        {
            onReport?.Invoke($"Deleting {file}");
            try
            {
                File.Delete(file);
                onReport?.Invoke($"Deleted {file}");
            }
            catch (Exception e)
            {
                onReport?.Invoke($"Error deleting {file}");
            }
        }
    }
}