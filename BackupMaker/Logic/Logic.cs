using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows.Documents;
using BackupMaker.Windows;
using CommonClasses;

namespace BackupMaker.Logic;

public class Logic
{
    private List<string> includedDirectories { get; set; } = new List<string>();
    private List<string> excludedFiles { get; set; } = new List<string>();
    private List<string> excludedDirectories { get; set; } = new List<string>();
    private List<string> errorList { get; set; } = new List<string>();

    public Logic()
    {
        this.includedDirectories =
        [
            "Documents",
            "Desktop",
            "Downloads",
            "Music",
            "Videos",
            "Pictures"
        ];
    }

    public void startBackup(string origin, string destiny, Action<string> onReport = null)
    {
        CommonLogic.GroupCollections(excludedFiles);
        string destinyDirectory = $"{destiny}:\\New Backup";  //Carpeta que guaradara el respaldo
        Directory.CreateDirectory(destinyDirectory);     //Creamos el directorio destino del respaldo

        List<string> users = GetUsersFolders($"{origin}:");
        foreach(string user in users)
        {
            string userFolderName = Path.GetFileName(user);
            string destinyUserFolder = $"{destiny}:\\New Backup\\{userFolderName}";
            Directory.CreateDirectory(destinyUserFolder);
            foreach(string directory in includedDirectories)
            {
                CopyFolders($"{user}\\{directory}", destinyUserFolder, onReport);
            }
        }
        
        string oldRoot = $"{origin}:\\windows.old";
        CheckOlds(oldRoot, destiny, onReport);

        if (this.errorList.Count > 0)
        {
            onReport?.Invoke("Errors found:");
            foreach(string err in this.errorList)
            {
                onReport?.Invoke(err);
            }
            onReport?.Invoke("If you don't know this folders ignore them.");
            onReport?.Invoke("It may be simbolic links to another libraries.");
        }
    }

    private void CopyFiles(string oDirectory, string dDirectory, Action<string> onReport = null)
    {
        var threadsConfig = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount * 2 };
        Parallel.ForEach(Directory.GetFiles(oDirectory), threadsConfig, file =>
        {
            string fileName = Path.GetFileName(file);
            string extension = Path.GetExtension(file);
            if (!excludedFiles.Contains(extension))
            {
                // onReport?.Invoke($"Copying {file}");
                File.Copy(file, $"{dDirectory}\\{fileName}", true);
                onReport?.Invoke($"Copied: {file}");
            }
            else
            {
                // onReport?.Invoke($"Error copying: {file}");
            }
        });
    }

    private void CopyFolders(string oDirectory, string dDirectory, Action<string> onReport = null)
    {
        var threadsConfig = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };
        string folderName = Path.GetFileName(oDirectory);
        try
        {
            Directory.CreateDirectory($"{dDirectory}\\{folderName}");
            onReport?.Invoke($"Found folder: {oDirectory}");
            CopyFiles(oDirectory, $"{dDirectory}\\{folderName}", onReport);
            string[] folders = Directory.GetDirectories(oDirectory);
            if (folders.Length > 0)
            {
                Parallel.ForEach(folders, threadsConfig, folder =>
                {
                    CopyFolders(folder, $"{dDirectory}\\{folderName}", onReport);
                });
            }
        }
        catch (Exception ex)
        {
            // Console.WriteLine("Error al copiar un archivo");
            errorList.Add(ex.Message);
        }
    }

    private List<string> GetUsersFolders(string oDirectory)
    {
        this.excludedDirectories =
        [
            $"{oDirectory}\\Users\\All Users",
            $"{oDirectory}\\Users\\Default",
            $"{oDirectory}\\Users\\Default User"
        ];
        List<string> users = new List<string>(Directory.GetDirectories($"{oDirectory}\\Users"));   //Obtenemos las capetas de usuario
        users = users.Where(i => !excludedDirectories.Contains(i)).ToList();
        return users;
    }

    private void CheckOlds(string oldRoot, string destiny, Action<string> onReport = null)
    {
        if(Directory.Exists(oldRoot))
        {
            if (!Directory.Exists($"{destiny}:\\New Backup\\Old instalations"))
            {
                Directory.CreateDirectory($"{destiny}:\\New Backup\\Old instalations");
            }

            if (Directory.Exists($"{oldRoot}\\windows.old"))
            {
                CheckOlds($"{oldRoot}\\windows.old", destiny, onReport);
            }
        
            List<string> users = GetUsersFolders($"{oldRoot}");

            foreach (string user in users)
            {
                string folderName = Path.GetFileName(user);
                string destinyFolder = $"{destiny}:\\New Backup\\Old instalations\\{folderName}";
                Directory.CreateDirectory(destinyFolder);
                foreach (string directory in includedDirectories)
                {
                    CopyFolders($"{oldRoot}\\Users\\{folderName}\\{directory}", destinyFolder, onReport);
                }
            }
        }
    }
}