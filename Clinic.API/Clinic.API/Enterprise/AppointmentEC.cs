using System;
using Clinic.API.Database;
using Clinic.Library.Models;

namespace Clinic.API.Enterprise;

public class AppointmentEC
{
    //return list of appointments from data base
    public IEnumerable<Appointment> GetAppointments()
    {
        return FakeDatabase.Appointments;
    }

    //return appointment by id from data base
    public Appointment? GetByID(int id)
    {
        return FakeDatabase.Appointments.FirstOrDefault(p => p.ID == id);
    }

    //delete appointment from data base by id
    public Appointment? Delete(int id)
    {
        var toRemove = GetByID(id);
        if (toRemove != null)
        {
            FakeDatabase.Appointments.Remove(toRemove);
        }
        return toRemove;
    }
    
    //add or update appointment on data base
    public Appointment? AddOrUpdate(Appointment? appointment)
    {
        Patient? assignedPatient = appointment?.AssignedPatient;
        Physician? assignedPhysician = appointment?.AssignedPhysician;
        
        //make sure appointment and its patient and physician are not null
        if (appointment == null || assignedPatient == null || assignedPhysician == null)
        {
            return null;
        }

        //assign ID for new appointments
        if (appointment.ID <= 0)
        {
            int maxID = -1;
            if (FakeDatabase.Appointments.Any())
            {
                maxID = FakeDatabase.Appointments.Select(a => a?.ID ?? -1).Max();
            }
            else
            {
                maxID = 0;
            }
            appointment.ID = ++maxID;
            FakeDatabase.Appointments.Add(appointment);
        }
        //if appointment already exists, replace w/ new updated appointment
        else
        {
            var apptToCopy = FakeDatabase.Appointments.FirstOrDefault(apt => (apt?.ID ?? 0) == appointment.ID);
            if (apptToCopy != null)
            {
                var index = FakeDatabase.Appointments.IndexOf(apptToCopy);
                FakeDatabase.Appointments.RemoveAt(index);
                FakeDatabase.Appointments.Insert(index, appointment);
            }
        }
        return appointment;
    }

    //return appointment by search query
    public IEnumerable<Appointment?> Search(string query)
    {
        return FakeDatabase.Appointments.Where(p => 
            (p?.AssignedPatient?.Name?.ToUpper()?.Contains(query?.ToUpper() ?? string.Empty) ?? false)
            || (p?.AssignedPhysician?.Name?.ToUpper()?.Contains(query?.ToUpper() ?? string.Empty) ?? false));
    }
}
