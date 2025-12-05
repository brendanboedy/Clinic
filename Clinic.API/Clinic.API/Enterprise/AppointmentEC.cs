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
        return Filebase.Current.Appointments.Select(ap => new AppointmentDTO(ap));
    }
    //FakeDatabase
    //return appointment by id from data base
    public AppointmentDTO? GetByID(int id)
    {
        var appointment = Filebase.Current.Appointments.FirstOrDefault(ap => ap.ID == id);
        return new AppointmentDTO(appointment);
    }

    //delete appointment from data base by id
    public AppointmentDTO? Delete(int id)
    {
        //var toRemove = GetByID(id);
        var toRemove = Filebase.Current.Appointments.FirstOrDefault(ap => ap.ID == id);
        if (toRemove != null)
        {
            Filebase.Current.Appointments.Remove(toRemove);
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

        var appointment = new Appointment(appointmentDTO);
        appointmentDTO = new AppointmentDTO(Filebase.Current.AddOrUpdate(appointment));
        return appointmentDTO;
    }
    /*
    //return appointment by search query
    public IEnumerable<AppointmentDTO?> Search(string query)
    {
        var queryList = FakeDatabase.Appointments.Where(ap => 
            (ap?.AssignedPatient?.Name?.ToUpper()?.Contains(query?.ToUpper() ?? string.Empty) ?? false)
            || (ap?.AssignedPhysician?.Name?.ToUpper()?.Contains(query?.ToUpper() ?? string.Empty) ?? false))
            .Select(ap => new AppointmentDTO(ap));
        return queryList;
    }*/
}
