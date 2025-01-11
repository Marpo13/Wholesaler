using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Tests.Builders;
using Wholesaler.Tests.Helpers;
using Xunit;

namespace Wholesaler.Tests.ClientController;

public class ClientControllerTestsGetAll : WholesalerWebTest
{
    private readonly ClientBuilder _clientBuilder;

    public ClientControllerTestsGetAll(WebApplicationFactory<Program> factory)
        : base(factory)
    {
        _clientBuilder = new();
    }

    [Fact]
    public async Task GetAllClients_ReturnsListOfClientsDtoAsync()
    {
        //Arrange
        var client1 = _clientBuilder.Build();
        var client2 = _clientBuilder.Build();
        var client3 = _clientBuilder.Build();
        var client4 = _clientBuilder.Build();

        Seed(client1);
        Seed(client2);
        Seed(client3);
        Seed(client4);

        //Act
        var response = await _client.GetAsync("clients");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var clientsDto = await JsonDeserializeHelper.DeserializeAsync<List<ClientDto>>(response);

        var clientDto1 = clientsDto.First(c => c.Id == client1.Id);
        clientDto1.Name.Should().Be(client1.Name);
        clientDto1.Surname.Should().Be(client1.Surname);

        var clientDto2 = clientsDto.First(c => c.Id == client2.Id);
        clientDto2.Name.Should().Be(client2.Name);
        clientDto2.Surname.Should().Be(client2.Surname);

        var clientDto3 = clientsDto.First(c => c.Id == client3.Id);
        clientDto3.Name.Should().Be(client3.Name);
        clientDto3.Surname.Should().Be(client3.Surname);

        var clientDto4 = clientsDto.First(c => c.Id == client4.Id);
        clientDto4.Name.Should().Be(client4.Name);
        clientDto4.Surname.Should().Be(client4.Surname);
    }
}
