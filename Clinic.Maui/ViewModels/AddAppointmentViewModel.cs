using System;
using Clinic.Library.Models;

namespace Clinic.Maui.ViewModels;

public class AddAppointmentViewModel
{
    public AddAppointmentViewModel()
    {
        Model = new Appointment();
    }
    public AddAppointmentViewModel(Appointment? model)
    {
        Model = model;
    }

    public Appointment? Model { get; set; }
}
