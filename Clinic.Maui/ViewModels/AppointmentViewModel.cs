using System;

namespace Clinic.Maui.ViewModels;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Clinic.Library.Models;
using Clinic.Library.Services;

public class AppointmentViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Patient?> Patients {
        get {
            return new ObservableCollection<Patient?>(PatientServiceProxy.Current.PatientList);
        }
    }
    public ObservableCollection<Physician?> Physicians {
        get {
            return new ObservableCollection<Physician?>(PhysicianServiceProxy.Current.PhysicianList);
        }
    }
    public ObservableCollection<AddAppointmentViewModel?> Appointments{
        get{
            return new ObservableCollection<AddAppointmentViewModel?>
            (AppointmentServiceProxy
            .Current
            .AppointmentList
            .Select(ap => new AddAppointmentViewModel(ap)));
        }
    }
    public void Refresh()
    {
        NotifyPropertyChanged(nameof(Patients));
        NotifyPropertyChanged(nameof(Physicians));
        NotifyPropertyChanged(nameof(Appointments));
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    /*
    //references to proxys
    private PatientServiceProxy _patientSvc;
    private PhysicianServiceProxy _physicianSvc;
    private AppointmentServiceProxy _appointmentSvc;

    //private appointment list
    private List<Appointment?> appointments;

    //Binding Context ItemsSource "Appointments"
    public List<Appointment?> Appointments
    {
        get
        {
            return appointments;
        }
    }

    //Binding Context SelectedItem "selectedAppointment"
    public Appointment? selectedAppointment { get; set; }
    
    //constructor
    public AppointmentViewModel()
    {
        //assign proxys
        _patientSvc = PatientServiceProxy.Current;
        _physicianSvc = PhysicianServiceProxy.Current;
        _appointmentSvc = AppointmentServiceProxy.Current;
        appointments = _appointmentSvc.AppointmentList;

        //assign patient and physician for each appointment
        foreach (var app in Appointments)
        {
            if (app != null)
            {
                app.Patient = _patientSvc.GetByID(app.PatientID);
                app.Physician = _physicianSvc.GetByID(app.PhysicianID);
            }
        }
    }
    */
}
