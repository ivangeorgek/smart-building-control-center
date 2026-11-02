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
        List<Building> buildings = new();
        var techTowerBuilding = new Building
        {
            Name = "Tech Tower",
            IsOccupied = true,
        };
        techTowerBuilding.Floors.Add(new Floor { Number = 1, Name = "First Floor" });
        techTowerBuilding.Floors.Add(new Floor { Number = 2, Name = "Second Floor" });
        techTowerBuilding.Floors.Add(new Floor { Number = 3, Name = "Third Floor" });

        buildings.Add(techTowerBuilding);
        return buildings;
    }
}
