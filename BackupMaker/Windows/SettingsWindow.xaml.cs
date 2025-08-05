using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Schema;
using CommonClasses;
namespace BackupMaker.Windows;

public partial class SettingsWindow : Window
{
    public SettingsWindow()
    {
        InitializeComponent();
    }

    public void AllVideoCheck(object sender, EventArgs e)
    {
        var videoExtensions = (VideoExtensions)this.Resources["VideoExtensions"];
        var currentStatus = sender as  CheckBox;
        AllCheck(videoExtensions, currentStatus.IsChecked);
    }

    public void AllAudioCheck(object sender, EventArgs e)
    {
        var audioExtensions = (AudioExtensions)this.Resources["AudioExtensions"];
        var currentStatus = sender as  CheckBox;
        AllCheck(audioExtensions, currentStatus.IsChecked);
    }

    public void AllImagesCheck(object sender, EventArgs e)
    {
        var imageExtensions = (ImageExtensions)this.Resources["ImageExtensions"];
        var currentStatus = sender as  CheckBox;
        AllCheck(imageExtensions, currentStatus.IsChecked);
    }

    public void AllDocsCheck(object sender, EventArgs e)
    {
        var docExtensions = (DocExtensions)this.Resources["DocExtensions"];
        var currentStatus = sender as  CheckBox;
        AllCheck(docExtensions, currentStatus.IsChecked);
    }

    public void AllCompressedCheck(object sender, EventArgs e)
    {
        var compressedExtensions = (CompressedExtensions)this.Resources["CompressedExtensions"];
        var currentStatus = sender as  CheckBox;
        AllCheck(compressedExtensions, currentStatus.IsChecked);
    }

    public void AllSystemCheck(object sender, EventArgs e)
    {
        var systemExtensions = (SystemExtensions)this.Resources["SystemExtensions"];
        var currentStatus = sender as  CheckBox;
        AllCheck(systemExtensions, currentStatus.IsChecked);
    }

    public void AllProjectsCheck(object sender, EventArgs e)
    {
        var projectExtensions = (ProjectExtensions)this.Resources["ProjectExtensions"];
        var currentStatus = sender as  CheckBox;
        AllCheck(projectExtensions, currentStatus.IsChecked);
    }

    private void AllCheck(ObservableCollection<Extension> extensionsList, bool? checkStatus)
    {
        if (checkStatus == true)
        {
            foreach (var item in extensionsList)
            {
                item.excluded = true;
            }
        }
        else
        {
            foreach (var item in extensionsList)
            {
                item.excluded = false;
            }
        }
    }
}