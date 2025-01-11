using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Wholesaler.Tests.Builders;
using Xunit;

namespace Wholesaler.Tests.ClientController;

public class ClientControllerTestsDelete : WholesalerWebTest
{
    private readonly ClientBuilder _clientBuilder;

    public ClientControllerTestsDelete(WebApplicationFactory<Program> factory)
        : base(factory)
    {
        _clientBuilder = new();
    }

    [Fact]
    public async Task DeleteClient_WithValidId_ReturnsNoContentAsync()
    {
        //Arrange
        var client = _clientBuilder.Build();
        Seed(client);

        var id = client.Id;

        //Act
        var response = await _client.DeleteAsync($"clients/{id}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        var deletedClient = _dbContext.Clients.FirstOrDefault(c => c.Id == client.Id);
        deletedClient.Should().BeNull();
    }

    public async Task DeleteClient_WithInvalidId_ReturnsBadRequestAsync()
    {
        //Arrange
        var client = _clientBuilder.Build();
        Seed(client);

        var id = Guid.NewGuid();

        //Act
        var response = await _client.DeleteAsync($"clients/{id}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var notDeletedClient = _dbContext.Clients.FirstOrDefault(c => c.Id == client.Id);
        notDeletedClient.Should().NotBeNull();
    }
}
