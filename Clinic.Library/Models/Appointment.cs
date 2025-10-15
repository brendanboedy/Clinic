using System;
using Clinic.Library.Services;

namespace Clinic.Library.Models;

public class Appointment
{
    public int ID { get; set; }
    public DateOnly? AppointmentDate { get; set; }
    public TimeOnly? AppointmentStartTime { get; set; }
    public TimeOnly? AppointmentEndTime { get; set; }
    public Patient? AssignedPatient;
    public Physician? AssignedPhysician;

    //ToString Override
    public override string ToString()
    {
        return $"{ID}. {AppointmentDate} - Patient ID: {AssignedPatient?.ID}, Physician ID: {AssignedPhysician?.ID}\n" +
            $"Start Time: {AppointmentStartTime}, End Time: {AppointmentEndTime}\n";
    }
    public Appointment() { }

    public Appointment(int apID, int patID, int phyID)
    {
        var appointmentCopy = AppointmentServiceProxy
            .Current
            .AppointmentList
            .FirstOrDefault(a => (a?.ID ?? 0) == apID);
        //assign appointment to appointment to copy if it exists
        if (appointmentCopy != null)
        {
            ID = appointmentCopy.ID;
            AppointmentDate = appointmentCopy.AppointmentDate;
            AppointmentStartTime = appointmentCopy.AppointmentStartTime;
            AppointmentEndTime = appointmentCopy.AppointmentEndTime;
            AssignedPatient = appointmentCopy.AssignedPatient;
            AssignedPhysician = appointmentCopy.AssignedPhysician;
        }
        //assign corresponding patient and physician
        else
        {
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
