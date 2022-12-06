using GalaryWithMVVM.Models;
using System.Windows.Media;

namespace GalaryWithMVVM.ViewModels;

public class Model
{
    public ImageSource? CurrentImageSource { get; internal set; }
    public GalaryImage? Photo { get; internal set; }
}