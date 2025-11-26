using System;
using System.Windows.Input;
using Clinic.Library.Models;
using Clinic.Library.Services;

namespace Clinic.Maui.ViewModels;

public class AddPhysicianViewModel
{
    public AddPhysicianViewModel()
    {
        Model = new Physician();
        SetUpCommands();
    }
    public AddPhysicianViewModel(Physician? model)
    {
        Model = model;
        SetUpCommands();
    }

    public Physician? Model { get; set; }

    //method to add physician to API
    public async Task<bool> AddNewPhysician()
    {
        try
        {
            //add physician to list
		    await PhysicianServiceProxy.Current.AddOrUpdate(Model);
            //go back to physician view
            await Shell.Current.GoToAsync("//Physicians");
            return true;
        } catch (Exception e)
        {
            return false;
        }
    }
    public ICommand? DeleteCommand { get; set; }
    public ICommand? EditCommand { get; set; }
    private void SetUpCommands()
    {
        DeleteCommand = new Command(DoDelete);
        EditCommand = new Command(apvm => DoEdit(apvm as AddPhysicianViewModel));
    }

    //functionality for Delete command for inline Delete button
    private void DoDelete()
    {
        if (Model?.ID > 0)
        {
            PhysicianServiceProxy.Current.Delete(Model?.ID ?? 0);
            Shell.Current.GoToAsync("//Physicians");
        }
    }
    
    //functionality for Edit command for inline Edit button
    private void DoEdit(AddPhysicianViewModel? apvm)
    {
        //obtain ID and go to AddPhysician view
        var selectedID = apvm?.Model?.ID ?? 0;
        Shell.Current.GoToAsync($"//AddPhysician?PhysicianID={selectedID}");
    }
}
