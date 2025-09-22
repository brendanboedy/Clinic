using Clinic.Maui.ViewModels;

namespace Clinic.Maui;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

		//bind main page to main view model
		//main view model holds data necessary for main page
		BindingContext = new MainViewModel();
	}

}

