using System;
using Clinic.API.Database;
using Clinic.Library.Models;

namespace Clinic.API.Enterprise;

public class PhysicianEC
{
    //return list of physicians from data base
    public IEnumerable<Physician> GetPhysicians()
    {
        return FakeDatabase.Physicians;
    }

    //return physician by id from data base
    public Physician? GetByID(int id)
    {
        return FakeDatabase.Physicians.FirstOrDefault(p => p.ID == id);
    }

    //delete physician by id from data base
    public Physician? Delete(int id)
    {
        var toRemove = GetByID(id);
        if (toRemove != null)
        {
            FakeDatabase.Physicians.Remove(toRemove);
        }
        return toRemove;
    }
    
    //add or update a physician on the data base
    public Physician? AddOrUpdate(Physician? physician)
    {
        //make sure physician not null
        if (physician == null)
        {
            return null;
        }

        //assign ID
        if (physician.ID <= 0)
        {
            //adding new physician
            int maxID = -1;
            if (FakeDatabase.Physicians.Any())
            {
                maxID = FakeDatabase.Physicians.Select(p => p?.ID ?? -1).Max();
            }
            else
            {
                maxID = 0;
            }
            physician.ID = ++maxID;
            FakeDatabase.Physicians.Add(physician);
        }
        else
        {
            var physicianToEdit = FakeDatabase.Physicians.FirstOrDefault(p => (p?.ID ?? 0) == physician.ID);
            if (physicianToEdit != null)
            {
                var index = FakeDatabase.Physicians.IndexOf(physicianToEdit);
                FakeDatabase.Physicians.RemoveAt(index);
                FakeDatabase.Physicians.Insert(index, physician);
            }
        }

        //return added physician or physician to update
        return physician;
    }
}
