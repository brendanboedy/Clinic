using System;
using Clinic.Library.Models;

namespace Clinic.API.Database;

public static class FakeDatabase
{
    public static List<Patient> Patients = new List<Patient>
    {
        new Patient{Name = "First", DateOfBirth = new DateTime(2005, 5, 26), ID = 1},
        new Patient{Name = "Second", DateOfBirth = new DateTime(2005, 5, 26), ID = 2},
        new Patient{Name = "Third", DateOfBirth = new DateTime(2005, 5, 26), ID = 3}
    };
    public static List<Physician> Physicians = new List<Physician>
    {
        new Physician{Name = "Doctor A", LicenseNumber = 0, Specialty = "Cardiology", ID = 1},
        new Physician{Name = "Doctor B", LicenseNumber = 0, Specialty = "Neurology", ID = 2},
        new Physician{Name = "Doctor C", LicenseNumber = 0, Specialty = "Pediatrics", ID = 3}
    };
    public static List<Appointment> Appointments = new List<Appointment>
    {

    };
}
