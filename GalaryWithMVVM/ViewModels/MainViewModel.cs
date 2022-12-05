﻿using GalaryWithMVVM.Commands;
using GalaryWithMVVM.Models;
using GalaryWithMVVM.Views;
using GalaryWithMVVM.Views.UserControls;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;



namespace GalaryWithMVVM.ViewModels;



public class MainViewModel : BaseViewModel
{

    private ObservableCollection<GalaryImage> galaryImages;
    public ObservableCollection<GalaryImage> GalaryImages
    {
        get { return galaryImages; }
        set
        {
            galaryImages = value;
            OnPropertyChanged();
        }
    }


    public GalaryImage GalaryImage { get; set; }

    public RelayCommand SmallIconCommand { get; set; }
    public RelayCommand NormadIconCommand { get; set; }
    public RelayCommand LargeIconCommand { get; set; }
    public RelayCommand AddImageCommand { get; set; }



    public BitmapImage CurrentPicture { get; set; }


    public MainViewModel(UniformGrid uniform)
    {
        GalaryImages = new();
        GalaryImages = new ObservableCollection<GalaryImage>(Repositories.FakeRepo.GetGalaryImages());

        foreach (var image in GalaryImages)
        {
            BitmapImage picture = new BitmapImage(new Uri(image!.ImageUrl!, UriKind.Relative));
            CurrentPicture = picture;

            var vm = new UCViewModel();
            vm.CurrentImageSource = picture;
            vm.Photo = image;

            var uc = new UserControl_Photos();
            uc.DataContext = vm;

            uniform.Children.Add(uc);

            uc.MouseDoubleClick += Photo_MouseDoubleClick;
        }


        MessageBox.Show(@"if you want to add a picture
Edit -> Add Image", "Information", MessageBoxButton.OK, MessageBoxImage.Information);


        SmallIconCommand = new RelayCommand((o) =>
        {
            var uniformGrid = o as UniformGrid;
            uniformGrid!.Columns = 4;
        });

        NormadIconCommand = new RelayCommand((o) =>
        {
            var uniformGrid = o as UniformGrid;
            uniformGrid!.Columns = 3;
        });

        LargeIconCommand = new RelayCommand((o) =>
        {
            var uniformGrid = o as UniformGrid;
            uniformGrid!.Columns = 2;
        });


        AddImageCommand = new RelayCommand((o) =>
        {
            var uniformGrid = o as UniformGrid;


            var viewModelForAdd = new Add_Image_WindowViewModel();

            Add_Image_Window addWindowPage = new();
            addWindowPage.DataContext = viewModelForAdd;
            addWindowPage.ShowDialog();



            BitmapImage picture = new BitmapImage(new Uri(viewModelForAdd.filePath!, UriKind.Relative));


            

            var uCViewModel = new UCViewModel();
            uCViewModel.CurrentImageSource = picture;
            uCViewModel.Photo = viewModelForAdd.Image;

            UserControl_Photos uc = new UserControl_Photos();
            uc.DataContext = uCViewModel;


            uniformGrid.Children.Add(uc);
            GalaryImages.Add(viewModelForAdd.Image);
        });
    }



    private void Photo_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var uc = sender as UserControl_Photos;






        var viewModel = new PhotoWindowViewModel();
        viewModel.Galaries = GalaryImages;
        viewModel.user = uc!;

        PhotoWindow window = new();
        window.DataContext = viewModel;



        window.ShowDialog();

    }

}
