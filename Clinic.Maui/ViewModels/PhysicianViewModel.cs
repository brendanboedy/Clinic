using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Clinic.Library.Models;
using Clinic.Library.Services;

namespace Clinic.Maui.ViewModels;

public class PhysicianViewModel : INotifyPropertyChanged
{
    //ItemsSource Binding
    public ObservableCollection<Physician?> Physicians
    {
        get
        {
            return new ObservableCollection<Physician?>(PhysicianServiceProxy.Current.PhysicianList);
        }
    }
    //ItemSelected Binding
    public Physician? physician { get; set; }
    public void Refresh()
    {
        NotifyPropertyChanged("Physicians");
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
