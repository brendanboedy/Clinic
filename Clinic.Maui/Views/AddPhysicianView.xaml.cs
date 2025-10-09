using Clinic.Library.Models;
using Clinic.Library.Services;

namespace Clinic.Maui.Views;

[QueryProperty(nameof(PhysicianID), "PhysicianID")]

public partial class AddPhysicianView : ContentPage
{
	public int PhysicianID { get; set; }
	public AddPhysicianView()
	{
		InitializeComponent();
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		if (PhysicianID == 0)
		{
			BindingContext = new Physician();
		}
        else
        {
			BindingContext = new Physician(PhysicianID);
        }
	}

	private void addClicked(object sender, EventArgs e)
	{
		//add physician to list
		PhysicianServiceProxy.Current.AddOrUpdate(BindingContext as Physician);

		//go back to physician view
		Shell.Current.GoToAsync("//Physicians");
    }

	private void cancelClicked(object sender, EventArgs e)
	{
		//go back to physician view
		Shell.Current.GoToAsync("//Physicians");
    }
}