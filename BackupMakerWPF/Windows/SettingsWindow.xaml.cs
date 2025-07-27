using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;

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

public class ExcludedExtensions : ObservableCollection<Extension>
{
    public ExcludedExtensions()
    {
        var extensions = new List<Extension>()
        {
            new (".exe", true),
            new (".mp3", false),
            new (".mp4", true),
            new (".ini", false),
            new (".ink", false),
            new (".tmp", false),
            new (".lnk", false),
            new (".log", false),
            new (".bak", false),
            new (".old", false),
            new (".dll", false),
            new (".win", false),
            new (".iso", false),
            new (".img", false),
            new (".asp", false),
            new (".msp", false),
            new (".cfg", false),
            new (".cat", false),
            new (".bin", false),
            new (".mkv", false),
            new (".efi", false),
            new (".p7b", false),
            new (".inf", false),
            new (".msi", false),
            new (".bmp", false),
            new (".mui", false),
            new (".dat", false),
            new (".man", false),
            new (".m4a", false),
        };

        foreach (var extension in extensions)
        {
            Add(extension);
        }
    }
}