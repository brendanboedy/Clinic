using System;
using System.Windows.Input;
using Clinic.Library.Models;
using Clinic.Library.Services;

namespace Clinic.Maui.ViewModels;

public class AddAppointmentViewModel
{
    public AddAppointmentViewModel()
    {
        Model = new Appointment();
        SetUpCommands();
    }
    public AddAppointmentViewModel(Appointment? model)
    {
        Model = model;
        SetUpCommands();
    }

    public Appointment? Model { get; set; }

    //command properties for inline buttons and binding
    public ICommand? DeleteCommand { get; set; }
    public ICommand? EditCommand { get; set; }
    private void SetUpCommands()
    {
        DeleteCommand = new Command(DoDelete);
        EditCommand = new Command(async (aavm) => await DoEdit(aavm as AddAppointmentViewModel));

    }

    //command funcitonality for delete inline button
    private void DoDelete()
    {
        if(Model?.ID > 0)
        {
            AppointmentServiceProxy.Current.Delete(Model.ID);
            Shell.Current.GoToAsync("//Appointments");
        }
    }
    //command funcitonality for edit inline button
    private async Task DoEdit(AddAppointmentViewModel? aavm)
    {
        if (aavm == null)
        {
            return;
        }
        //get selected ID and pass to AddPatients view
        var aptID = aavm?.Model?.ID ?? 0;
        var route = $"//AddAppointment?AppointmentID={aptID}&_nonce={Guid.NewGuid()}";
        await Shell.Current.GoToAsync(route);
    }
}
