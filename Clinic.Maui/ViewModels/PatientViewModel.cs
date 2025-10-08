using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Clinic.Library.Models;
using Clinic.Library.Services;

namespace Clinic.Maui.ViewModels;

public class PatientViewModel : INotifyPropertyChanged
{
    //ItemsSource Binding - list of patients
    public ObservableCollection<Patient?> Patients
    {
        get
        {
            //access private list connected to service proxy
            return new ObservableCollection<Patient?>(PatientServiceProxy.Current.PatientList);
        }
    }
    //SelectedItem in patient view
    public Patient? patient { get; set; }
    public void Refresh()
    {
        NotifyPropertyChanged("Patients");
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
