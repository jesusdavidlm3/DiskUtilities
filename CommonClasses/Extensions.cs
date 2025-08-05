using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace CommonClasses;

public class Extension : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public string extension { get; set; }
    private bool _excluded;
    public bool excluded
    {
        get => _excluded;
        set
        {
            if (_excluded != value)
            {
                _excluded = value;
                OnPropertyChanged(nameof(excluded));
            }
        }
    }
    
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public Extension(string extension, bool excluded)
    {
        this.extension = extension;
        this.excluded = excluded;
    }
}

public class VideoExtensions : ObservableCollection<Extension>
{
    public static bool AllChecked { get; set; } = false;
    public static List<Extension> extensions { get; set; } = new List<Extension>
    {
        new (".mp4", false),
        new (".mkv", false),
        new (".avi", false),
        new (".wmv", false),
        new (".mov", false),
        new (".webm", false),
    };
    
    public VideoExtensions()
    {
        foreach (var extension in extensions)
        {
            Add(extension);
        }
    }
}

public class AudioExtensions : ObservableCollection<Extension>
{
    public static bool AllChecked { get; set; } = false;
    public static List<Extension> extensions { get; set; } = new List<Extension>{
        new (".mp3", false),
        new (".m4a", false),
        new (".wav", false),
        new (".aac", false),
        new (".flac", false),
    };
    
    public AudioExtensions()
    {
        foreach (var extension in extensions)
        {
            Add(extension);
        }
    }
}

public class ImageExtensions : ObservableCollection<Extension>
{
    public static bool AllChecked { get; set; } = false;
    public static List<Extension> extensions { get; set; } = new List<Extension>
    {
        new(".bmp", false),
        new(".gif", false),
        new(".jpg", false),
        new(".jpeg", false),
        new(".png", false),
        new(".tiff", false),
        new(".webp", false),
    };
    public ImageExtensions()
    {
        foreach (var extension in extensions)
        {
            Add(extension);
        }
    }
}

public class DocExtensions : ObservableCollection<Extension>
{
    public static bool AllChecked { get; set; } = false;
    public static List<Extension> extensions { get; set; } = new List<Extension>()
    {
        new(".txt", false),
        new(".pdf", false),
        new(".doc", false),
        new(".docx", false),
        new(".xls", false),
        new(".xlsx", false),
        new(".ppt", false),
        new(".pptx", false),
        new(".pptm", false),
        new(".pptxm", false),
        new(".odt", false),
        new(".odtx", false),
        new(".odsx", false),
        new(".odsxm", false),
        new(".odtm", false),
        new(".odtmx", false),
        new(".odtxm", false),
    };
    public DocExtensions()
    {
        foreach (var extension in extensions)
        {
            Add(extension);
        }
    }
}

public class CompressedExtensions : ObservableCollection<Extension>
{
    public static bool AllChecked { get; set; } = false;
    public static List<Extension> extensions { get; set; } = new List<Extension>
    {
        new (".iso", false),
        new (".zip", false),
        new (".rar", false),
        new (".7z", false),
        new (".tar.gz", false)
    };
    
    public CompressedExtensions()
    {
        foreach (var extension in extensions)
        {
            Add(extension);
        }
    }
}

public class SystemExtensions : ObservableCollection<Extension>
{
    public static bool AllChecked { get; set; } = false;
    public static List<Extension> extensions { get; set; } = new List<Extension>
    {
        new(".exe", false),
        new(".ini", true),
        new(".ink", true),
        new(".tmp", true),
        new(".lnk", true),
        new(".log", true),
        new(".bak", true),
        new(".old", true),
        new(".dll", true),
        new(".win", true),
        new(".img", true),
        new(".asp", true),
        new(".msp", true),
        new(".cfg", true),
        new(".cat", true),
        new(".bin", true),
        new(".efi", true),
        new(".p7b", true),
        new(".inf", true),
        new(".msi", true),
        new(".mui", true),
        new(".dat", true),
        new(".man", true)
    };
    
    public SystemExtensions()
    {
        foreach (var extension in extensions)
        {
            Add(extension);
        }
    }
}

public class ProjectExtensions : ObservableCollection<Extension>
{
    public static bool AllChecked { get; set; } = false;
    public static List<Extension> extensions { get; set; } = new List<Extension>
    {
        new(".psd", false),
        new(".ai", false),
        new(".indd", false),
        new(".poproj", false),
        new(".chproj", false),
        new(".blend", false),
        new(".drp", false),
        new(".dra", false),
        new(".settings", false),
        new(".obj", false),
        new(".fbx", false),
        new(".stl", false),
        new(".ply", false),
        new(".dae", false),
    };
    
    public ProjectExtensions()
    {
        foreach (var extension in extensions)
        {
            Add(extension);
        }
    }
}