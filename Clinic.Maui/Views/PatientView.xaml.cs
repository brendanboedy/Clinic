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
}