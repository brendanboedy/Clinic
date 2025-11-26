using System;
using Clinic.Library.Data;
using Clinic.Library.Models;
using Clinic.Library.Utilities;
using Newtonsoft.Json;

namespace Clinic.Library.Services;

public class AppointmentServiceProxy
{
    //list to hold appointments in memory
    private List<Appointment?> appointmentList { get; set; }

    //private proxy constructor
    private AppointmentServiceProxy()
    {
        appointmentList = new List<Appointment?>();
        var appointmentsResponse = new WebRequestHandler().Get("/Appointment").Result;
        if (appointmentsResponse != null)
        {
            appointmentList = JsonConvert.DeserializeObject<List<Appointment?>>(appointmentsResponse) ?? new List<Appointment?>();
        }
    }

    //singleton instance and lock object
    private static AppointmentServiceProxy? instance;
    private static object instanceLock = new object();

    //static property to get singleton instance
    //lock critical section
    public static AppointmentServiceProxy Current
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new AppointmentServiceProxy();
                }
                return instance;
            }
        }
    }

    //public getter to access private list
    public List<Appointment?> AppointmentList
    {
        get { return appointmentList; }
    }

    public async Task<Appointment?> AddOrUpdate(Appointment? appointment)
    {
        //make sure appointment not null
        if (appointment == null)
        {
            return null;
        }

        //post on API
        var appointmentPayload = await new WebRequestHandler().Post("/Appointment", appointment);
        var appointmentFromServer = JsonConvert.DeserializeObject<Appointment>(appointmentPayload);

        //assign ID for new appointments
        if (appointment.ID <= 0)
        {
            /*int maxID = -1;
            if (appointmentList.Any())
            {
                maxID = appointmentList.Select(a => a?.ID ?? -1).Max();
            }
            else
            {
                maxID = 0;
            }
            appointment.ID = ++maxID;*/
            appointmentList.Add(appointmentFromServer);
        }
        //if appointment already exists, replace w/ new updated appointment
        else
        {
            var apptToCopy = AppointmentList.FirstOrDefault(apt => (apt?.ID ?? 0) == appointment.ID);
            if (apptToCopy != null)
            {
                var index = AppointmentList.IndexOf(apptToCopy);
                AppointmentList.RemoveAt(index);
                appointmentList.Insert(index, appointment);
            }
        }
        return appointment;
    }

    public Appointment? Delete(int appointmentID)
    {
        var response = new WebRequestHandler().Delete($"/Appointment/{appointmentID}").Result;
        //find appointment by ID
        var existingAppointment = appointmentList.FirstOrDefault(a => (a?.ID ?? 0) == appointmentID);
        if (existingAppointment == null) { return null; }

        //remove appointment from list
        appointmentList.Remove(existingAppointment);
        return existingAppointment;
    }

    public async Task<List<Appointment?>> Search(QueryRequest query)
    {
        var appointmentPayload = await new WebRequestHandler().Post("/Appointment/Search", query);
        var appointmentFromServer = JsonConvert.DeserializeObject<List<Appointment?>>(appointmentPayload);

        appointmentList = appointmentFromServer;
        return appointmentList;
    }

    public Appointment? GetByID(int ID)
    {
        if (ID <= 0)
        {
            return null;
        }
        return appointmentList.FirstOrDefault(a => a?.ID == ID);
    }
}
