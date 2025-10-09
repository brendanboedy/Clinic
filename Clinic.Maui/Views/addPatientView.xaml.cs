using Clinic.Library.Models;
using Clinic.Library.Services;
using Clinic.Maui.ViewModels;

namespace Clinic.Maui.Views;

public partial class AddPatientView : ContentPage
{
	public AddPatientView()
	{
		InitializeComponent();
	}

	private void cancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
    }

	private void addClicked(object sender, EventArgs e)
	{
		//add the patient
		PatientServiceProxy.Current.Add(BindingContext as Patient);

		//go back to patient view page
		Shell.Current.GoToAsync("//Patients");
    }

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		BindingContext = new Patient();
    }
}