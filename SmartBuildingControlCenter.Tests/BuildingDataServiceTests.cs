using SmartBuildingControlCenter.Api.Services;
using Xunit;

namespace SmartBuildingControlCenter.Tests;

public class BuildingDataServiceTests
{
    [Fact]
    public void GetBuildings_ShouldReturnOneBuilding()
    {
        var service = new BuildingDataService();

        var buildings = service.GetBuildings();

        Assert.Single(buildings);
    }

    [Fact]
    public void GetRoomByNumber_ShouldReturnCorrectRoom_WhenRoomExists()
    {
        var service = new BuildingDataService();
        var room = service.GetRoomByNumber(102);
        Assert.NotNull(room);
        Assert.Equal(102, room.Number);
        Assert.Equal("Open Office", room.Name);
    }

    [Fact]
    public void GetRoomByNumber_ShouldReturnNull_WhenRoomDoesNotExist()
    {
        var service = new BuildingDataService();
        var room = service.GetRoomByNumber(999);
        Assert.Null(room);
    }

    [Fact]
    public void SetRoomOccupancy_ShouldUpdateRoom_WhenRoomExists()
    {
        var service = new BuildingDataService();

        var result = service.SetRoomOccupancy(102, false);
        var room = service.GetRoomByNumber(102);

        Assert.True(result);
        Assert.NotNull(room);
        Assert.Equal(102, room.Number);
        Assert.False(room.IsOccupied);
    }

    [Fact]
    public void SetRoomOccupancy_ShouldReturnFalse_WhenRoomDoesNotExist()
    {
        var service = new BuildingDataService();

        var result = service.SetRoomOccupancy(999, true);

        Assert.False(result);
    }
}
