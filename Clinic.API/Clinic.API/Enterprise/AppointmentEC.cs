using System;
using Clinic.API.Database;
using Clinic.Library.Models;

namespace Clinic.API.Enterprise;

public class AppointmentEC
{
    public IEnumerable<Appointment> GetAppointments()
    {
        return FakeDatabase.Appointments;
    }
    public Appointment? GetByID(int id)
    {
        return FakeDatabase.Appointments.FirstOrDefault(p => p.ID == id);
    }
    public Appointment? Delete(int id)
    {
        var toRemove = GetByID(id);
        if (toRemove != null)
        {
            FakeDatabase.Appointments.Remove(toRemove);
        }
        return toRemove;
    }
}
