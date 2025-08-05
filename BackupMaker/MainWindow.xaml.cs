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
using System.Windows.Shapes;
using BackupMaker.Windows;

namespace BackupMaker;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    
    private readonly Logic.Logic logic = new Logic.Logic();
    
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OpenSettingsWindows(object sender, EventArgs e)
    {
        var window = new SettingsWindow();
        window.Show();
    }
    private void HandleChange(object sender, EventArgs e)
    {
        var drives = (AvailableDrives)Resources["DrivesList"];

        var selectedOrigin = Origin.SelectedItem;
        var selectedDestiny = Destiny.SelectedItem;
        
        drives.RefreshDrives();

        if (selectedOrigin != null && drives.Contains((char)selectedOrigin))
        {
            Origin.SelectedItem = selectedOrigin;
        }
        
        if (selectedDestiny != null && drives.Contains((char)selectedDestiny))
        {
            Destiny.SelectedItem = selectedDestiny;
        }

        if (Destiny.SelectedItem != null && (Origin.SelectedItem == Destiny.SelectedItem))
        {
            SameDiskErr.Visibility = Visibility.Visible;
        }
    }

    private void VerifySelection(object sender, EventArgs e)
    {
        if (Origin.SelectedItem != null && Destiny.SelectedItem != null &&
            Origin.SelectedItem.Equals(Destiny.SelectedItem))
        {
            NoSoErr.Visibility = Visibility.Collapsed;
            SameDiskErr.Visibility = Visibility.Visible;
            StartButton.IsEnabled = false;
        }
        else if (Origin.SelectedItem != null && !Directory.Exists($"{Origin.SelectedItem}:\\Users"))
        {
            SameDiskErr.Visibility = Visibility.Collapsed;
            NoSoErr.Visibility = Visibility.Visible;
            StartButton.IsEnabled = false;
        }
        else if (Origin.SelectedItem == null || Destiny.SelectedItem == null)
        {
            StartButton.IsEnabled = false;
        }
        else
        {
            SameDiskErr.Visibility = Visibility.Collapsed;
            NoSoErr.Visibility = Visibility.Collapsed;
            StartButton.IsEnabled = true;
        }
    }

    private async void StartBackup(object sender, EventArgs e)
    {
        Success.Visibility = Visibility.Collapsed;
        StartButton.IsEnabled = false;
        ProgressIndicator.Visibility = Visibility.Visible;
        Origin.IsEnabled =  false;
        Destiny.IsEnabled = false;
        var selectedOrigin = $"{Origin.SelectedItem}";
        var selectedDestiny = $"{Destiny.SelectedItem}";
        
        await Task.Run((() => logic.startBackup(selectedOrigin, selectedDestiny, (report) =>
        {
            Dispatcher.Invoke(() =>
            {
                Log.Text += $"{report}\n";
                LogScroller.ScrollToBottom();
            });
        })));
        
        StartButton.IsEnabled = true;
        ProgressIndicator.Visibility = Visibility.Collapsed;
        Origin.IsEnabled = true;
        Destiny.IsEnabled = true;
        Success.Visibility = Visibility.Visible;
    }

}

public class AvailableDrives : ObservableCollection<char>
{
    public void RefreshDrives()
    {
        this.Clear();
        foreach (var drive in DriveInfo.GetDrives())
        {
            if (drive.IsReady == true)
            {
                this.Add(drive.Name[0]);
            }
        }
    } 
}