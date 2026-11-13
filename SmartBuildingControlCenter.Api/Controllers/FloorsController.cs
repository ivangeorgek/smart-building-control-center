using Microsoft.AspNetCore.Mvc;
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
}
