using GalaryWithMVVM.ViewModels;
using System.Windows;

namespace GalaryWithMVVM;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var viewModel = new MainViewModel(wrapPanel);
        this.DataContext = viewModel;
    }

}
