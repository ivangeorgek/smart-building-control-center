namespace SmartBuildingControlCenter.Api.Models;

public class BuildingStatistics
{
    public int TotalRooms { get; init; }
    public int OccupiedRooms { get; init; }
    public int UnoccupiedRooms { get; init; }
    public double OccupancyPercentage { get; init; }
}
