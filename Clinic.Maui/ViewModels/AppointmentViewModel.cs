using System;

namespace Clinic.Maui.ViewModels;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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
    //property for selected appointment
    public AddAppointmentViewModel? SelectedAppointment { get; set; }
    //public properties for binding entry for patient/physician ID
    public int patientID { get; set; }
    public int physicianID { get; set; }

    //property for search bar
    public string? Query { get; set;}

    //method for search w query
    /*public void Search()
    {
        var appointmentDTOs = AppointmentServiceProxy.Current.Search(new Clinic.Library.Data.QueryRequest { Content = Query }).Result;
        Appointments = new ObservableCollection<AddAppointmentViewModel?>(appointmentDTOs.Select(a => new AddAppointmentViewModel(a)));
        NotifyPropertyChanged(nameof(Appointments));
    }*/

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
    public async Task AddAppointmentCheck()
    {
        //grab corresponding patient
        Patient? thePatient = PatientServiceProxy
            .Current
            .PatientList
            .FirstOrDefault
            (p => (p?.ID ?? 0) == patientID);
        Physician? thePhysician = PhysicianServiceProxy
            .Current
            .PhysicianList
            .FirstOrDefault
            (p => (p?.ID ?? 0) == physicianID);
        //if either is null - change visibility of error label
        if (thePatient == null || thePhysician == null)
        {
            ErrorLabelVisibility = true;
        }
        //collapse error label visibility, navigate to AddAppointmentView
        else
        {
            ErrorLabelVisibility = false;
            await Shell.Current.GoToAsync("//AddAppointment", new Dictionary<string, object>
            {
                ["PatientID"] = patientID,
                ["PhysicianID"] = physicianID,
                ["AppointmentID"] = 0,
                ["_Nonce"] = Guid.NewGuid().ToString()
            });
            //Shell.Current.GoToAsync($"//AddAppointment?PatientID={patientID}&PhysicianID={physicianID}&AppointmentID=0");
        }
        
    }
    public void Refresh()
    {
        NotifyPropertyChanged(nameof(Patients));
        NotifyPropertyChanged(nameof(Physicians));
        NotifyPropertyChanged(nameof(Appointments));
        ErrorLabelVisibility = false;
    }
    public void Delete()
    {
        if(SelectedAppointment == null)
        {
            return;
        }
        AppointmentServiceProxy.Current.Delete(SelectedAppointment.Model?.ID ?? 0);
        NotifyPropertyChanged(nameof(Appointments));
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
