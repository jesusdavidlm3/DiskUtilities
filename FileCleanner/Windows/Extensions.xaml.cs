using System.Windows;
using System.Windows.Controls;
using CommonClasses;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace FileCleanner.Windows;

public partial class Extensions : Window
{
    public Extensions()
    {
        InitializeComponent();
    }

    private void AllVideoChecked(object sender, RoutedEventArgs e)
    {
        var video = (VideoExtensions)this.Resources["VideoExtensions"];
        var currentStatus = sender as CheckBox;
        CommonLogic.CheckExtensionGroup(video, currentStatus.IsChecked);
    }

    private void AllAudioChecked(object sender, RoutedEventArgs e)
    {
        var audio = (AudioExtensions)this.Resources["AudioExtensions"];
        var currentStatus = sender as CheckBox;
        CommonLogic.CheckExtensionGroup(audio, currentStatus.IsChecked);
    }

    private void AllImageChecked(object sender, RoutedEventArgs e)
    {
        var image = (ImageExtensions)this.Resources["ImageExtensions"];
        var currentStatus = sender as CheckBox;
        CommonLogic.CheckExtensionGroup(image, currentStatus.IsChecked);
    }

    private void AllDocsChecked(object sender, RoutedEventArgs e)
    {
        var docs = (DocExtensions)this.Resources["DocExtensions"];
        var currentStatus = sender as CheckBox;
        CommonLogic.CheckExtensionGroup(docs, currentStatus.IsChecked);
    }

    private void AllCompressedChecked(object sender, RoutedEventArgs e)
    {
        var compressed = (CompressedExtensions)this.Resources["CompressedExtensions"];
        var currentStatus = sender as CheckBox;
        CommonLogic.CheckExtensionGroup(compressed, currentStatus.IsChecked);
    }

    private void AllSystemChecked(object sender, RoutedEventArgs e)
    {
        var system = (SystemExtensions)this.Resources["SystemExtensions"];
        var currentStatus = sender as CheckBox;
        CommonLogic.CheckExtensionGroup(system, currentStatus.IsChecked);
    }

    private void AllProjectsChecked(object sender, RoutedEventArgs e)
    {
        var projects = (ProjectExtensions)this.Resources["ProjectExtensions"];
        var currentStatus = sender as CheckBox;
        CommonLogic.CheckExtensionGroup(projects, currentStatus.IsChecked);
    }
}