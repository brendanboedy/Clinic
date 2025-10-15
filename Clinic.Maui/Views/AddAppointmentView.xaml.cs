using Clinic.Library.Models;
using Clinic.Library.Services;

namespace Clinic.Maui.Views;

[QueryProperty(nameof(PatientID), "PatientID")]
[QueryProperty(nameof(PhysicianID), "PhysicianID")]

public partial class AddAppointmentView : ContentPage
{
	public AddAppointmentView()
	{
		InitializeComponent();
	}
	public int PatientID { get; set; }
	public int PhysicianID { get; set; }
	public int AppointmentID { get; set; }
}