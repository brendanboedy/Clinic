using System;

namespace Clinic.Library.Models;

public class Patient
{
    // Patient Properties
    public int ID { get; set; }
    public string Name { get; set; }
    public string? Race { get; set; }
    public string? Gender { get; set; }
    public string? Address { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Notes { get; set; }

    //ToString Override
    public override string ToString()
    {
        //Only display properties that have values
        string thePatient = $"{ID}. {Name} - DOB: {DateOfBirth:MM/dd/yyyy}"
            + (Address != null ? $"\n\tAddress: {Address}" : "")
            + (Race != null ? $"\n\tRace: {Race}" : "")
            + (Gender != null ? $"\n\tGender: {Gender}" : "")
            + (Notes != null ? $"\n\tNotes: {Notes}\n" : "");
        return thePatient;
    }

    //Constructor
    public Patient(string name, DateTime birthDate)
    {
        Name = name;
        DateOfBirth = birthDate;
    }
}
