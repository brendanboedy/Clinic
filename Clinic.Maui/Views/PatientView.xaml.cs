using Clinic.Maui.ViewModels;

namespace Clinic.Maui.Views;

public partial class PatientView : ContentPage
{
	public PatientView()
	{
		InitializeComponent();
		//binding patient content page to patient view model
		BindingContext = new PatientViewModel();
	}

	private void clickedAddPatient(object sender, EventArgs e)
	{
		//navigate to AddPatient view
		Shell.Current.GoToAsync("//AddPatient?PatientID=0");
    }

	private void clickedBack(object sender, EventArgs e)
	{
		//navigate back to MainPage
		Shell.Current.GoToAsync("//MainPage");
    }

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		(BindingContext as PatientViewModel)?.Refresh();
    }

    private void deleteClicked(object sender, EventArgs e)
	{
		(BindingContext as PatientViewModel)?.Delete();
    }

    private void editClicked(object sender, EventArgs e)
	{
		var selectedID = (BindingContext as PatientViewModel)?.SelectedPatient?.ID ?? 0;
		Shell.Current.GoToAsync($"//AddPatient?PatientID={selectedID}");
    }
}