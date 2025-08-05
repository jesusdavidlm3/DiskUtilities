using System.Collections.ObjectModel;
using System.Reflection;

namespace CommonClasses;

public static class CommonLogic
{
    public static void WalkFolder(string currentFolder, Action? folderAction = null, Action<string>? fileAction = null)
    {
        if (fileAction != null)
        {
            WalkFiles(currentFolder, fileAction);
        }
        List<string> folderList = Directory.GetDirectories(currentFolder).ToList();
        if (folderAction != null)
        {
            var threadsConfig = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };
            Parallel.ForEach(folderList, threadsConfig, folder =>
            {
                folderAction.Invoke();
            });
        }
        foreach (string folder in folderList)
        {
            WalkFolder(folder, folderAction, fileAction);
        }
    }

    private static void WalkFiles(string directory, Action<string>? callBack = null)
    {
        if (callBack != null)
        {
            var threadsConfig = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount * 2 };
            var filesList = Directory.GetFiles(directory);
            Parallel.ForEach(filesList, threadsConfig, file =>
            {
                callBack?.Invoke(file);
            });        
        }
    }

    public static void GroupCollections(List<string> excludedFiles)
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
    }

    public static void CheckExtensionGroup(ObservableCollection<Extension> extensionsList, bool? checkStatus)
    {
        if (checkStatus == true)
        {
            foreach (var item in extensionsList)
            {
                item.excluded = true;
            }
        }
        else
        {
            foreach (var item in extensionsList)
            {
                item.excluded = false;
            }
        }
    }
}