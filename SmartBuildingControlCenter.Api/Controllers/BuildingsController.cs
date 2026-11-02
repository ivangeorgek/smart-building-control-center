using Microsoft.AspNetCore.Mvc;
using SmartBuildingControlCenter.Api.Models;

namespace SmartBuildingControlCenter.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuildingsController : ControllerBase
{
    [HttpGet]
    public List<Building> Get()
    {
        List<Building> buildings = new ();
        buildings.Add(new Building
            {
                Name = "Tech Tower",
                FloorCount = 3,
                IsOccupied = true
            });
        return buildings;
    }
}
