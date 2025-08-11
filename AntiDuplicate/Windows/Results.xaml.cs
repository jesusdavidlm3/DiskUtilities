using System.Windows;
using AntiDuplicate;
using AntiDuplicate.Classes;

namespace AntiDuplicate.Windows;

public partial class Results : Window
{
    public Results(DuplicatesFound duplicatesFound)
    {
        InitializeComponent();
        DataContext = duplicatesFound;
    }
}