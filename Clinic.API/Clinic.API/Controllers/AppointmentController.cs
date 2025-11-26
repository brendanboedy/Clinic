using Clinic.API.Enterprise;
using Clinic.Library.Data;
using Clinic.Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly ILogger<AppointmentController> _logger;
    public AppointmentController(ILogger<AppointmentController> logger)
    {
        _logger = logger;
    }

    //return list of appointments using EC
    [HttpGet]
    public IEnumerable<Appointment> Get()
    {
        return new AppointmentEC().GetAppointments();
    }

    //return appointment by id using EC
    [HttpGet("{id}")]
    public Appointment? GetByID(int id)
    {
        return new AppointmentEC().GetByID(id);
    }

    //delete appointment by id using EC
    [HttpDelete("{id}")]
    public Appointment? Delete(int id)
    {
        return new AppointmentEC().Delete(id);
    }

    //add or update appointment using EC
    [HttpPost]
    public Appointment? AddOrUpdate([FromBody] Appointment appointment)
    {
        return new AppointmentEC().AddOrUpdate(appointment);
    }

    //return appointment by search query
    [HttpPost("Search")]
    public IEnumerable<Appointment?> Search(QueryRequest query)
    {
        return new AppointmentEC().Search(query?.Content ?? string.Empty);
    }
}
