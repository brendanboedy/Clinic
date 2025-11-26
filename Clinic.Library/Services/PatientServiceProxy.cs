using System;
using System.Data;
using Clinic.Library.Models;
using Clinic.Library.Utilities;
using Newtonsoft.Json;

namespace Clinic.Library.Services;

public class PatientServiceProxy
{
    //proxy list holding all patients
    private List<Patient?> patientList { get; set; }

    //public list to access private
    public List<Patient?> PatientList
    {
        get
        {
            return patientList;
        }
    }

    //private proxy constructor
    private PatientServiceProxy()
    {
        patientList = new List<Patient?>();
        var patientsResponse = new WebRequestHandler().Get("/Patient").Result;
        if (patientsResponse != null)
        {
            patientList = JsonConvert.DeserializeObject<List<Patient?>>(patientsResponse) ?? new List<Patient?>();
        }
    }

    private static PatientServiceProxy? instance;
    private static object instanceLock = new object();

    //lock critical section
    //if instance is null, create new service proxy, otherwise return existing instance
    public static PatientServiceProxy Current
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new PatientServiceProxy();
                }
                return instance;
            }
        }
    }

    public async Task<Patient?> AddOrUpdate(Patient? patient)
    {
        //make sure patient not null
        if (patient == null)
        {
            return null;
        }

        var patientPayload = await new WebRequestHandler().Post("/Patient", patient);
        var patientFromServer = JsonConvert.DeserializeObject<Patient>(patientPayload);

        //assign ID
        if (patient.ID <= 0)
        {
            //adding new patient
            /*int maxID = -1;
            if (patientList.Any())
            {
                maxID = patientList.Select(p => p?.ID ?? -1).Max();
            }
            else
            {
                maxID = 0;
            }
            patient.ID = ++maxID;*/
            patientList.Add(patientFromServer);
        }
        else
        {
            var patientToEdit = PatientList.FirstOrDefault(p => (p?.ID ?? 0) == patient.ID);
            if (patientToEdit != null)
            {
                var index = PatientList.IndexOf(patientToEdit);
                PatientList.RemoveAt(index);
                patientList.Insert(index, patient);
            }
        }

        //return added patient or patient to update
        return patient;
    }

    public Patient? Delete(int patientID)
    {
        //find existing patient
        var existingPatient = patientList.FirstOrDefault(p => p?.ID == patientID);
        if (existingPatient == null) { return null; }

        //remove patient from list
        patientList.Remove(existingPatient);

        //return deleted patient
        return existingPatient;
    }

    public Patient? GetByID(int ID)
    {
        if (ID <= 0)
        {
            return null;
        }
        return patientList.FirstOrDefault(p => p?.ID == ID);
    }

}
