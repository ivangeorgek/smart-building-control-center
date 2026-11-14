using Microsoft.AspNetCore.Mvc;
using SmartBuildingControlCenter.Api.Models;
using SmartBuildingControlCenter.Api.Services;

namespace SmartBuildingControlCenter.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FloorsController : ControllerBase
{
    private readonly BuildingDataService _buildingDataService;

    public FloorsController(BuildingDataService buildingDataService)
    {
        _buildingDataService = buildingDataService;
    }

    [HttpGet("{floorNumber:int}/occupied-count")]
    public ActionResult<int> GetOccupiedRoomCount(int floorNumber)
    {
        var result = _buildingDataService.GetOccupiedRoomCountByFloor(floorNumber);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("{floorNumber:int}/statistics")]
    public ActionResult<FloorStatistics> GetFloorStatistics(int floorNumber)
    {
        var result = _buildingDataService.GetFloorStatistics(floorNumber);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
