using Clinic.API.Enterprise;
using Clinic.Library.Data;
using Clinic.Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PhysicianController : ControllerBase
{
    private readonly ILogger<PhysicianController> _logger;
    public PhysicianController(ILogger<PhysicianController> logger)
    {
        _logger = logger;
    }

    //return list of physicians using EC
    [HttpGet]
    public IEnumerable<Physician> Get()
    {
        return new PhysicianEC().GetPhysicians();
    }

    //return physician by id using EC
    [HttpGet("{id}")]
    public Physician? GetByID(int id)
    {
        return new PhysicianEC().GetByID(id);
    }

    //delete a physician from database using EC
    [HttpDelete("{id}")]
    public Physician? Delete(int id)
    {
        return new PhysicianEC().Delete(id);
    }

    //add or update physician on database using EC
    [HttpPost]
    public Physician? AddOrUpdate([FromBody] Physician physician)
    {
        return new PhysicianEC().AddOrUpdate(physician);
    }
}
