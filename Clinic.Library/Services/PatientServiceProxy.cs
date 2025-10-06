using System;
using System.Data;
using Clinic.Library.Models;

namespace Clinic.Library.Services;

public class PatientServiceProxy
{
    public List<Patient> patientList { get; set; }

    //private proxy constructor
    private PatientServiceProxy()
    {
        patientList = new List<Patient>();
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

    //public getter to access private list
    public List<Patient> PatientList
    {
        get { return patientList; }
    }

    public Patient? Add(Patient patient)
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
            if (patientList.Any())
            {
                maxID = patientList.Select(p => p?.ID ?? -1).Max();
            }
            else
            {
                maxID = 0;
            }
            patient.ID = ++maxID;
            patientList.Add(patient);
        }

        //return added patient
        return patient;
    }

    public Patient? Delete(int patientID)
    {
        //find existing patient
        var existingPatient = patientList.FirstOrDefault(p => p.ID == patientID);
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
        return patientList.FirstOrDefault(p => p.ID == ID);
    }

}
