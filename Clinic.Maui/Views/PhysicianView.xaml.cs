using Clinic.Maui.ViewModels;

namespace Clinic.Maui.Views;

public partial class PhysicianView : ContentPage
{
	public PhysicianView()
	{
		InitializeComponent();
		//binding physician content page to physician view model
		BindingContext = new PhysicianViewModel();
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		(BindingContext as PhysicianViewModel)?.Refresh();
    }

	private void addClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//AddPhysician?PhysicianID=0");
    }

	private void backClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
    }

    private void editClicked(object sender, EventArgs e)
	{
		var selectedID = (BindingContext as PhysicianViewModel)?.SelectedPhysician?.ID ?? 0;
		Shell.Current.GoToAsync($"//AddPhysician?PhysicianID={selectedID}");
    }

    private void deleteClicked(object sender, EventArgs e)
	{
		(BindingContext as PhysicianViewModel)?.Delete();
    }
}