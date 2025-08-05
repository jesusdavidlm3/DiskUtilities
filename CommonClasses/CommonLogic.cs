using System.Reflection;

namespace CommonClasses;

public static class CommonLogic
{
    public static void WalkFolder(List<string> folderList)
    {
        foreach (string folder in folderList)
        {
            
        }
    }

    public static void WalkFiles(List<string> filesList)
    {
        foreach (string file in filesList)
        {
            
        }
    }

    public static void GroupCollections(List<Type> groups, List<string> excludedFiles)
    {
        foreach (var group in groups)
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
}