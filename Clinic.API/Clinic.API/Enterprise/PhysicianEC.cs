using System;
using Clinic.API.Database;
using Clinic.Library.Models;

namespace Clinic.API.Enterprise;

public class PhysicianEC
{
    public IEnumerable<Physician> GetPhysicians()
    {
        return FakeDatabase.Physicians;
    }
    public Physician? GetByID(int id)
    {
        return FakeDatabase.Physicians.FirstOrDefault(p => p.ID == id);
    }
    public Physician? Delete(int id)
    {
        var toRemove = GetByID(id);
        if (toRemove != null)
        {
            FakeDatabase.Physicians.Remove(toRemove);
        }
        return toRemove;
    }
}
