using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Clinic.Library.DTO;
using Clinic.Library.Models;
using Clinic.Library.Services;

namespace Clinic.Maui.ViewModels;

public class AddAppointmentViewModel : INotifyPropertyChanged
{
    public AddAppointmentViewModel()
    {
        Model = new AppointmentDTO();
        ErrorLabelVisibility = false;
        SetUpCommands();
    }
    public AddAppointmentViewModel(AppointmentDTO? model)
    {
        Model = model;
        ErrorLabelVisibility = false;
        SetUpCommands();
    }

    //property for view model's appointment
    public AppointmentDTO? Model { get; set; }

    //property for visibility of error message on AddAppointmentView
    private bool errorLabelVisibility;
    public bool ErrorLabelVisibility
    {
        get { return errorLabelVisibility; }
        set
        {
            if (errorLabelVisibility != value)
            {
                errorLabelVisibility = value;
                NotifyPropertyChanged();
            }
        }
    }

    //async function to add new appointment to server
    public async Task<bool> AddNewAppointment()
    {
        try{
            //check to make sure date/time is valid
            if (CheckConstraints() == false)
            {
                return false;
            }
            //add the appointment
            await AppointmentServiceProxy.Current.AddOrUpdate(Model);
            //go back to Appointment View
            await Shell.Current.GoToAsync("//Appointments");
            return true;
        } catch(Exception e)
        {
            return false;
        }
    }

    //function to check appointment characteristics
    //checking valid date and time
    public bool CheckConstraints()
    {
        //make sure model and its necessary characteristics not null
        if (Model == null || Model.AppointmentDate == null || Model.AppointmentStartTime == null)
        {
            //make error label visibility true
            ErrorLabelVisibility = true;
            return false;
        }
        //make sure day of week M-F
        else if (Model.AppointmentDate.Value.DayOfWeek == DayOfWeek.Saturday ||
                Model.AppointmentDate.Value.DayOfWeek == DayOfWeek.Sunday)
        {
            //invalid date, make error label visible
            ErrorLabelVisibility = true;
            return false;
        }
        //make sure appointment time between 9-5
        else if (!IsWithinBusinessHours())
        {
            //invalid time return false
            ErrorLabelVisibility = true;
            return false;
        }
        //make sure appointments not conflicting
        else if (!noOtherAppointments())
        {
            //conflicting with existing appointment return false
            ErrorLabelVisibility = true;
            return false;
        }
        //passed constraints return true
        ErrorLabelVisibility = false;
        return true;
    }

    //method to check business hours
    public bool IsWithinBusinessHours()
    {
        TimeOnly start = new(9, 0);
        TimeOnly end = new(17, 0);
        //nullable ok - nested method - will only be called if AppointmentStartTime != null
        var time = Model.AppointmentStartTime.Value;
        return time >= start && time <= end;
    }

    public bool noOtherAppointments()
    {
        var appointments = AppointmentServiceProxy.Current.AppointmentList;
        //check to see if any other appointments exist on the same date
        foreach (var appointment in appointments)
        {
            //if updating appointment, skip check against itself
            if ((appointment?.ID ?? 0) == Model.ID)
            {
                continue;
            }
            
            //new appointment on same date as existing appointment
            //check to see if new appointment start time is between existing appointment start and end time
            //and if the physician IDs are the same
            if (appointment.AppointmentDate == Model.AppointmentDate && Conflicting(Model, appointment))
            {
                //conflicting dates, times, physicians
                return false;
            }
        }
        //no conflicting appointments found
        return true;
    }
    //method to check for existing appointments conflicting
    public bool Conflicting(AppointmentDTO a, AppointmentDTO b)
    {
        //Astart < Bend && Bstart < Aend
        if (a.AssignedPhysician.ID == b.AssignedPhysician.ID && 
            a.AppointmentStartTime < b.AppointmentEndTime &&
            b.AppointmentStartTime < a.AppointmentEndTime){
            //overlapping appointments returnt rue
            return true;
        }
        //non conflicting return false
        return false;
    }

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
        //get selected ID and pass to AddAppointment view
        var aptID = aavm?.Model?.ID ?? 0;
        var route = $"//AddAppointment?AppointmentID={aptID}&_nonce={Guid.NewGuid()}";
        await Shell.Current.GoToAsync(route);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
