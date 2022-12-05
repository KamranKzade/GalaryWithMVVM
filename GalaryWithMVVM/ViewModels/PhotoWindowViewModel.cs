﻿using GalaryWithMVVM.Commands;
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
using System.Windows.Threading;

namespace GalaryWithMVVM.ViewModels;

public class PhotoWindowViewModel:BaseViewModel
{

    public DispatcherTimer Timer { get; set; }

    public UserControl_Photos user { get; set; }


    private ObservableCollection<GalaryImage> galaries;

    public ObservableCollection<GalaryImage> Galaries
    {
        get { return galaries; }
        set { galaries = value;OnPropertyChanged(); }
    }




    public int MyProperty { get; set; }


    public RelayCommand BackWindowCommand { get; set; }
    public RelayCommand PrevCommand { get; set; }
    public RelayCommand PauseCommand { get; set; }
    public RelayCommand NextCommand { get; set; }


    public PhotoWindowViewModel()
    {
        Timer = new();

        Galaries = new();


        BackWindowCommand = new RelayCommand((o) =>
        {
            var window = o as Window;
            window.Close();
        });


        PrevCommand = new RelayCommand((o) =>
        {
            var mygrid = o as Grid;

            try
            {
                var vm = new UCViewModel();

                UserControl_Photos photo = new();

                vm.CurrentImageSource = new BitmapImage(new Uri(Galaries![Galaries.IndexOf(vm.Photo) - 1]!.ImageUrl!, UriKind.Relative));
                vm.Photo = Galaries[Galaries.IndexOf(vm.Photo) - 1];

                photo.DataContext = vm;

                user = photo;

                mygrid.Children.Add(photo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        });

        NextCommand = new RelayCommand((o) =>
        {
            var mygrid = o as Grid;

            try
            {
                var vm = new UCViewModel();

                UserControl_Photos photo = new();

                vm.CurrentImageSource = new BitmapImage(new Uri(Galaries![Galaries.IndexOf(vm.Photo) + 1]!.ImageUrl!, UriKind.Relative));
                vm.Photo = Galaries[Galaries.IndexOf(vm.Photo) + 1];
                photo.DataContext = vm;


                user = photo;
                mygrid!.Children.Add(photo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        });


        PauseCommand = new RelayCommand((o) =>
        {
            var grid = o as Grid;
            var mygrid = grid!.Children[2] as Grid;
            var toggleButton = (grid!.Children[3] as StackPanel)!.Children[1] as ToggleButton;
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
            var vm = new UCViewModel();

            UserControl_Photos photo = new();

            vm.CurrentImageSource = new BitmapImage(new Uri(Galaries![Galaries.IndexOf(vm.Photo) + 1]!.ImageUrl!, UriKind.Relative));
            vm.Photo = Galaries[Galaries.IndexOf(vm.Photo) + 1];

            photo.DataContext = vm;

            user = photo;
            //  mygrid.Children.Add(photo);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            Timer.Stop();
        }
    }
}