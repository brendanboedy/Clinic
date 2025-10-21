using Clinic.Library.Models;
using Clinic.Library.Services;

namespace Clinic.Maui.Views;

public partial class AddAppointmentView : ContentPage, IQueryAttributable
{
	public int PatientID { get; set; }
	public int PhysicianID { get; set; }
	public int AppointmentID { get; set; }
	public AddAppointmentView()
	{
		InitializeComponent();
	}

	public string? patientName
	{
		get
		{
			return (BindingContext as Appointment)?.AssignedPatient?.Name;
		}
	}
	
	public string? physicianName
    {
        get
        {
			return (BindingContext as Appointment)?.AssignedPhysician?.Name;
        }
    }

    private void addAppointmentClicked(object sender, EventArgs e)
	{
		//add the appointment
		AppointmentServiceProxy.Current.AddOrUpdate(BindingContext as Appointment);

		//go back to Appointment View
		Shell.Current.GoToAsync("//Appointments");
    }

    private void cancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//Appointments");
    }

	//content page runs this function first for attributes
	public void ApplyQueryAttributes(IDictionary<string, object> query)
	{
		//assign query properties
		PatientID = (int)query["PatientID"];
		PhysicianID = (int)query["PhysicianID"];
		AppointmentID = (int)query["AppointmentID"];

		//create new appointment
		if (AppointmentID == 0)
		{
			BindingContext = new Appointment(PatientID, PhysicianID);
		}
		//update existing appointment
		else
		{
			BindingContext = new Appointment(AppointmentID);
		}
	}
}