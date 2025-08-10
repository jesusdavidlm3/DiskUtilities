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
using Microsoft.WindowsAPICodePack.Shell;

namespace AntiDuplicate;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
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
            FolderToScan.Text = $"Selected folder: {Path.GetFileName(folder)}";
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
}