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
    public Physician? SelectedPhysician { get; set; }
    public void Refresh()
    {
        NotifyPropertyChanged(nameof(Physicians));
    }
    public void Delete()
    {
        //make sure physician not null
        if(SelectedPhysician == null)
        {
            return;
        }
        PhysicianServiceProxy.Current.Delete(SelectedPhysician?.ID ?? 0);
        //update view
        NotifyPropertyChanged(nameof(Physicians));
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
