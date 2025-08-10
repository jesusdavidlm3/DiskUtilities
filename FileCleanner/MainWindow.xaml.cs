using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using Path = System.IO.Path;
using CommonClasses;
using FileCleanner.Windows;

namespace FileCleanner;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private string FolderToScan;
    
    public void SelectFolder(object sender, EventArgs e)
    {
        var dialog = new CommonOpenFileDialog{IsFolderPicker = true, Title = "Select folder to clean"};
        if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
        {
            string folder = dialog.FileName;
            UiSelectedFolder.Text = $"Folder selected: {Path.GetFileName(dialog.FileName)}";
            FolderToScan = dialog.FileName;
            StartButton.IsEnabled = true;
            if (Directory.Exists($"{folder}\\Windows"))
            {
                Warning.Visibility = Visibility.Visible;
            }else if (Path.GetFileName(folder) == "Program Files (x86)")
            {
                Warning.Visibility = Visibility.Visible;
            }else if (Path.GetFileName(folder) == "Program Files")
            {
                Warning.Visibility = Visibility.Visible;
            }else if (Path.GetFileName(folder) == "Windows")
            {
                Warning.Visibility = Visibility.Visible;
            }
            else
            {
                Warning.Visibility = Visibility.Collapsed;
            }
        }
    }

    public void OpenExtensionsWindows(object sender, EventArgs e)
    {
        var extensionsWindows = new Extensions();
        extensionsWindows.ShowDialog();
    }
    
    public async void StartCleanning(object sender, EventArgs e)
    {
        Success.Visibility = Visibility.Collapsed;
        StartButton.IsEnabled = false;
        SelectFolderButton.IsEnabled = false;
        ProgressIndicator.Visibility = Visibility.Visible;
        
        CommonLogic.GroupCollections(Logic.Logic.excludedFiles);
        await Task.Run(() =>
        {
            CommonLogic.WalkFolder(FolderToScan, null, (file) => Logic.Logic.DeleteFile(file));            
        });

        
        Success.Visibility = Visibility.Visible;
        StartButton.IsEnabled = true;
        SelectFolderButton.IsEnabled = true;
        ProgressIndicator.Visibility = Visibility.Collapsed;
    }
}

public static class DeletionLogic
{
    public static void DeleteFiles(string folder)
    {
        
    }
}