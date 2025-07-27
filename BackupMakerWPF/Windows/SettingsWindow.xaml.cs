using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Schema;

namespace BackupMakerWPF.Windows;

public partial class SettingsWindow : Window
{
    public SettingsWindow()
    {
        InitializeComponent();
    }
}

public class Extension
{
    public string extension { get; set; }
    public bool excluded { get; set; }

    public Extension(string extension, bool excluded)
    {
        this.extension = extension;
        this.excluded = excluded;
    }
}

public class VideoExtensions : ObservableCollection<Extension>
{
    public VideoExtensions()
    {
        var extensions = new List<Extension>()
        {
            new (".mp4", false),
            new (".mkv", false),
            new (".avi", false),
            new (".wmv", false),
            new (".mov", false),
            new (".webm", false),
        };

        foreach (var extension in extensions)
        {
            Add(extension);
        }
    }
}

class AudioExtensions : ObservableCollection<Extension>
{
    public AudioExtensions()
    {
        var extensions = new List<Extension>()
        {
            new (".mp3", false),
            new (".m4a", false),
            new (".wav", false),
            new (".aac", false),
            new (".flac", false),
        };
        
        foreach (var extension in extensions)
        {
            Add(extension);
        }
    }
}

public class ImageExtensions : ObservableCollection<Extension>
{
    public ImageExtensions()
    {
        var extensions = new List<Extension>
        {
            new(".bmp", false),
            new(".gif", false),
            new(".jpg", false),
            new(".jpeg", false),
            new(".png", false),
            new(".tiff", false),
            new(".webp", false),
        };
        foreach (var extension in extensions)
        {
            Add(extension);
        }
    }
}

class DocExtensions : ObservableCollection<Extension>
{
    public DocExtensions()
    {
        var extensions = new List<Extension>()
        {
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
        
        foreach (var extension in extensions)
        {
            Add(extension);
        }
    }
}

class CompressedExtensions : ObservableCollection<Extension>
{
    public CompressedExtensions()
    {
        var extensions = new List<Extension>()
        {
            new (".iso", false),
            new (".zip", false),
            new (".rar", false),
            new (".7z", false),
            new (".tar.gz", false)
        };
        
        foreach (var extension in extensions)
        {
            Add(extension);
        }
    }
}

class SystemExtensions : ObservableCollection<Extension>
{
    public SystemExtensions()
    {
        var extensions = new List<Extension>()
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
        foreach (var extension in extensions)
        {
            Add(extension);
        }
    }
}

public class ProjectExtensions : ObservableCollection<Extension>
{
    public ProjectExtensions()
    {
        var extensions = new List<Extension>
        {
            new(".psd", false),
            new (".ai", false),
            new (".indd", false),
            new (".poproj", false),
            new (".chproj", false),
            new (".blend", false),
            new (".drp", false),
            new (".dra", false),
            new (".settings", false),
            new (".obj", false),
            new (".fbx", false),
            new (".stl", false),
            new (".ply", false),
            new (".dae", false),
        };
        foreach (var extension in extensions)
        {
            Add(extension);
        }
    }
}