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
    private DuplicatesFound duplicatesFound { get; set; } = new DuplicatesFound();
    private HashSet<int> matchedEntries = new HashSet<int>();
    
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
        IndexedFiles.Clear();
        duplicatesFound.Clear();
        ProcessPhase.Text = "Indexing files, Please wait...";

        await Task.Run(() =>
        {
            CommonLogic.WalkFolder(FolderToScan, null, file =>
            {
                var info = new FileInfo(file);
                IndexedFiles.Add(info);
                Console.WriteLine(info.Name);
            });
        });

        
        await Task.Run(() =>
        {
            Dispatcher.Invoke(() => ProcessPhase.Text = "Comparing files, Please wait...");
            for (int i = 0; i < IndexedFiles.Count; i++)
            {
                if (!matchedEntries.Contains(i))
                {
                    for (int j = IndexedFiles.Count; j-1 > i; j--)
                    {
                        if (matchedEntries.Contains(j)) continue;
                        if (IndexedFiles[i].Name == IndexedFiles[j-1].Name)
                        {
                            Console.WriteLine($"{IndexedFiles[i].FullName} igual a {IndexedFiles[j-1].FullName}");
                            var duplicate = duplicatesFound.FirstOrDefault(c => c.FileName == IndexedFiles[j-1].Name);
                            if(duplicate != null)
                            {
                                var file = new FileEntry(IndexedFiles[i]);
                                duplicate.Add(file);
                            }
                            else
                            {
                                var newDuplicate = new Coincidence(IndexedFiles[i].Name);
                                var file1 = new FileEntry(IndexedFiles[i]);
                                var file2 = new FileEntry(IndexedFiles[j-1]);
                                newDuplicate.Add(file1);
                                newDuplicate.Add(file2);
                                duplicatesFound.Add(newDuplicate);
                                matchedEntries.Add(j);
                            }

                            matchedEntries.Add(i);
                        }
                    }   
                }
            }
        });
        
        
        ProgressIndicator.Visibility = Visibility.Collapsed;
        StartButton.IsEnabled = true;
        SelectFolderButton.IsEnabled = true;

        var resultsWindow = new Results(duplicatesFound);
        resultsWindow.ShowDialog();
    }
}