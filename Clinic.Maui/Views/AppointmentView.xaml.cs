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

    private void editClicked(object sender, EventArgs e)
    {
    }

    private void deleteClicked(object sender, EventArgs e)
    {
    }

    private void backClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as AppointmentViewModel)?.Refresh();
    }
}