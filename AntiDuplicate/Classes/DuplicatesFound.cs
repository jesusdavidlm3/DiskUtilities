using System.Collections.ObjectModel;
using System.IO;

namespace AntiDuplicate.Classes;

public class FileEntry
{
    public FileInfo Info { get; set; }
    public bool IsChecked { get; set; }

    public FileEntry(FileInfo info, bool? isChecked = false)
    {
        Info = info;
        IsChecked = (bool)isChecked;
    }
}

public class Coincidence : List<FileEntry>
{
    public string FileName { get; set; }

    public Coincidence(string filename)
    {
        FileName = filename;
    }
}

public class DuplicatesFound : ObservableCollection<Coincidence>
{
    public DuplicatesFound() { }
}