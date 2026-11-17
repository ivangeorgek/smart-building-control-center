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
}
