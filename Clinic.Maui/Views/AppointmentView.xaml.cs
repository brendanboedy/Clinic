using Clinic.Maui.ViewModels;

namespace Clinic.Maui.Views;

public partial class AppointmentView : ContentPage
{
	public AppointmentView()
	{
		InitializeComponent();
		//binding appointment content page to appointment view model
		BindingContext = new AppointmentViewModel();
	}
}