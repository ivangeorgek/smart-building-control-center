using Microsoft.AspNetCore.Mvc;
using SmartBuildingControlCenter.Api.Models;
using SmartBuildingControlCenter.Api.Services;

namespace SmartBuildingControlCenter.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly BuildingDataService _buildingDataService;

    public RoomsController(BuildingDataService buildingDataService)
    {
        _buildingDataService = buildingDataService;
    }

    [HttpGet]
    public List<Room> Get()
    {
        return _buildingDataService.GetRooms();
    }

    [HttpGet("{number:int}")]
    public ActionResult<Room> GetByNumber(int number)
    {
        var room = _buildingDataService.GetRoomByNumber(number);
        if (room == null)
        {
            return NotFound();
        }

        return room;
    }

    [HttpPut("{number:int}/occupancy")]
    public IActionResult SetOccupancy(int number, [FromBody] bool isOccupied)
    {
        return _buildingDataService.SetRoomOccupancy(number, isOccupied)
            ? NoContent()
            : NotFound();
    }

    [HttpGet("occupied-count")]
    public ActionResult<int> GetOccupiedRoomCount()
    {
        return _buildingDataService.GetOccupiedRoomCount();
    }
}
