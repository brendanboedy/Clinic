using System;
using Clinic.Library.Models;

namespace Clinic.Library.Services;

public class AppointmentServiceProxy
{
    //list to hold appointments in memory
    private List<Appointment?> appointmentList { get; set; }

    //access to patient and physician service proxy
    //private PatientServiceProxy _patientSvc;
    //private PhysicianServiceProxy _physicianSvc;

    //private proxy constructor
    private AppointmentServiceProxy()
    {
        //_patientSvc = PatientServiceProxy.Current;
        //_physicianSvc = PhysicianServiceProxy.Current;
        appointmentList = new List<Appointment?>();
    }

    //singleton instance and lock object
    private static AppointmentServiceProxy? instance;
    private static object instanceLock = new object();

    //static property to get singleton instance
    //lock critical section
    public static AppointmentServiceProxy Current
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new AppointmentServiceProxy();
                }
                return instance;
            }
        }
    }

    //public getter to access private list
    public List<Appointment?> AppointmentList
    {
        get { return appointmentList; }
    }

    public Appointment? Add(Appointment? appointment)
    {
        //make sure appointment not null
        if (appointment == null)
        {
            return null;
        }

        //assign ID
        if (appointment.ID <= 0)
        {
            int maxID = -1;
            if (appointmentList.Any())
            {
                maxID = appointmentList.Select(a => a?.ID ?? -1).Max();
            }
            else
            {
                maxID = 0;
            }
            appointment.ID = ++maxID;
            appointmentList.Add(appointment);
        }

        return appointment;
    }

    public Appointment? Delete(int appointmentID)
    {
        //find appointment by ID
        var existingAppointment = appointmentList.FirstOrDefault(a => a?.ID == appointmentID);
        if (existingAppointment == null) { return null; }

        //remove appointment from list
        appointmentList.Remove(existingAppointment);
        return existingAppointment;
    }

    public Appointment? GetByID(int ID)
    {
        if (ID <= 0)
        {
            return null;
        }
        return appointmentList.FirstOrDefault(a => a?.ID == ID);
    }
}
