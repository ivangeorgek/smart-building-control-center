using Microsoft.AspNetCore.Mvc;
namespace SmartBuildingControlCenter.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuildingsController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "Smart Building Control Center API is running";
    }
}