using Clinic.API.Enterprise;
using Clinic.Library.Data;
using Clinic.Library.Models;
using Clinic.Library.DTO;
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
    public IEnumerable<AppointmentDTO> Get()
    {
        return new AppointmentEC().GetAppointments();
    }

    //return appointment by id using EC
    [HttpGet("{id}")]
    public AppointmentDTO? GetByID(int id)
    {
        return new AppointmentEC().GetByID(id);
    }

    //delete appointment by id using EC
    [HttpDelete("{id}")]
    public AppointmentDTO? Delete(int id)
    {
        return new AppointmentEC().Delete(id);
    }

    //add or update appointment using EC
    [HttpPost]
    public AppointmentDTO? AddOrUpdate([FromBody] AppointmentDTO appointment)
    {
        return new AppointmentEC().AddOrUpdate(appointment);
    }

    //return appointment by search query
    [HttpPost("Search")]
    public IEnumerable<AppointmentDTO?> Search(QueryRequest query)
    {
        return new AppointmentEC().Search(query?.Content ?? string.Empty);
    }
}
