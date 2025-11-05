using Clinic.API.Database;
using Clinic.API.Enterprise;
using Clinic.Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PhysicianController
{
    private readonly ILogger<PhysicianController> _logger;
    public PhysicianController(ILogger<PhysicianController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Physician> Get()
    {
        return new PhysicianEC().GetPhysicians();
    }

    [HttpGet("{id}")]
    public Physician? GetByID(int id)
    {
        return new PhysicianEC().GetByID(id);
    }

    [HttpDelete("{id}")]
    public Physician? Delete(int id)
    {
        return new PhysicianEC().Delete(id);
    }
}
