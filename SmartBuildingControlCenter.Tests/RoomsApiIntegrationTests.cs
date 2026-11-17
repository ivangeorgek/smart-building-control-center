using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace SmartBuildingControlCenter.Tests;

public class RoomsApiIntegrationTests
{
    [Fact]
    public async Task GetOccupiedRoomCount_ShouldReturnOneInitially()
    {
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/api/rooms/occupied-count");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<int>();
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task GetRoomByNumber_ShouldReturnNotFound_WhenRoomDoesNotExist()
    {
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/api/rooms/999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task SetRoomOccupancy_ShouldUpdateOccupiedRoomCount()
    {
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var putResponse = await client.PutAsJsonAsync("/api/rooms/101/occupancy", true);

        Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

        var getResponse = await client.GetAsync("/api/rooms/occupied-count");

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        var result = await getResponse.Content.ReadFromJsonAsync<int>();
        Assert.Equal(2, result);
    }

    [Fact]
    public async Task SetRoomOccupancy_ShouldNotChangeState_WhenRoomDoesNotExist()
    {
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var beforeResponse = await client.GetAsync("/api/rooms/occupied-count");
        Assert.Equal(HttpStatusCode.OK, beforeResponse.StatusCode);
        var countBefore = await beforeResponse.Content.ReadFromJsonAsync<int>();

        // 404 for Non-existing room's occupancy update
        var putResponse = await client.PutAsJsonAsync("/api/rooms/999/occupancy", true);
        Assert.Equal(HttpStatusCode.NotFound, putResponse.StatusCode);

        // Room occupancy count shouldn't change
        var afterResponse = await client.GetAsync("/api/rooms/occupied-count");
        Assert.Equal(HttpStatusCode.OK, afterResponse.StatusCode);
        var countAfter = await afterResponse.Content.ReadFromJsonAsync<int>();
        Assert.Equal(countBefore, countAfter);
    }
}
