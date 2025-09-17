using Clinic.Library.Models;
using Microsoft.VisualBasic.FileIO;
namespace assignmentOne_COP4870;

class Program
{
    static void Main(string[] args)
    {
        //Lists for each created patient, physician, and appointment
        List<Patient> patients = new List<Patient>();
        List<Physician> physicians = new List<Physician>();
        List<Appointment> appointments = new List<Appointment>();

        //main menu loop for user input
        bool cont = true;
        do
        {
            //user choices menu
            Console.WriteLine("\nPlease select the desired action:");
            Console.WriteLine("Create a Patient (CP)");
            Console.WriteLine("Update a Patient (UP)");
            Console.WriteLine("Delete a Patient (DP)");
            Console.WriteLine("Create a Physician (CPh)");
            Console.WriteLine("Update a Physician (UPh)");
            Console.WriteLine("Delete a Physician (DPh)");
            Console.WriteLine("Create an Appointment (CA)");
            Console.WriteLine("Update an Appointment (UA)");
            Console.WriteLine("Delete an Appointment (DA)");
            Console.WriteLine("View all Patients (VP)");
            Console.WriteLine("View all Physicians (VPh)");
            Console.WriteLine("View all Appointments (VA)");
            Console.WriteLine("Exit (E)");
            Console.Write("Enter your choice: ");

            //read user choice and make sure string - case insensitive
            var choice = Console.ReadLine() ?? "";
            choice = choice.ToUpper();


            //switch statement to handle user input
            switch (choice)
            {
                //create patient
                case "CP":
                    Console.WriteLine("\nEnter Patient Name:");
                    var name = Console.ReadLine() ?? "";
                    Console.WriteLine("Enter Patient Birth Date (yyyy-mm-dd):");
                    DateTime birthDate;
                    //check to make sure date format is correct/accepted
                    while (!DateTime.TryParse(Console.ReadLine(), out birthDate))
                    {
                        //have user re enter birth date if format is incorrect
                        Console.WriteLine("Invalid date format. Please enter the date in yyyy-mm-dd format:");
                    }
                    //create new patient
                    Patient newPatient = new Patient(name, birthDate);

                    //update ID for new patient
                    var maxID = -1;
                    if (patients.Any()) //check to make sure list not empty
                    {
                        maxID = patients.Select(p => p?.ID ?? -1).Max();
                    }
                    else
                    {
                        maxID = 0;
                    }
                    newPatient.ID = ++maxID;
                    patients.Add(newPatient);
                    Console.WriteLine("Patient created successfully.");
                    break;
                //update patient
                case "UP":
                    //check to make sure patients exist to update
                    if (patients.Any())
                    {
                        Console.WriteLine("\nPerforming Patient Update:");
                        int patientID = getPatientID(patients);
                        Patient patientToUpdate = patients.First(p => p.ID == patientID);
                        //call method to update patient information
                        UpdatePatientMenu(patientToUpdate);
                    }
                    else
                    {
                        Console.WriteLine("\nNo patients available to update.");
                    }
                    break;
                //delete patient
                case "DP":
                    //check to make sure patients exist to delete
                    if (patients.Any())
                    {
                        Console.WriteLine("\nPerforming Patient Deletetion:");
                        int patientID = getPatientID(patients);
                        patients.Remove(patients.First(p => p.ID == patientID));
                        Console.WriteLine("Patient deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("\nNo patients available to delete.");
                    }
                    break;
                //create physician
                case "CPH":
                    Console.WriteLine("\nEnter Physician Name:");
                    var phyName = Console.ReadLine() ?? "";
                    Console.WriteLine("Enter Physician License Number:");
                    int licenseNumber;
                    //check to make sure license number is valid integer
                    while (!int.TryParse(Console.ReadLine(), out licenseNumber))
                    {
                        //license number must be valid integer
                        Console.WriteLine("Invalid License Number. Please enter a valid Integer:");
                    }
                    Physician newPhysician = new Physician(phyName, licenseNumber);
                    //update ID for new physician
                    var maxPhyID = -1;
                    if (physicians.Any()) //check to make sure list not empty
                    {
                        maxPhyID = physicians.Select(p => p?.ID ?? -1).Max();
                    }
                    else
                    {
                        maxPhyID = 0;
                    }
                    newPhysician.ID = ++maxPhyID;
                    physicians.Add(newPhysician);
                    Console.WriteLine("Physician created successfully.");
                    break;
                //update physician
                case "UPH":
                    if (physicians.Any())
                    {
                        Console.WriteLine("\nPerforming Physician Update:");

                        //get physician ID to update
                        int physicianID = getPhysicianID(physicians);

                        //assign physician to update
                        Physician physicianToUpdate = physicians.First(phy => phy.ID == physicianID);
                        Console.WriteLine($"Please enter {physicianToUpdate.Name}'s specialty: ");

                        //update physician specialty
                        physicianToUpdate.Specialty = Console.ReadLine();
                        Console.WriteLine("Physician specialty updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("\nNo physicians available to update.");
                    }
                    break;
                //delete physician
                case "DPH":
                    if (physicians.Any())
                    {
                        Console.WriteLine("\nPerforming Physician Deletetion:");
                        //get physician ID to delete
                        int physicianID = getPhysicianID(physicians);
                        physicians.Remove(physicians.First(phy => phy.ID == physicianID));
                        Console.WriteLine("Physician deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("\nNo physicians available to delete.");
                    }
                    break;
                //create appointment
                case "CA":
                    if (patients.Any() && physicians.Any())
                    {
                        Console.WriteLine("\nCreating New Appointment:");

                        //obtain appt date
                        Console.WriteLine("Enter Appointment Date (yyyy-mm-dd):");
                        DateOnly appointmentDate;
                        //check to make sure date format is correct/accepted
                        while (!DateOnly.TryParse(Console.ReadLine(), out appointmentDate))
                        {
                            //have user re enter appointment date if format is incorrect
                            Console.WriteLine("Invalid date format. Please enter the date in yyyy-mm-dd format:");
                        }
                        //check to make sure date is M-F
                        while (appointmentDate.DayOfWeek == DayOfWeek.Saturday || appointmentDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            Console.WriteLine("Appointments can only be scheduled Monday through Friday. Please enter a valid date:");
                            while (!DateOnly.TryParse(Console.ReadLine(), out appointmentDate))
                            {
                                //have user re enter appointment date if format is incorrect
                                Console.WriteLine("Invalid date format. Please enter the date in yyyy-mm-dd format:");
                            }
                        }

                        //obtain appt start time
                        Console.WriteLine("Enter Appointment Start Time (HH:MM, 24-hour format):");
                        TimeOnly appointmentStartTime;
                        //check to make sure time format is correct
                        while (!TimeOnly.TryParse(Console.ReadLine(), out appointmentStartTime))
                        {
                            //have user re enter appointment start time if format is incorrect
                            Console.WriteLine("Invalid time format. Please enter the time in HH:MM, 24-hour format:");
                        }
                        //check to make sure time is within business hours 8am-5pm
                        while (!(appointmentStartTime.Hour >= 8 && appointmentStartTime.Hour < 17))
                        {
                            Console.WriteLine("Appointments can only be scheduled between 8:00 and 17:00. Please enter a valid time:");
                            while (!TimeOnly.TryParse(Console.ReadLine(), out appointmentStartTime))
                            {
                                //have user re enter appointment start time if format is incorrect
                                Console.WriteLine("Invalid time format. Please enter the time in HH:MM, 24-hour format:");
                            }
                        }

                        //grab patient and physician IDs for appointment creation
                        Console.WriteLine("Select Physician for Appointment:");
                        int physicianID = getPhysicianID(physicians);

                        Console.WriteLine("Select Patient for Appointment:");
                        int patientID = getPatientID(patients);

                        //check to make sure physician does not have another appointment at the same time
                        Appointment tempAppointment = new Appointment(appointmentDate, appointmentStartTime, patientID, physicianID);
                        if (noOtherAppointments(appointments, tempAppointment))
                        {
                            //create ID for current appointment
                            var maxApptID = -1;
                            if (appointments.Any()) //check to make sure list not empty
                            {
                                maxApptID = appointments.Select(a => a?.ID ?? -1).Max();
                            }
                            else
                            {
                                maxApptID = 0;
                            }
                            tempAppointment.ID = ++maxApptID;
                            appointments.Add(tempAppointment);
                            Console.WriteLine("Appointment created successfully.");
                        }
                        else
                        {
                            Console.WriteLine("The selected physician has another appointment at the chosen time. Please select a different time or physician.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nAppointments require at least one patient and one physician to be created.");
                    }
                    break;
                //update appointment
                case "UA":
                    //update appointment
                    if (appointments.Any())
                    {
                        foreach (var appointment in appointments)
                        {
                            Console.WriteLine(appointment);
                        }
                        Console.WriteLine("\nEnter Appointment ID to Update:");
                        int appointmentID;
                        //check to make sure user input is valid
                        while (!int.TryParse(Console.ReadLine(), out appointmentID) || !appointments.Any(a => a.ID == appointmentID))
                        {
                            Console.WriteLine("Invalid Appointment ID. Please enter a valid Appointment ID:");
                        }
                        Appointment appointmentToUpdate = appointments.First(a => a.ID == appointmentID);
                        UpdateAppointmentMenu(appointments, appointmentToUpdate);
                    }
                    else
                    {
                        Console.WriteLine("\nNo appointments available to update.");
                    }
                    break;
                //delete appointment
                case "DA":
                    if (appointments.Any())
                    {
                        foreach (var appointment in appointments)
                        {
                            Console.WriteLine(appointment);
                        }
                        Console.WriteLine("\nEnter Appointment ID to Delete:");
                        int appointmentID;
                        //check to make sure user input is valid
                        while (!int.TryParse(Console.ReadLine(), out appointmentID) || !appointments.Any(a => a.ID == appointmentID))
                        {
                            Console.WriteLine("Invalid Appointment ID. Please enter a valid Appointment ID:");
                        }
                        appointments.Remove(appointments.First(a => a.ID == appointmentID));
                        Console.WriteLine("Appointment deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("\nNo appointments available to delete.");
                    }
                    break;
                //view all patients
                case "VP":
                    if (patients.Any())
                    {
                        Console.WriteLine("\nListing All Patients:");
                        foreach (var patient in patients)
                        {
                            Console.WriteLine(patient);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nNo patients available to view.");
                    }
                    break;
                //view all physicians
                case "VPH":
                    if (physicians.Any())
                    {
                        Console.WriteLine("\nListing All Physicians:");
                        foreach (var physician in physicians)
                        {
                            Console.WriteLine(physician);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nNo physicians available to view.");
                    }
                    break;
                //view all appointments
                case "VA":
                    if (appointments.Any())
                    {
                        Console.WriteLine("\nListing All Appointments:");
                        foreach (var appointment in appointments)
                        {
                            Console.WriteLine(appointment);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nNo appointments available to view.");
                    }
                    break;
                //exit program
                case "E":
                    cont = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

        } while (cont);
    }

    //Method to update patient information
    private static void UpdatePatientMenu(Patient patientToUpdate)
    {
        var cont = true;
        do
        {
            //present user with update 
            Console.WriteLine("\nSelect the information you would like to update:");
            Console.WriteLine("Update Patient Race (UR)");
            Console.WriteLine("Update Patient Gender (UG)");
            Console.WriteLine("Update Patient Address (UA)");
            Console.WriteLine("Update Patient Notes (UN)");
            Console.WriteLine("Return to Main Menu (RM)");
            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine() ?? "";
            choice = choice.ToUpper();

            //corresponding response with switch statement
            switch (choice)
            {
                //update race
                case "UR":
                    Console.WriteLine($"\nPlease enter {patientToUpdate.Name}'s race: ");
                    patientToUpdate.Race = Console.ReadLine();
                    Console.WriteLine("Patient race updated successfully.");
                    break;
                //update gender
                case "UG":
                    Console.WriteLine($"\nPlease enter {patientToUpdate.Name}'s gender: ");
                    patientToUpdate.Gender = Console.ReadLine();
                    Console.WriteLine("Patient gender updated successfully.");
                    break;
                //update address
                case "UA":
                    Console.WriteLine($"\nPlease enter {patientToUpdate.Name}'s address: ");
                    patientToUpdate.Address = Console.ReadLine();
                    Console.WriteLine("Patient address updated successfully.");
                    break;
                //update notes
                case "UN":
                    Console.WriteLine($"\nPlease enter notes for {patientToUpdate.Name}: ");
                    patientToUpdate.Notes = Console.ReadLine();
                    Console.WriteLine("Patient notes updated successfully.");
                    break;
                //return to main menu
                case "RM":
                    cont = false;
                    break;
                default:
                    Console.WriteLine("\nInvalid choice. Please try again.");
                    break;
            }
        } while (cont);
    }

    //method to grab ID of patient
    private static int getPatientID(List<Patient> patients)
    {
        Console.WriteLine("\nListing Current Patients:");
        foreach (var patient in patients)
        {
            Console.WriteLine($"{patient.ID}. {patient.Name}");
        }
        Console.WriteLine("\nEnter Patient ID:");
        int patientID;
        //check to make sure user input is valid
        while (!int.TryParse(Console.ReadLine(), out patientID) || !patients.Any(p => p.ID == patientID))
        {
            Console.WriteLine("Invalid Patient ID. Please enter a valid Patient ID:");
        }
        return patientID;
    }

    //method to grab ID of physician
    private static int getPhysicianID(List<Physician> physicians)
    {
        Console.WriteLine("\nListing Current Physicians:");
        foreach (var physician in physicians)
        {
            Console.WriteLine($"{physician.ID}. {physician.Name}");
        }
        Console.WriteLine("\nEnter Physician ID:");
        int physicianID;
        //check to make sure user input is valid
        while (!int.TryParse(Console.ReadLine(), out physicianID) || !physicians.Any(phy => phy.ID == physicianID))
        {
            Console.WriteLine("Invalid Physician ID. Please enter a valid Physician ID:");
        }
        return physicianID;
    }

    //method to check if physician has another appointment at the same time
    private static bool noOtherAppointments(List<Appointment> appointments, Appointment newAppointment)
    {
        //check to see if any other appointments exist on the same date
        foreach (var appointment in appointments)
        {
            //if updating appointment, skip check against itself
            if (appointment.ID == newAppointment.ID)
            {
                continue;
            }
            if (appointment.AppointmentDate == newAppointment.AppointmentDate)
            {
                //new appointment on same date as existing appointment
                //check to see if new appointment start time is between existing appointment start and end time
                //and if the physician IDs are the same
                if (conflicting(appointment, newAppointment))
                {
                    //conflicting appt
                    return false;
                }
            }
        }
        //no conflicting appointments found
        return true;
    }
    //returns true if the appointments are conflicting - helper function for noOtherAppointments
    private static bool conflicting(Appointment otherAppointment, Appointment newAppointment)
    {
        //Astart < Bend && Bstart < Aend
        if (newAppointment.PhysicianID == otherAppointment.PhysicianID
            && newAppointment.AppointmentStartTime <= otherAppointment.AppointmentEndTime
            && otherAppointment.AppointmentStartTime <= newAppointment.AppointmentEndTime)
        {
            return true;
        }
        return false;
    }
    private static void UpdateAppointmentMenu(List<Appointment> appointments, Appointment appointmentToUpdate)
    {
        Console.WriteLine("Enter new Appointment Date (yyyy-mm-dd):");
        DateOnly newAppointmentDate;
        //check to make sure date format is correct/accepted
        while (!DateOnly.TryParse(Console.ReadLine(), out newAppointmentDate))
        {
            //have user re enter appointment date if format is incorrect
            Console.WriteLine("Invalid date format. Please enter the date in yyyy-mm-dd format:");
        }
        //check to make sure date is M-F
        while (newAppointmentDate.DayOfWeek == DayOfWeek.Saturday || newAppointmentDate.DayOfWeek == DayOfWeek.Sunday)
        {
            Console.WriteLine("Appointments can only be scheduled Monday through Friday. Please enter a valid date:");
            while (!DateOnly.TryParse(Console.ReadLine(), out newAppointmentDate))
            {
                //have user re enter appointment date if format is incorrect
                Console.WriteLine("Invalid date format. Please enter the date in yyyy-mm-dd format:");
            }
        }
        //obtain appt start time
        Console.WriteLine("Enter new Appointment Start Time (HH:MM, 24-hour format):");
        TimeOnly newAppointmentStartTime;
        //check to make sure time format is correct
        while (!TimeOnly.TryParse(Console.ReadLine(), out newAppointmentStartTime))
        {
            //have user re enter appointment start time if format is incorrect
            Console.WriteLine("Invalid time format. Please enter the time in HH:MM, 24-hour format:");
        }
        //check to make sure time is within business hours 8am-5pm
        while (!(newAppointmentStartTime.Hour >= 8 && newAppointmentStartTime.Hour < 17))
        {
            Console.WriteLine("Appointments can only be scheduled between 8:00 and 17:00. Please enter a valid time:");
            while (!TimeOnly.TryParse(Console.ReadLine(), out newAppointmentStartTime))
            {
                //have user re enter appointment start time if format is incorrect
                Console.WriteLine("Invalid time format. Please enter the time in HH:MM, 24-hour format:");
            }
        }
        //create temp appointment to check for conflicts before updating appointmentToUpdate
        Appointment appointmentToCheck = new Appointment(newAppointmentDate, newAppointmentStartTime, appointmentToUpdate.PatientID, appointmentToUpdate.PhysicianID)
        {
            ID = appointmentToUpdate.ID
        };
        //check to make sure physician does not have another appointment at the same time
        if (noOtherAppointments(appointments, appointmentToCheck))
        {
            appointmentToUpdate.AppointmentDate = newAppointmentDate;
            appointmentToUpdate.AppointmentStartTime = newAppointmentStartTime;
            appointmentToUpdate.AppointmentEndTime = newAppointmentStartTime.AddHours(1); //Default to 1 hour appointments
            Console.WriteLine("Appointment updated successfully.");
        }
        else
        {
            Console.WriteLine("The selected physician has another appointment at the chosen time. Please select a different time or physician.");
        }
    }
}
