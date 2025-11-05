using Clinic.API.Database;
using Clinic.API.Enterprise;
using Clinic.Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private readonly ILogger<PatientController> _logger;
    public PatientController(ILogger<PatientController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Patient> Get()
    {
        return new PatientEC().GetPatients();
    }

    [HttpGet("{id}")]
    public Patient? GetByID(int id)
    {
        return new PatientEC().GetByID(id);
    }

    [HttpDelete("{id}")]
    public Patient? Delete(int id)
    {
        return new PatientEC().Delete(id);
    }
}
