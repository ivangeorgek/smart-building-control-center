using SmartBuildingControlCenter.Api.Models;
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

    [Theory]
    [InlineData(101, "Meeting Room")]
    [InlineData(102, "Open Office")]
    [InlineData(304, "Room 304")]
    public void GetRoomByNumber_ShouldReturnExpectedName(
        int roomNumber,
        string expectedName)
    {
        var service = new BuildingDataService();

        var room = service.GetRoomByNumber(roomNumber);

        Assert.NotNull(room);
        Assert.Equal(expectedName, room.Name);
    }

    [Theory]
    [InlineData(102, false)]
    [InlineData(101, true)]
    public void SetRoomOccupancy_ShouldSetExpectedOccupancy(
        int roomNumber,
        bool expectedOccupancy)
    {
        var service = new BuildingDataService();

        var result = service.SetRoomOccupancy(roomNumber, expectedOccupancy);
        var room = service.GetRoomByNumber(roomNumber);

        Assert.True(result);
        Assert.NotNull(room);
        Assert.Equal(roomNumber, room.Number);
        Assert.Equal(expectedOccupancy, room.IsOccupied);
    }

    [Fact]
    public void SetRoomOccupancy_ShouldNotAffectOtherRooms()
    {
        var service = new BuildingDataService();

        var meetingRoom = service.GetRoomByNumber(101);
        Assert.NotNull(meetingRoom);
        var isMeetingRoomOccupancySet = service.SetRoomOccupancy(101, true);

        var openOfficeRoom = service.GetRoomByNumber(102);
        Assert.NotNull(openOfficeRoom);
        var isOpenOfficeRoomOccupancySet = service.SetRoomOccupancy(102, false);

        Assert.True(isMeetingRoomOccupancySet);
        Assert.True(isOpenOfficeRoomOccupancySet);

        Assert.Equal(101, meetingRoom.Number);
        Assert.Equal(102, openOfficeRoom.Number);

        Assert.True(meetingRoom.IsOccupied);
        Assert.False(openOfficeRoom.IsOccupied);
    }

    [Fact]
    public void SetRoomOccupancy_ShouldNotModifyExistingRooms_WhenRoomDoesNotExist()
    {
        // Arrange
        var service = new BuildingDataService();

        var existingRoom = service.GetRoomByNumber(102);
        Assert.NotNull(existingRoom);
        Assert.True(existingRoom.IsOccupied);

        // Act
        var isNonExistingRoomOccupancySet = service.SetRoomOccupancy(999, false);

        // Assert
        Assert.False(isNonExistingRoomOccupancySet);
        Assert.True(existingRoom.IsOccupied);
    }

    [Fact]
    public void GetOccupiedRoomCount_ShouldReturnOneInitially()
    {
        var service = new BuildingDataService();

        var count = service.GetOccupiedRoomCount();

        Assert.Equal(1, count);
    }

    [Fact]
    public void GetOccupiedRoomCount_ShouldIncreaseWhenRoomBecomesOccupied()
    {
        var service = new BuildingDataService();

        var result = service.SetRoomOccupancy(101, true);
        Assert.True(result);
        var count = service.GetOccupiedRoomCount();

        Assert.Equal(2, count);
    }

    [Fact]
    public void GetOccupiedRoomCount_ShouldDecreaseWhenRoomBecomesUnoccupied()
    {
        var service = new BuildingDataService();

        var result = service.SetRoomOccupancy(102, false);
        Assert.True(result);
        var count = service.GetOccupiedRoomCount();

        Assert.Equal(0, count);
    }

    [Fact]
    public void GetOccupancyPercentage_ShouldReturnExpectedPercentageInitially()
    {
        var service = new BuildingDataService();

        var percentage = service.GetOccupancyPercentage();

        Assert.Equal(8.33, percentage, 2);
    }

    [Fact]
    public void GetOccupancyPercentage_ShouldIncreaseWhenRoomBecomesOccupied()
    {
        var service = new BuildingDataService();

        var result = service.SetRoomOccupancy(101, true);
        Assert.True(result);
        var percentage = service.GetOccupancyPercentage();

        Assert.Equal(16.67, percentage, 2);
    }

    [Fact]
    public void GetOccupancyPercentage_ShouldDecreaseWhenRoomBecomesUnoccupied()
    {
        var service = new BuildingDataService();

        var result = service.SetRoomOccupancy(102, false);
        Assert.True(result);
        var percentage = service.GetOccupancyPercentage();

        Assert.Equal(0, percentage);
    }

    [Fact]
    public void GetBuildingStatistics_ShouldReturnCorrectInitialStatistics()
    {
        var service = new BuildingDataService();

        BuildingStatistics statistics = service.GetBuildingStatistics();

        Assert.Equal(12, statistics.TotalRooms);
        Assert.Equal(1, statistics.OccupiedRooms);
        Assert.Equal(11, statistics.UnoccupiedRooms);
        Assert.Equal(8.33, statistics.OccupancyPercentage, 2);
    }

    [Fact]
    public void GetBuildingStatistics_ShouldUpdateWhenRoomOccupancyChanges()
    {
        var service = new BuildingDataService();

        var result = service.SetRoomOccupancy(101, true);
        Assert.True(result);
        BuildingStatistics statistics = service.GetBuildingStatistics();

        Assert.Equal(12, statistics.TotalRooms);
        Assert.Equal(2, statistics.OccupiedRooms);
        Assert.Equal(10, statistics.UnoccupiedRooms);
        Assert.Equal(16.67, statistics.OccupancyPercentage, 2);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 0)]
    [InlineData(3, 0)]
    public void GetOccupiedRoomCountByFloor_ShouldReturnExpectedCount(
        int floorNumber,
        int expectedCount)
    {
        // Arrange
        var service = new BuildingDataService();

        // Act
        var result = service.GetOccupiedRoomCountByFloor(floorNumber);

        // Assert
        Assert.Equal(expectedCount, result);
    }

    [Fact]
    public void GetOccupiedRoomCountByFloor_ShouldUpdateWhenRoomOccupancyChanges()
    {
        var service = new BuildingDataService();

        var count = service.GetOccupiedRoomCountByFloor(1);
        Assert.Equal(1, count);

        var setOccupancy = service.SetRoomOccupancy(101, true);
        Assert.True(setOccupancy);

        var result = service.GetOccupiedRoomCountByFloor(1);
        Assert.Equal(2, result);
    }

    [Fact]
    public void GetOccupiedRoomCountByFloor_ShouldReturnNull_WhenFloorDoesNotExist()
    {
        // Arrange
        var service = new BuildingDataService();

        // Act
        var result = service.GetOccupiedRoomCountByFloor(999);

        // Assert
        Assert.Null(result);
    }
}
