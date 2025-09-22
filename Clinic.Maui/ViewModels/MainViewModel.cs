using System;
using Clinic.Library.Models;

namespace Clinic.Maui.ViewModels;

public class MainViewModel
{
    public List<Appointment> appointments
    {
        get
        {
            return new List<Appointment>
            {
                //need a fake appointment here
            };
        }
    }
}
