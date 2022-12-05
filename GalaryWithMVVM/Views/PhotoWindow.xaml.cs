using GalaryWithMVVM.ViewModels;
using System.Windows;

namespace GalaryWithMVVM.Views;

public partial class PhotoWindow : Window
{
    public PhotoWindow()
    {
        InitializeComponent();
        var vm = new PhotoWindowViewModel();
        this.DataContext = vm;
    }
}
