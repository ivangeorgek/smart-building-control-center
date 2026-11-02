namespace SmartBuildingControlCenter.Api.Models;

public class Building
{
    public string Name { get; set; } = string.Empty;
    public int FloorCount { get; set; }
    public bool IsOccupied { get; set; }
}
