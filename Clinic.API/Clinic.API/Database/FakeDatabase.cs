using System;
using Clinic.Library.Models;

namespace Clinic.API.Database;

public static class FakeDatabase
{
    //list for patients
    public static List<Patient> Patients = new List<Patient>();

    //list for physicians
    public static List<Physician> Physicians = new List<Physician>();

    //list for appointments
    public static List<Appointment> Appointments = new List<Appointment>();
}
