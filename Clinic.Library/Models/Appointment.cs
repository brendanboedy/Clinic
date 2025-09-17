using System;

namespace Clinic.Library.Models;

public class Appointment
{
    public int ID { get; set; }
    public DateOnly AppointmentDate { get; set; }
    public TimeOnly AppointmentStartTime { get; set; }
    public TimeOnly AppointmentEndTime { get; set; }
    public int PatientID { get; set; }
    public int PhysicianID { get; set; }

    //ToString Override
    public override string ToString()
    {
        return $"{ID}. {AppointmentDate} - Patient ID: {PatientID}, Physician ID: {PhysicianID}\n" +
            $"Start Time: {AppointmentStartTime}, End Time: {AppointmentEndTime}";
    }

    //Constructor
    public Appointment(DateOnly appointmentDate, TimeOnly appointmentStartTime, int patientId, int physicianId)
    {
        AppointmentDate = appointmentDate;
        AppointmentStartTime = appointmentStartTime;
        AppointmentEndTime = appointmentStartTime.AddHours(1); //Default to 1 hour appointments
        PatientID = patientId;
        PhysicianID = physicianId;
    }
}
