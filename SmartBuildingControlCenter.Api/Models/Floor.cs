namespace SmartBuildingControlCenter.Api.Models;

public class Floor
{
    public int Number { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Room> Rooms { get; set; } = new();
}
