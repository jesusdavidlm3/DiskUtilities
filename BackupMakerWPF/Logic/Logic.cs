using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows.Documents;
using BackupMakerWPF.Windows;

namespace BackupMakerWPF.Logic;

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
            // "Documents",
            "Desktop",
            "Downloads",
            "Music",
            "Videos",
            "Pictures"
        ];



    // var videoExtensions = new VideoExtensions();
    // var audioExtensions = new AudioExtensions();
    // var imageExtensions = new ImageExtensions();
    // var docExtensions =  new DocExtensions();
    // var compressedExtensions = new CompressedExtensions();
    // var systemExtensions = new SystemExtensions();
    // var projectExtensions = new ProjectExtensions();

        
    }

    public void startBackup(string origin, string destiny, Action<string> onReport = null)
    {
        var groupCollection = new List<Type>
        {
            typeof(VideoExtensions),
            typeof(AudioExtensions),
            typeof(ImageExtensions),
            typeof(DocExtensions),
            typeof(CompressedExtensions),
            typeof(SystemExtensions),
            typeof(ProjectExtensions)
        };
        foreach (var group in groupCollection)
        {
            var prop = group.GetProperty("extensions", BindingFlags.Public | BindingFlags.Static);
            var extensions = prop?.GetValue(null) as IEnumerable<Extension>;
            if (extensions != null)
                foreach (var item in extensions)
                {
                    if (item.excluded == true)
                    {
                        excludedFiles.Add(item.extension);
                    }
                }
        }
        string destinyDirectory = $"{destiny}:\\Nuevo respaldo";  //Carpeta que guaradara el respaldo
        Directory.CreateDirectory(destinyDirectory);     //Creamos el directorio destino del respaldo

        List<string> users = GetUsersFolders($"{origin}:");
        foreach(string user in users)
        {
            string userFolderName = Path.GetFileName(user);
            string destinyUserFolder = $"{destiny}:\\Nuevo respaldo\\{userFolderName}";
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
            onReport?.Invoke("Se enconraron los siguientes errores:");
            foreach(string err in this.errorList)
            {
                onReport?.Invoke(err);
            }
            onReport?.Invoke("Si desconoce la ubicacion de estas carpetas ignorelas.");
            onReport?.Invoke("Es posible que sean enlaces simbolicos a otras bibliotecas.");
        }
    }

    private void CopyFiles(string oDirectory, string dDirectory, Action<string> onReport = null)
    {
        foreach (string file in Directory.GetFiles(oDirectory))
        {
            string fileName = Path.GetFileName(file);
            string extension = Path.GetExtension(file);
            if (!excludedFiles.Contains(extension))
            {
                // Console.WriteLine($"Se esta copiando {file}");
                onReport?.Invoke($"Se esta copiando {file}");
                File.Copy(file, $"{dDirectory}\\{fileName}", true);
                // Console.WriteLine($"Se copio: {file}");
                onReport?.Invoke($"Se copio: {file}");
            }
            else
            {
                // Console.WriteLine($"No se copio: {file}");
                onReport?.Invoke($"No se copio: {file}");
            }
        }
    }

    private void CopyFolders(string oDirectory, string dDirectory, Action<string> onReport = null)
    {
        string folderName = Path.GetFileName(oDirectory);
        try
        {
            Directory.CreateDirectory($"{dDirectory}\\{folderName}");
            // Console.WriteLine($"Se encontro la carpeta: {oDirectory}");
            onReport?.Invoke($"Se encontro la carpeta: {oDirectory}");
            CopyFiles(oDirectory, $"{dDirectory}\\{folderName}", onReport);
            string[] folders = Directory.GetDirectories(oDirectory);
            if (folders.Length > 0)
            {
                foreach (string folder in folders)
                {
                    CopyFolders(folder, $"{dDirectory}\\{folderName}", onReport);
                }
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
            if (!Directory.Exists($"{destiny}:\\Nuevo respaldo\\Instalaciones Anteriores"))
            {
                Directory.CreateDirectory($"{destiny}:\\Nuevo respaldo\\Instalaciones Anteriores");
            }

            if (Directory.Exists($"{oldRoot}\\windows.old"))
            {
                CheckOlds($"{oldRoot}\\windows.old", destiny, onReport);
            }
        
            List<string> users = GetUsersFolders($"{oldRoot}");

            foreach (string user in users)
            {
                string folderName = Path.GetFileName(user);
                string destinyFolder = $"{destiny}:\\Nuevo respaldo\\Instalaciones Anteriores\\{folderName}";
                Directory.CreateDirectory(destinyFolder);
                foreach (string directory in includedDirectories)
                {
                    CopyFolders($"{oldRoot}\\Users\\{folderName}\\{directory}", destinyFolder, onReport);
                }
            }
        }
    }
}