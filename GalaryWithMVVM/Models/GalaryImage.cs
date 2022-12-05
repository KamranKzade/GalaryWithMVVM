using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GalaryWithMVVM.Models;

public class GalaryImage: INotifyPropertyChanged
{


    private string? name;

    public string? Name
    {
        get { return name; }
        set
        {
            name = value;
            OnPropertyChanged();
        }
    }


    private string? author;

    public string? Author
    {
        get { return author; }
        set
        {
            author = value;
            OnPropertyChanged();
        }
    }


    private string? imageUrl;

    public string? ImageUrl
    {
        get { return imageUrl; }
        set
        {
            imageUrl = value;
            OnPropertyChanged();
        }
    }


    private DateTime time;

    public DateTime Time
    {
        get { return time; }
        set
        {
            time = value;
            OnPropertyChanged();
        }
    }



    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null!)
    {
        PropertyChangedEventHandler handler = PropertyChanged!;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(name));
        }
    }
}


