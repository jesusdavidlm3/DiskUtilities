using System.Collections.ObjectModel;
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
    private DuplicatesCollection duplicatesCollection = new();
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
        DeleteMode.IsEnabled = false;
        duplicatesCollection.Clear();
        ProcessPhase.Text = "Indexing files, Please wait...";
        
        var operation = DeleteMode.SelectedItem as SelectionOption;

        // Index the files
        await Task.Run(() =>
        {
            CommonLogic.WalkFolder(FolderToScan, null, file =>
            {
                var info = new FileInfo(file);
                IndexedFiles.Add(info);
            });
        });

        
        await Task.Run(() =>
        {
            Dispatcher.Invoke(() =>
            {
                ProcessPhase.Text = "Comparing files, Please wait...";
                ProgressBar.IsIndeterminate = false;
                ProgressBar.Maximum = IndexedFiles.Count;
                ProgressBar.Value = 0;
                To.Text = (IndexedFiles.Count).ToString();
            });

            bool iLimit = true; //true to run de "for" at least ane time
            
            for (int i = 0; iLimit; i++)
            {
                if (operation != null && operation.Id == 0)     //evaluate the "for" condition here
                {
                    iLimit = i < IndexedFiles.Count;            //If user select erase walk all.
                }else if (operation == null || operation.Id == 1)
                {
                    iLimit = matchedEntries.Count < 100;        //If user will review limit to 100 files
                }
                
                if (matchedEntries.Contains(i)) continue;
                for (int j = IndexedFiles.Count; j-1 > i; j--)
                {
                    if (matchedEntries.Contains(j-1)) continue;
                    if (IndexedFiles[i].Name != IndexedFiles[j - 1].Name) continue;
                    var duplicate = duplicatesCollection.FirstOrDefault(c => c.FileName == IndexedFiles[j-1].Name);
                    if(duplicate != null)
                    {
                        var file = new FileEntry(IndexedFiles[j-1]);
                        duplicate.Add(file);
                    }
                    else
                    {
                        var newDuplicate = new Coincidence(IndexedFiles[i].Name);
                        var file1 = new FileEntry(IndexedFiles[i]);
                        var file2 = new FileEntry(IndexedFiles[j-1]);
                        newDuplicate.Add(file1);
                        newDuplicate.Add(file2);
                        duplicatesCollection.Add(newDuplicate);
                        matchedEntries.Add(j-1);
                    }
                    matchedEntries.Add(i);
                }
                Dispatcher.Invoke(() =>
                {
                    From.Text = (i).ToString();
                    ProgressBar.Value = i;
                });
            }
            IndexedFiles.Clear();
            matchedEntries.Clear();
        });
        
        ProgressIndicator.Visibility = Visibility.Collapsed;
        StartButton.IsEnabled = true;
        SelectFolderButton.IsEnabled = true;
        DeleteMode.IsEnabled = true;

        if (operation == null ||  operation.Id == 1)
        {
            var resultsWindow = new Results(duplicatesCollection);
            resultsWindow.ShowDialog();   
        }
    }
}

public class DeleteModeOptions : ObservableCollection<Classes.SelectionOption>
{
    public DeleteModeOptions()
    {
        Add(new SelectionOption("Keep newer", 0));
        Add(new SelectionOption("Review files", 1));
    }
}