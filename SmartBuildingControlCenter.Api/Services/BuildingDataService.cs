using SmartBuildingControlCenter.Api.Models;

namespace SmartBuildingControlCenter.Api.Services;

public class BuildingDataService
{
    private readonly List<Building> _buildings;

    public BuildingDataService()
    {
        _buildings = CreateInitialBuildings();
    }

    private List<Building> CreateInitialBuildings()
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

    public List<Building> GetBuildings()
    {
        return _buildings;
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

    public Room? GetRoomByNumber(int number)
    {
        var rooms = GetRooms();
        return rooms.FirstOrDefault(room => room.Number == number);
    }

    public bool SetRoomOccupancy(int roomNumber, bool isOccupied)
    {
        var room = GetRoomByNumber(roomNumber);
        if (room == null)
        {
            return false;
        }

        room.IsOccupied = isOccupied;
        return true;
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

    private double CalculateOccupancyPercentage(int occupied, int total)
    {
        if (total == 0)
            return 0;
        return (double)occupied / total * 100;
    }

    public int GetOccupiedRoomCount()
    {
        var rooms = GetRooms();
        return rooms.Count(room => room.IsOccupied);
    }

    public double GetOccupancyPercentage()
    {
        var totalRoomCount = GetRooms().Count;
        var occupiedRoomCount = GetOccupiedRoomCount();
        return CalculateOccupancyPercentage(occupiedRoomCount, totalRoomCount);
    }

    public BuildingStatistics GetBuildingStatistics()
    {
        var totalRooms = GetRooms().Count;
        var occupiedRooms = GetOccupiedRoomCount();

        return new BuildingStatistics
        {
            TotalRooms = totalRooms,
            OccupiedRooms = occupiedRooms,
            UnoccupiedRooms = totalRooms - occupiedRooms,
            OccupancyPercentage = GetOccupancyPercentage()
        };
    }

    private Floor? GetFloorByNumber(int floorNumber)
    {
        return _buildings
            .SelectMany(building => building.Floors)
            .FirstOrDefault(floor => floor.Number == floorNumber);
    }

    public int? GetOccupiedRoomCountByFloor(int floorNumber)
    {
        var floor = GetFloorByNumber(floorNumber);
        return floor?.Rooms.Count(room => room.IsOccupied);
    }

    public FloorStatistics? GetFloorStatistics(int floorNumber)
    {
        var floor = GetFloorByNumber(floorNumber);

        if (floor == null)
            return null;

        var totalRooms = floor.Rooms.Count;
        var occupiedRooms = floor.Rooms.Count(room => room.IsOccupied);

        var occupancyPercentage = CalculateOccupancyPercentage(occupiedRooms, totalRooms);

        return new FloorStatistics()
        {
            FloorNumber = floor.Number,
            TotalRooms = totalRooms,
            OccupiedRooms = occupiedRooms,
            UnoccupiedRooms = totalRooms - occupiedRooms,
            OccupancyPercentage = occupancyPercentage
        };
    }
}
