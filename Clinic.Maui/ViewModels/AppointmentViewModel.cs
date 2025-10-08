using System;

namespace Clinic.Maui.ViewModels;

using Clinic.Library.Models;
using Clinic.Library.Services;

public class AppointmentViewModel
{
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
}
