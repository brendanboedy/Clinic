using System;
using Clinic.API.Database;
using Clinic.Library.Models;

namespace Clinic.API.Enterprise;

public class PatientEC
{
    //return list of patients in database
    public IEnumerable<Patient> GetPatients()
    {
        return FakeDatabase.Patients;
    }

    //return patient ID
    public Patient? GetByID(int id)
    {
        return FakeDatabase.Patients.FirstOrDefault(p => p.ID == id);
    }

    // Delete patient from database
    public Patient? Delete(int id)
    {
        var toRemove = GetByID(id);
        if (toRemove != null)
        {
            FakeDatabase.Patients.Remove(toRemove);
        }
        return toRemove;
    }

    //add or update patient in database
    public Patient? AddOrUpdate(Patient? patient)
    {
        //make sure patient not null
        if (patient == null)
        {
            return null;
        }

        //assign ID
        if (patient.ID <= 0)
        {
            //adding new patient
            int maxID = -1;
            if (FakeDatabase.Patients.Any())
            {
                maxID = FakeDatabase.Patients.Select(p => p?.ID ?? -1).Max();
            }
            else
            {
                maxID = 0;
            }
            patient.ID = ++maxID;
            FakeDatabase.Patients.Add(patient);
        }
        else
        {
            var patientToEdit = FakeDatabase.Patients.FirstOrDefault(p => (p?.ID ?? 0) == patient.ID);
            if (patientToEdit != null)
            {
                var index = FakeDatabase.Patients.IndexOf(patientToEdit);
                FakeDatabase.Patients.RemoveAt(index);
                FakeDatabase.Patients.Insert(index, patient);
            }
        }

        //return added patient or patient to update
        return patient;
    }
}
