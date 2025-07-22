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

namespace BackupMakerWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void Refresh(object sender, EventArgs e)
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