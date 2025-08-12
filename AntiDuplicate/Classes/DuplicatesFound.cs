using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace AntiDuplicate.Classes;

public class FileEntry : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public FileInfo Info { get; set; }
    private bool _IsChecked;

    public bool IsChecked
    {
        get => _IsChecked;
        set
        {
            if (_IsChecked != value)
            {
                _IsChecked = value;
                OnPropertyChanged(nameof(_IsChecked));
            }
        }
    }

    private void OnPropertyChanged(string  propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

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

public class DuplicatesCollection : ObservableCollection<Coincidence>
{
    public DuplicatesCollection(){ }
}