using Microsoft.AspNetCore.Mvc;
using SmartBuildingControlCenter.Api.Models;
using SmartBuildingControlCenter.Api.Services;

namespace SmartBuildingControlCenter.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuildingsController : ControllerBase
{
    private readonly BuildingDataService _buildingDataService;

    public BuildingsController(BuildingDataService buildingDataService)
    {
        _buildingDataService = buildingDataService;
    }

    [HttpGet]
    public List<Building> Get()
    {
        return _buildingDataService.GetBuildings();
    }

    [HttpGet("statistics")]
    public ActionResult<BuildingStatistics> GetStatistics()
    {
        return _buildingDataService.GetBuildingStatistics();
    }
}
