using Clinic.API.Database;
using Clinic.API.Enterprise;
using Clinic.Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AppointmentController
{
    private readonly ILogger<AppointmentController> _logger;
    public AppointmentController(ILogger<AppointmentController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Appointment> Get()
    {
        return new AppointmentEC().GetAppointments();
    }

    [HttpGet("{id}")]
    public Appointment? GetByID(int id)
    {
        return new AppointmentEC().GetByID(id);
    }

    [HttpDelete("{id}")]
    public Appointment? Delete(int id)
    {
        return new AppointmentEC().Delete(id);
    }
}
