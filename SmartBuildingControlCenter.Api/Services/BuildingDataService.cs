using SmartBuildingControlCenter.Api.Models;

namespace SmartBuildingControlCenter.Api.Services;

public class BuildingDataService
{
    public List<Building> GetBuildings()
    {
        List<Building> buildings = new();

        var techTowerBuilding = new Building
        {
            Name = "Tech Tower",
            IsOccupied = true,
        };

        techTowerBuilding.Floors.Add(new Floor { Number = 1, Name = "First Floor" });
        techTowerBuilding.Floors.Add(new Floor { Number = 2, Name = "Second Floor" });
        techTowerBuilding.Floors.Add(new Floor { Number = 3, Name = "Third Floor" });

        var firstFloor = techTowerBuilding.Floors[0];
        var secondFloor = techTowerBuilding.Floors[1];
        var thirdFloor = techTowerBuilding.Floors[2];

        firstFloor.Rooms.Add(new Room
        {
            Number = 101,
            Name = "Meeting Room",
            IsOccupied = false
        });
        firstFloor.Rooms.Add(new Room
        {
            Number = 102,
            Name = "Open Office",
            IsOccupied = true
        });

        AddRoomsToFloor(firstFloor, 103, 104);
        AddRoomsToFloor(secondFloor, 201, 204);
        AddRoomsToFloor(thirdFloor, 301, 304);

        buildings.Add(techTowerBuilding);
        return buildings;
    }

    public List<Room> GetRooms()
    {
        var rooms = new List<Room>();
        var buildings = GetBuildings();
        foreach (var building in buildings)
        {
            foreach (var floor in building.Floors)
            {
                foreach (var room in floor.Rooms)
                {
                    rooms.Add(room);
                }
            }
        }

        return rooms;
    }

    private void AddRoomsToFloor(Floor floor, int startRoomNumber, int endRoomNumber)
    {
        for (int roomNumber = startRoomNumber;
             roomNumber <= endRoomNumber;
             roomNumber++)
        {
            var newRoom = new Room
            {
                Number = roomNumber,
                Name = $"Room {roomNumber}",
            };
            floor.Rooms.Add(newRoom);
        }
    }
}
