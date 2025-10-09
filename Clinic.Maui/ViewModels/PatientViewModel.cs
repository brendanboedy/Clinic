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
    public Patient? SelectedPatient { get; set; }
    public void Refresh()
    {
        NotifyPropertyChanged(nameof(Patients));
    }
    public void Delete()
    {
        //check to make sure not passing null patient to proxy delete method
        if (SelectedPatient == null)
        {
            return;
        }
        PatientServiceProxy.Current.Delete(SelectedPatient?.ID ?? 0);
        //update view
        NotifyPropertyChanged(nameof(Patients));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
