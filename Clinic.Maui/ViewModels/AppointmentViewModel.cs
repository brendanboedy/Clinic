using System;

namespace Clinic.Maui.ViewModels;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Clinic.Library.Models;
using Clinic.Library.Services;

public class AppointmentViewModel : INotifyPropertyChanged
{
    public AppointmentViewModel()
    {
        ErrorLabelVisibility = false;
    }
    //list of current patients
    public ObservableCollection<Patient?> Patients
    {
        get
        {
            return new ObservableCollection<Patient?>(PatientServiceProxy.Current.PatientList);
        }
    }
    //list of current physicians
    public ObservableCollection<Physician?> Physicians
    {
        get
        {
            return new ObservableCollection<Physician?>(PhysicianServiceProxy.Current.PhysicianList);
        }
    }
    //list of current appointments
    public ObservableCollection<AddAppointmentViewModel?> Appointments
    {
        get
        {
            return new ObservableCollection<AddAppointmentViewModel?>
            (AppointmentServiceProxy
            .Current
            .AppointmentList
            .Select(ap => new AddAppointmentViewModel(ap)));
        }
    }
    //public properties for binding entry for patient/physician ID
    public int PatientID { get; set; }
    public int PhysicianID { get; set; }

    //properties for visbility of error label
    private bool errorLabelVisibility;
    public bool ErrorLabelVisibility
    {
        get{ return errorLabelVisibility; }
        set
        {
            if(errorLabelVisibility != value)
            {
                errorLabelVisibility = value;
                NotifyPropertyChanged();
            }
        }
    }

    //function to check validity of entered IDs and print error message if invalid
    public void AddAppointmentCheck()
    {
        //grab corresponding patient
        Patient? thePatient = PatientServiceProxy
            .Current
            .PatientList
            .FirstOrDefault
            (p => (p?.ID ?? 0) == PatientID);
        Physician? thePhysician = PhysicianServiceProxy
            .Current
            .PhysicianList
            .FirstOrDefault
            (p => (p?.ID ?? 0) == PhysicianID);
        //if either is null - change visibility of error label
        if (thePatient == null || thePhysician == null)
        {
            if(ErrorLabelVisibility == false)
            {
                ErrorLabelVisibility = true;
            }
        }
        //collapse error label visibility, navigate to AddAppointmentView
        else
        {
            if (ErrorLabelVisibility == true)
            {
                ErrorLabelVisibility = false;
            }
            Shell.Current.GoToAsync($"//AddAppointment?PatientID={thePatient.ID}&PhysicianID={thePhysician.ID}&AppointmentID=0");
        }
        
    }
    public void Refresh()
    {
        NotifyPropertyChanged(nameof(Patients));
        NotifyPropertyChanged(nameof(Physicians));
        NotifyPropertyChanged(nameof(Appointments));
        ErrorLabelVisibility = false;
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
