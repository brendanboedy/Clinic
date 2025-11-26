using Clinic.API.Enterprise;
using Clinic.Library.Models;
using Microsoft.AspNetCore.Mvc;
using Clinic.Library.Data;
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

    //get list of patients using EC
    [HttpGet]
    public IEnumerable<Patient> Get()
    {
        return new PatientEC().GetPatients();
    }

    //return patient by ID using EC
    [HttpGet("{id}")]
    public Patient? GetByID(int id)
    {
        return new PatientEC().GetByID(id);
    }

    //delete patient from database using EC
    [HttpDelete("{id}")]
    public Patient? Delete(int id)
    {
        return new PatientEC().Delete(id);
    }

    //add or update patient to database using EC
    [HttpPost()]
    public Patient? AddOrUpdate([FromBody] Patient patient)
    {
        return new PatientEC().AddOrUpdate(patient);
    }

    [HttpPost("Search")]
    public IEnumerable<Patient?> Search([FromBody] QueryRequest query)
    {
        return new PatientEC().Search(query.Content);
    }
}
