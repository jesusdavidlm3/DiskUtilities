using System.Windows;
using AntiDuplicate;
using AntiDuplicate.Classes;

namespace AntiDuplicate.Windows;

public partial class Results : Window
{
    
    public DuplicatesCollection duplicatesCollection;
    
    public Results(DuplicatesCollection duplicatesCollection)
    {
        InitializeComponent();
        this.duplicatesCollection = duplicatesCollection;
        DataContext = this.duplicatesCollection;
    }
}