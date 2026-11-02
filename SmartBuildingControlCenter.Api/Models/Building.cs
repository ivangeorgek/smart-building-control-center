namespace SmartBuildingControlCenter.Api.Models;

public class Building
{
    public string Name { get; set; } = string.Empty;
    public bool IsOccupied { get; set; }
    public List<Floor> Floors { get; set; } = new();
    public int FloorCount => Floors.Count;
}
