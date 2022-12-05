using GalaryWithMVVM.Commands;
using GalaryWithMVVM.Models;
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

    public RelayCommand SmallIconCommand { get; set; }
    public RelayCommand NormadIconCommand { get; set; }
    public RelayCommand LargeIconCommand { get; set; }



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

            //photo.MouseDoubleClick += Photo_MouseDoubleClick;
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
    }



    //private void Photo_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    //{
    //    var uc = sender as UserControl_Photos;


    //    Windows.PhotoWindow window = new(uc!.CurrentImageSource, uc.Photo, GalaryImages);

    //    window.ShowDialog();

    //}

    //private void MenuItem_Click_1(object sender, RoutedEventArgs e) => wrapPanel.Columns = 2;
    //private void MenuItem_Click_2(object sender, RoutedEventArgs e) => wrapPanel.Columns = 3;
    //private void MenuItem_Click(object sender, RoutedEventArgs e) => wrapPanel.Columns = 4;


    //private void Add_Image(object sender, RoutedEventArgs e)
    //{
    //    Add_Image_Window add = new();
    //    add.ShowDialog();

    //    BitmapImage picture = new BitmapImage(new Uri(add.filePath!, UriKind.Relative));

    //    UserControl_Photos uc = new(picture, add.Image);

    //    wrapPanel.Children.Add(uc);
    //    GalaryImages.Add(add.Image);

    //}

}
