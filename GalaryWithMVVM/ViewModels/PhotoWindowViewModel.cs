using GalaryWithMVVM.Commands;
using GalaryWithMVVM.Models;
using GalaryWithMVVM.Views.UserControls;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;
using System.Windows.Threading;

namespace GalaryWithMVVM.ViewModels;

public class PhotoWindowViewModel : BaseViewModel
{
    public Grid MyGrid { get; set; }
    public DispatcherTimer Timer { get; set; }

    public UserControl_Photos user { get; set; }

    private ObservableCollection<GalaryImage> galaries;
    public ObservableCollection<GalaryImage> Galaries
    {
        get { return galaries; }
        set { galaries = value; OnPropertyChanged(); }
    }


    private GalaryImage galary;
    public GalaryImage Galary
    {
        get { return galary; }
        set { galary = value; OnPropertyChanged(); }
    }

    public int Count { get; set; }

    public RelayCommand BackWindowCommand { get; set; }
    public RelayCommand PrevCommand { get; set; }
    public RelayCommand PauseCommand { get; set; }
    public RelayCommand NextCommand { get; set; }



    public PhotoWindowViewModel()
    {
        Timer = new();
        Galaries = new();
        Galary = new();


        BackWindowCommand = new RelayCommand((o) =>
        {
            var window = o as Window;
            window!.Close();
        });


        PrevCommand = new RelayCommand((o) =>
        {
            var mygrid = o as Grid;
            try
            {
                Count--;
                UserControl_Photos photo = new();
                var vm = new UCViewModel();
                vm.CurrentImageSource = new BitmapImage(new Uri(Galaries![Count]!.ImageUrl!, UriKind.Relative));
                vm.Photo = Galaries[Count];
                photo.DataContext = vm;

                if (Count < 0)
                {
                    Count = 0;
                }

                user = photo;
                MyGrid!.Children.Add(photo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Count = 0;
            }

        });


        NextCommand = new RelayCommand((o) =>
        {
            var mygrid = o as Grid;

            try
            {
                UserControl_Photos photo = new();
                var vm = new UCViewModel();
                vm.CurrentImageSource = new BitmapImage(new Uri(Galaries![Count]!.ImageUrl!, UriKind.Relative));
                vm.Photo = Galaries[Count];
                photo.DataContext = vm;

                if (Count > Galaries.Count)
                {
                    Count = Galaries.Count;
                }

                user = photo;
                MyGrid!.Children.Add(photo);
                Count++;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        });


        PauseCommand = new RelayCommand((o) =>
        {
            var toggleButton = o as ToggleButton;
            var ellipse = toggleButton!.Content as Ellipse;
            var imageBrush = ellipse!.Fill as ImageBrush;

            if (toggleButton.IsChecked == true)
            {

                imageBrush!.ImageSource = new BitmapImage(new Uri("../../../Images/play.png"!, UriKind.Relative));

                Timer.Interval = TimeSpan.FromMilliseconds(2000);
                Timer.Tick += Timer_Tick;
                Timer.Start();
            }
            else
            {
                imageBrush!.ImageSource = new BitmapImage(new Uri("../../../Images/pause.png"!, UriKind.Relative));
                Timer.Stop();
            }
        });
    }


    private void Timer_Tick(object? sender, EventArgs e)
    {
        try
        {
            UserControl_Photos photo = new();
            var vm = new UCViewModel();
            vm.CurrentImageSource = new BitmapImage(new Uri(Galaries![Count]!.ImageUrl!, UriKind.Relative));
            vm.Photo = Galaries[Count];
            photo.DataContext = vm;

            if (Count > Galaries.Count)
            {
                Count = Galaries.Count;
            }

            user = photo;
            MyGrid!.Children.Add(photo);
            Count++;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            Timer.Stop();
        }
    }
}