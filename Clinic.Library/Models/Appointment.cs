using System;
using Clinic.Library.Services;

namespace Clinic.Library.Models;

public class Appointment
{
    public int ID { get; set; }
    public DateTime? AppointmentDate { get; set; }
    public TimeOnly? AppointmentStartTime { get; set; }
    //property for conversion between TimePicker TimeSpan and TimeOnly AppointmentStartTime
    public TimeSpan? AppointmentTimeSpan
    {
        get
        {
            return AppointmentStartTime?.ToTimeSpan() ?? new TimeSpan();
        }
        set
        {
            if (value != null)
            {
                //set appointment start time and end time
                AppointmentStartTime = TimeOnly.FromTimeSpan(value ?? new TimeSpan());
                AppointmentEndTime = AppointmentStartTime.Value.AddHours(1);
            }
        }
    }
    public TimeOnly? AppointmentEndTime { get; set; }
    public Patient? AssignedPatient { get; set; }
    public Physician? AssignedPhysician { get; set; }

    //ToString Override
    public override string ToString()
    {
        //issue with AppointmentStartTime printing
        return $"{ID}. Patient: {AssignedPatient?.Name}, Physician: {AssignedPhysician?.Name}" +
        $"\nDate: {AppointmentDate:MM/dd/yyyy}, Time: {AppointmentStartTime?.ToString("hh:mm tt") ?? ""}";
    }

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
        //assign appointment to appointmentCopy if it exists
        if (appointmentCopy != null)
        {
            ID = appointmentCopy.ID;
            AppointmentDate = appointmentCopy.AppointmentDate;
            AppointmentStartTime = appointmentCopy.AppointmentStartTime;
            AppointmentEndTime = appointmentCopy.AppointmentEndTime;
            AssignedPatient = appointmentCopy.AssignedPatient;
            AssignedPhysician = appointmentCopy.AssignedPhysician;
        }
    }
    public Appointment(int patID, int phyID)
    {
        AssignedPatient = PatientServiceProxy.Current.PatientList.FirstOrDefault(p => (p?.ID ?? 0) == patID);
        AssignedPhysician = PhysicianServiceProxy.Current.PhysicianList.FirstOrDefault(p => (p?.ID ?? 0) == phyID);
    }
    public Appointment(){}
}
