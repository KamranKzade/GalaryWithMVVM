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

namespace GalaryWithMVVM.ViewModels;

public class Add_Image_WindowViewModel
{
    public GalaryImage Image { get; set; }
    public string filePath { get; set; }




    public RelayCommand AddImageCommand { get; set; }
    public RelayCommand AddImageButtonWithCommand { get; set; }




    public Add_Image_WindowViewModel()
    {
        Image = new GalaryImage();


        AddImageCommand = new RelayCommand((o) =>
        {
            var Picture = o as ImageBrush;

            OpenFileDialog op = new OpenFileDialog();

            op.Title = "Select a picture";
            op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            if (op.ShowDialog() == true)
            {
                filePath = op.FileName;

                Picture!.ImageSource = new BitmapImage(new Uri(op.FileName));
                Picture.Stretch = Stretch.Uniform;
            }
        });

        AddImageButtonWithCommand = new RelayCommand((o) =>
        {
            var window = o as Window;

            //object mystackpanel =  window.FindName("myStackPanel");

            foreach (StackPanel tb in FindVisualChilds<StackPanel>(window))
            {
                var imageName_name = (tb!.Children[0] as Grid)!.Children[1] as TextBox;
                var author_name = (tb.Children[2] as Grid)!.Children[1] as TextBox;
                var creation_time = (tb.Children[3] as Grid)!.Children[1] as TextBox;

                Image.Name = imageName_name!.Text;
                Image.Author = author_name!.Text;
                Image.ImageUrl = filePath;


                try
                {
                    Image.Time = DateTime.Parse(creation_time!.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                window.Close();
                break;
            }

            ///Application.Current.Shutdown();

        });
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





    //private void Profile_Photo_DragEnter(object sender, DragEventArgs e)
    //{
    //    if (e.Data.GetDataPresent(DataFormats.FileDrop))
    //    {
    //        string[] filenames = e.Data.GetData(DataFormats.FileDrop, true) as string[];

    //        foreach (string fileName in filenames!)
    //        {
    //            Picture.ImageSource = new BitmapImage(new Uri(fileName));

    //            filePath = fileName;
    //        }
    //    }
    //}

    //private void Button_Click(object sender, RoutedEventArgs e)
    //{
    //    Image.Name = image_name.Text;
    //    Image.Author = author_name.Text;
    //    Image.ImageUrl = filePath;
    //    try
    //    {
    //        Image.Time = DateTime.Parse(creation_name.Text);
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
    //    }

    //    DialogResult = true;
    //}
}
