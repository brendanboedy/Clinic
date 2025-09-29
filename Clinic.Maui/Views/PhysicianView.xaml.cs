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
}