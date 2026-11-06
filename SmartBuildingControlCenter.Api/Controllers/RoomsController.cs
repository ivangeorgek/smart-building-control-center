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
}
