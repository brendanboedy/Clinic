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

    private void appointmentsClicked(object sender, EventArgs e)
    {
    }

    private void patientsClicked(object sender, EventArgs e)
    {
    }

    private void physiciansClicked(object sender, EventArgs e)
    {
    }

}

