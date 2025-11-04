using Clinic.Library.Models;
using Clinic.Library.Services;
using Clinic.Maui.ViewModels;

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
		// Reset first to force a fresh rebind on a reused ShellContent page
		BindingContext = null;

		int TryGetInt(string key)
			=> query.TryGetValue(key, out var raw) && int.TryParse(raw?.ToString(), out var val) ? val : 0;
		
		AppointmentID = TryGetInt("AppointmentID");
		PatientID     = TryGetInt("PatientID");
		PhysicianID   = TryGetInt("PhysicianID");

		// EDIT: when AppointmentID > 0 we don't need patient/physician ids
		if (AppointmentID > 0)
		{
			BindingContext = new Appointment(AppointmentID);
			return;
		}

		// NEW: AppointmentID == 0 -> we need patient/physician (but tolerate missing)
		BindingContext = new Appointment(PatientID, PhysicianID);

	}
}