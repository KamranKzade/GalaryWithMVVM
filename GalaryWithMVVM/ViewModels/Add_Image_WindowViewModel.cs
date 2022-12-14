using GalaryWithMVVM.Commands;
using GalaryWithMVVM.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GalaryWithMVVM.ViewModels;

public class Add_Image_WindowViewModel : BaseViewModel
{
    public GalaryImage Image { get; set; }
    public string FilePath { get; set; }
    public ImageBrush Picture { get; set; }



    public RelayCommand AddImageCommand { get; set; }
    public RelayCommand AddImageButtonWithCommand { get; set; }
    public RelayCommand CLoadModelFromDisk { get; set; }



    public Add_Image_WindowViewModel()
    {
        Image = new GalaryImage();


        AddImageCommand = new RelayCommand((o) =>
        {
            var Picture = o as ImageBrush;

            OpenFileDialog Op = new ();

            Op.Title = "Select a picture";
            Op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            if (Op.ShowDialog() == true)
            {
                FilePath = Op.FileName;

                Picture!.ImageSource = new BitmapImage(new Uri(Op.FileName));
                Picture.Stretch = Stretch.Uniform;
            }
        });

        AddImageButtonWithCommand = new RelayCommand((o) =>
        {
            var window = o as Window;


            foreach (StackPanel tb in FindVisualChilds<StackPanel>(window!))
            {
                var imageName_name = (tb!.Children[0] as Grid)!.Children[1] as TextBox;
                var author_name = (tb.Children[2] as Grid)!.Children[1] as TextBox;
                var creation_time = (tb.Children[3] as Grid)!.Children[1] as TextBox;

                Image.Name = imageName_name!.Text;
                Image.Author = author_name!.Text;
                Image.ImageUrl = FilePath;


                try
                {
                    Image.Time = DateTime.Parse(creation_time!.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                window!.Close();
                break;
            }


        });

        CLoadModelFromDisk = new RelayCommand((o) =>
        {
            var ellipse = o as Ellipse;
            var picture = ellipse!.Fill as ImageBrush;
            Picture = picture!;
            ellipse.DragEnter += Ellipse_DragEnter;



        });
    }

    private void Ellipse_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            string[]? filenames = e.Data.GetData(DataFormats.FileDrop, true) as string[];

            foreach (string fileName in filenames!)
            {
                Picture.ImageSource = new BitmapImage(new Uri(fileName));

                FilePath = fileName;
            }
        }
    }

    public static IEnumerable<T> FindVisualChilds<T>(DependencyObject depObj) where T : DependencyObject
    {
        if (depObj == null) yield return (T)Enumerable.Empty<T>();
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
        {
            DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
            if (ithChild == null) continue;
            if (ithChild is T t) yield return t;
            foreach (T childOfChild in FindVisualChilds<T>(ithChild)) yield return childOfChild;
        }
    }
}
