using GalaryWithMVVM.ViewModels;
using System.Windows;


namespace GalaryWithMVVM.Views;


public partial class Add_Image_Window : Window
{
    public Add_Image_Window()
    {

        InitializeComponent();
        var vm = new Add_Image_WindowViewModel();
        this.DataContext = vm;
    }

}