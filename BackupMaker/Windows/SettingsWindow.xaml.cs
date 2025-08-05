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
        CommonLogic.CheckExtensionGroup(videoExtensions, currentStatus.IsChecked);
    }

    public void AllAudioCheck(object sender, EventArgs e)
    {
        var audioExtensions = (AudioExtensions)this.Resources["AudioExtensions"];
        var currentStatus = sender as  CheckBox;
        CommonLogic.CheckExtensionGroup(audioExtensions, currentStatus.IsChecked);
    }

    public void AllImagesCheck(object sender, EventArgs e)
    {
        var imageExtensions = (ImageExtensions)this.Resources["ImageExtensions"];
        var currentStatus = sender as  CheckBox;
        CommonLogic.CheckExtensionGroup(imageExtensions, currentStatus.IsChecked);
    }

    public void AllDocsCheck(object sender, EventArgs e)
    {
        var docExtensions = (DocExtensions)this.Resources["DocExtensions"];
        var currentStatus = sender as  CheckBox;
        CommonLogic.CheckExtensionGroup(docExtensions, currentStatus.IsChecked);
    }

    public void AllCompressedCheck(object sender, EventArgs e)
    {
        var compressedExtensions = (CompressedExtensions)this.Resources["CompressedExtensions"];
        var currentStatus = sender as  CheckBox;
        CommonLogic.CheckExtensionGroup(compressedExtensions, currentStatus.IsChecked);
    }

    public void AllSystemCheck(object sender, EventArgs e)
    {
        var systemExtensions = (SystemExtensions)this.Resources["SystemExtensions"];
        var currentStatus = sender as  CheckBox;
        CommonLogic.CheckExtensionGroup(systemExtensions, currentStatus.IsChecked);
    }

    public void AllProjectsCheck(object sender, EventArgs e)
    {
        var projectExtensions = (ProjectExtensions)this.Resources["ProjectExtensions"];
        var currentStatus = sender as  CheckBox;
        CommonLogic.CheckExtensionGroup(projectExtensions, currentStatus.IsChecked);
    }
}