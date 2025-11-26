using Clinic.Library.Models;
using Clinic.Library.Services;
using Clinic.Maui.ViewModels;

namespace Clinic.Maui.Views;

[QueryProperty(nameof(PatientID), "PatientID")]
public partial class AddPatientView : ContentPage
{
	public int PatientID{ get; set; }
	public AddPatientView()
	{
		InitializeComponent();
	}

	private void cancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//Patients");
	}

	private void addClicked(object sender, EventArgs e)
	{
		var response = (BindingContext as AddPatientViewModel)?.AddNewPatient();
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		if (PatientID == 0)
		{
			BindingContext = new AddPatientViewModel(new Patient());
		}
        else
        {
			BindingContext = new AddPatientViewModel(new Patient(PatientID));
        }
	}
}