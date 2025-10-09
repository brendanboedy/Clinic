using Clinic.Library.Models;

namespace Clinic.Maui.Views;

public partial class AddPhysicianView : ContentPage
{
	public AddPhysicianView()
	{
		InitializeComponent();
	}

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		BindingContext = new Physician();
    }
}