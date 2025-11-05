using System;
using Clinic.API.Database;
using Clinic.Library.Models;

namespace Clinic.API.Enterprise;

public class PatientEC
{
    public IEnumerable<Patient> GetPatients()
    {
        return FakeDatabase.Patients;
    }
    public Patient? GetByID(int id)
    {
        return FakeDatabase.Patients.FirstOrDefault(p => p.ID == id);
    }
    public Patient? Delete(int id)
    {
        var toRemove = GetByID(id);
        if (toRemove != null)
        {
            FakeDatabase.Patients.Remove(toRemove);
        }
        return toRemove;
    }
}
