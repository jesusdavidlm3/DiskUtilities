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
using Microsoft.WindowsAPICodePack.Dialogs;
using Path = System.IO.Path;
using AntiDuplicate;
using AntiDuplicate.Classes;
using AntiDuplicate.Windows;
using Microsoft.WindowsAPICodePack.Shell;
using CommonClasses;
using AntiDuplicate.Classes;

namespace AntiDuplicate;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private string FolderToScan;
    private List<FileInfo> IndexedFiles = new List<FileInfo>();
    private DuplicatesFound duplicatesFound = new DuplicatesFound();
    
    public MainWindow()
    {
        InitializeComponent();
    }

    private void SelectFolder(object sender, RoutedEventArgs e)
    {
        var dialog = new CommonOpenFileDialog{IsFolderPicker = true, Title = "Select folder to look"};
        if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
        {
            string folder = dialog.FileName;
            UiSelectedFolder.Text = $"Selected folder: {Path.GetFileName(folder)}";
            FolderToScan = folder;
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

    private async void StartComparison(object sender, EventArgs e)
    {
        ProgressIndicator.Visibility = Visibility.Visible;
        StartButton.IsEnabled = false;
        SelectFolderButton.IsEnabled = false;

        await Task.Run(() =>
        {
            CommonLogic.WalkFolder(FolderToScan, null, file =>
            {
                var info = new FileInfo(file);
                IndexedFiles.Add(info);
            });
            for (int i = 0; i < IndexedFiles.Count; i++)
            {
                for (int j = IndexedFiles.Count; j-1 > i; j--)
                {
                    if (IndexedFiles[i].Name == IndexedFiles[j-1].Name)
                    {
                        Console.WriteLine($@"{IndexedFiles[i].FullName} es igual a {IndexedFiles[j-1].FullName}");
                        var duplicate = new Coincidence { IndexedFiles[i], IndexedFiles[j-1] };
                        duplicatesFound.Add(duplicate);
                    }
                }
            }
        });
        
        
        ProgressIndicator.Visibility = Visibility.Collapsed;
        StartButton.IsEnabled = true;
        SelectFolderButton.IsEnabled = true;

        var resultsWindow = new Results();
        resultsWindow.ShowDialog();
    }
}