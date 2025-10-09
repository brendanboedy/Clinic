using System;

namespace Clinic.Library.Models;

public class Physician
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public int LicenseNumber { get; set; }
    public string? Specialty { get; set; }

    //ToString Override
    public override string ToString()
    {
        string thePhysician = $"{ID}. {Name} - License Number: {LicenseNumber}"
            + (Specialty != null ? $"\n\tSpecialty: {Specialty}\n" : "");
        return thePhysician;
    }

    public string Display
    {
        get
        {
            return ToString();
        }
    }

    //Constructor
    public Physician(string name, int licenseNumber)
    {
        Name = name;
        LicenseNumber = licenseNumber;
    }
    public Physician(){}
}
