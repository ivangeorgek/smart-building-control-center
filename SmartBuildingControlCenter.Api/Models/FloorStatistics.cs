namespace SmartBuildingControlCenter.Api.Models;

public class FloorStatistics
{
    public int FloorNumber { get; init; }
    public int TotalRooms { get; init; }
    public int OccupiedRooms { get; init; }
    public int UnoccupiedRooms { get; init; }
    public double OccupancyPercentage { get; init; }
}
