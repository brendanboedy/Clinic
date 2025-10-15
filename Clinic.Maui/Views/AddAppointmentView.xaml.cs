using Clinic.Library.Models;
using Clinic.Library.Services;

namespace Clinic.Maui.Views;

[QueryProperty(nameof(PatientID), "PatientID")]
[QueryProperty(nameof(PhysicianID), "PhysicianID")]
[QueryProperty(nameof(AppointmentID), "AppointmentID")]
public partial class AddAppointmentView : ContentPage
{
	public AddAppointmentView()
	{
		InitializeComponent();
	}
	public int PatientID { get; set; }
	public int PhysicianID { get; set; }
	public int AppointmentID { get; set; }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
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

}