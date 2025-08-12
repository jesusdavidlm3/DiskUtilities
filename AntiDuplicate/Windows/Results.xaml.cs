using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using AntiDuplicate;
using AntiDuplicate.Classes;

namespace AntiDuplicate.Windows;

public partial class Results : Window
{
    
    public ObservableCollection<Coincidence> duplicatesCollection;
    
    public Results(ObservableCollection<Coincidence> duplicatesCollection)
    {
        InitializeComponent();
        this.duplicatesCollection = duplicatesCollection;
        DataContext = this.duplicatesCollection;
    }
}