using Clinic.Library.Models;
using Clinic.Library.Services;
using Clinic.Maui.ViewModels;

namespace Clinic.Maui.Views;

[QueryProperty(nameof(PhysicianID), "PhysicianID")]

public partial class AddPhysicianView : ContentPage
{
	public int PhysicianID { get; set; }
	public AddPhysicianView()
	{
		InitializeComponent();
	}

	private void addClicked(object sender, EventArgs e)
	{
		var response = (BindingContext as AddPhysicianViewModel)?.AddNewPhysician();
    }

	private void cancelClicked(object sender, EventArgs e)
	{
		//go back to physician view
		Shell.Current.GoToAsync("//Physicians");
    }

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		if (PhysicianID == 0)
		{
			BindingContext = new AddPhysicianViewModel(new Physician());
		}
        else
        {
			BindingContext = new AddPhysicianViewModel(new Physician(PhysicianID));
        }
	}
}