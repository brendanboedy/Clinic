using System;
using Clinic.Library.Services;

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
    public Physician() { }
    
    public Physician(int theID)
    {
        var physicianToCopy = PhysicianServiceProxy.Current.PhysicianList.FirstOrDefault(p => (p?.ID ?? 0) == theID);
        if(physicianToCopy != null)
        {
            ID = physicianToCopy.ID;
            Name = physicianToCopy.Name;
            LicenseNumber = physicianToCopy.LicenseNumber;
            Specialty = physicianToCopy.Specialty;
        }
    }
}
