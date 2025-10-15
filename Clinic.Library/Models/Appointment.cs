using System;
using Clinic.Library.Services;

namespace Clinic.Library.Models;

public class Appointment
{
    public int ID { get; set; }
    public DateOnly? AppointmentDate { get; set; }
    public TimeOnly? AppointmentStartTime { get; set; }
    public TimeOnly? AppointmentEndTime { get; set; }
    public Patient? AssignedPatient { get; set; }
    public Physician? AssignedPhysician { get; set; }

    //ToString Override
    public override string ToString()
    {
        return $"{ID}. Patient: {AssignedPatient?.Name}, Physician: {AssignedPhysician?.Name},\n" +
            "Date: {AppointmentDate}, Time: {AppointmentStartTime}";
    }
    public Appointment() { }

    //display property
    public string Display
    {
        get
        {
            return ToString();
        }
    }
    public Appointment(int apID)
    {
        var appointmentCopy = AppointmentServiceProxy
            .Current
            .AppointmentList
            .FirstOrDefault(a => (a?.ID ?? 0) == apID);
        //get patient and physician of appointment
        int patID = appointmentCopy?.AssignedPatient?.ID ?? 0;
        int phyID = appointmentCopy?.AssignedPhysician?.ID ?? 0;

        //assign appointment to appointmentCopy if it exists
        if (appointmentCopy != null)
        {
            ID = appointmentCopy.ID;
            AppointmentDate = appointmentCopy.AppointmentDate;
            AppointmentStartTime = appointmentCopy.AppointmentStartTime;
            AppointmentEndTime = appointmentCopy.AppointmentEndTime;
            AssignedPatient = PatientServiceProxy.Current.PatientList.FirstOrDefault(p => (p?.ID ?? 0) == patID);
            AssignedPhysician = PhysicianServiceProxy.Current.PhysicianList.FirstOrDefault(p => (p?.ID ?? 0) == phyID);
        }
    }

    public Appointment(int patID, int phyID)
    {
        AssignedPatient = PatientServiceProxy.Current.PatientList.FirstOrDefault(p => (p?.ID ?? 0) == patID);
        AssignedPhysician = PhysicianServiceProxy.Current.PhysicianList.FirstOrDefault(p => (p?.ID ?? 0) == phyID);
    }
}
