using System;
using System.Windows.Input;
using Clinic.Library.Models;
using Clinic.Library.Services;

namespace Clinic.Maui.ViewModels;

public class AddPatientViewModel
{
    public AddPatientViewModel()
    {
        Model = new Patient();
        SetUpCommands();
    }

    public AddPatientViewModel(Patient? model)
    {
        Model = model;
        SetUpCommands();
    }

    public Patient? Model { get; set; }

    //command properties for inline buttons and binding
    public ICommand? DeleteCommand { get; set; }
    public ICommand? EditCommand { get; set; }
    private void SetUpCommands()
    {
        DeleteCommand = new Command(DoDelete);
        EditCommand = new Command((apvm) => DoEdit(apvm as AddPatientViewModel));

    }

    //command funcitonality for delete inline button
    private void DoDelete()
    {
        if(Model?.ID > 0)
        {
            PatientServiceProxy.Current.Delete(Model.ID);
            Shell.Current.GoToAsync("//Patients");
        }
    }
    //command funcitonality for edit inline button
    private void DoEdit(AddPatientViewModel? apvm)
    {
        if (apvm == null)
        {
            return;
        }
        //get selected ID and pass to AddPatients view
        var selectedID = apvm?.Model?.ID ?? 0;
        Shell.Current.GoToAsync($"//AddPatient?PatientID={selectedID}");
    }
}
