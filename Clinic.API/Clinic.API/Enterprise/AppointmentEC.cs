using System;
using Clinic.API.Database;
using Clinic.Library.Models;
using Clinic.Library.DTO;

namespace Clinic.API.Enterprise;

public class AppointmentEC
{
    //return list of appointments from data base
    public IEnumerable<AppointmentDTO> GetAppointments()
    {
        return FakeDatabase.Appointments.Select(ap => new AppointmentDTO(ap));
    }

    //return appointment by id from data base
    public AppointmentDTO? GetByID(int id)
    {
        var appointment = FakeDatabase.Appointments.FirstOrDefault(ap => ap.ID == id);
        return new AppointmentDTO(appointment);
    }

    //delete appointment from data base by id
    public AppointmentDTO? Delete(int id)
    {
        //var toRemove = GetByID(id);
        var toRemove = FakeDatabase.Appointments.FirstOrDefault(ap => ap.ID == id);
        if (toRemove != null)
        {
            FakeDatabase.Appointments.Remove(toRemove);
        }
        return new AppointmentDTO(toRemove);
    }
    
    //add or update appointment on data base
    public AppointmentDTO? AddOrUpdate(AppointmentDTO? appointmentDTO)
    {
        Patient? assignedPatient = appointmentDTO?.AssignedPatient;
        Physician? assignedPhysician = appointmentDTO?.AssignedPhysician;
        
        //make sure appointment and its patient and physician are not null
        if (appointmentDTO == null || assignedPatient == null || assignedPhysician == null)
        {
            return null;
        }

        //assign ID for new appointments
        if (appointmentDTO.ID <= 0)
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
            appointmentDTO.ID = ++maxID;
            //convert appointmentDTO into an appointment
            FakeDatabase.Appointments.Add(new Appointment(appointmentDTO));
        }
        //if appointment already exists, replace w/ new updated appointment
        else
        {
            var apptToCopy = FakeDatabase.Appointments.FirstOrDefault(apt => (apt?.ID ?? 0) == appointmentDTO.ID);
            if (apptToCopy != null)
            {
                var index = FakeDatabase.Appointments.IndexOf(apptToCopy);
                FakeDatabase.Appointments.RemoveAt(index);
                FakeDatabase.Appointments.Insert(index, new Appointment(appointmentDTO));
            }
        }
        return appointmentDTO;
    }

    //return appointment by search query
    public IEnumerable<AppointmentDTO?> Search(string query)
    {
        var queryList = FakeDatabase.Appointments.Where(ap => 
            (ap?.AssignedPatient?.Name?.ToUpper()?.Contains(query?.ToUpper() ?? string.Empty) ?? false)
            || (ap?.AssignedPhysician?.Name?.ToUpper()?.Contains(query?.ToUpper() ?? string.Empty) ?? false))
            .Select(ap => new AppointmentDTO(ap));
        return queryList;
    }
}
