using GalaryWithMVVM.Models;
using System.Windows.Media;


namespace GalaryWithMVVM.ViewModels;


public class UCViewModel : BaseViewModel
{
    private GalaryImage? photo;
    public GalaryImage? Photo
    {
        get { return photo; }
        set
        {
            photo = value;
            OnPropertyChanged();
        }
    }


    private ImageSource? _currentImageSource;
    public ImageSource? CurrentImageSource
    {
        get { return _currentImageSource; }
        set
        {
            _currentImageSource = value;
            OnPropertyChanged();
        }
    }
}