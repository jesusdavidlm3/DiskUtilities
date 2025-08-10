using System.Collections.ObjectModel;
using System.IO;

namespace AntiDuplicate.Classes;

public class Coincidence : List<FileInfo>
{
    public Coincidence() { }
}

public class DuplicatesFound : ObservableCollection<Coincidence>
{
    public DuplicatesFound() { }
}