using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Tests.Helpers;
using Xunit;

namespace Wholesaler.Tests.ClientController;

public class ClientControllerTestsAdd : WholesalerWebTest
{
    public ClientControllerTestsAdd(WebApplicationFactory<Program> factory)
        : base(factory)
    {
    }

    [Theory]
    [InlineData("Patrycja", "Ptak")]
    [InlineData("Natalia", "Nowak")]
    [InlineData("Bogumiła", "Bogacka")]
    [InlineData("Katarzyna", "Kowalska")]
    [InlineData("Iga", "Igła")]
    public async Task AddClient_WithValidData_ReturnsClientDtoAsync(string name, string surname)
    {
        //Arrange
        var addClientRequestModel = new AddClientRequestModel()
        {
            Name = name,
            Surname = surname
        };

        var httpContent = addClientRequestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PostAsync("clients", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var clientDto = await JsonDeserializeHelper.DeserializeAsync<ClientDto>(response);
        var clientDb = _dbContext.Clients.First(c => c.Surname == addClientRequestModel.Surname);

        clientDb.Id.Should().Be(clientDto.Id);
        clientDb.Name.Should().Be(addClientRequestModel.Name);

        clientDto.Name.Should().Be(addClientRequestModel.Name);
        clientDto.Surname.Should().Be(addClientRequestModel.Surname);
    }

    [Theory]
    [InlineData("Ptak")]
    [InlineData("Nowak")]
    [InlineData("Bogacka")]
    [InlineData("Kowalska")]
    [InlineData("Igła")]
    public async Task AddClient_WithoutName_ReturnsBadRequestAsync(string surname)
    {
        //Arrange
        var addClientRequestModel = new AddClientRequestModel()
        {
            Name = string.Empty,
            Surname = surname
        };

        var httpContent = addClientRequestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PostAsync("clients", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Theory]
    [InlineData("Patrycja")]
    [InlineData("Nadia")]
    [InlineData("Bogusława")]
    [InlineData("Kamila")]
    [InlineData("Iga")]
    public async Task AddClient_WithoutSurname_ReturnsBadRequestAsync(string name)
    {
        //Arrange
        var addClientRequestModel = new AddClientRequestModel()
        {
            Name = name,
            Surname = string.Empty
        };

        var httpContent = addClientRequestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PostAsync("clients", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Theory]
    [InlineData("Patrycja")]
    [InlineData("Nadia")]
    public async Task AddClient_WithEmptySurname_ReturnsBadRequestAsync(string name)
    {
        //Arrange
        var addClientRequestModel = new AddClientRequestModel
        {
            Name = name,
            Surname = " "
        };

        var httpContent = addClientRequestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PostAsync("clients", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Theory]
    [InlineData("Ptak")]
    [InlineData("Nowak")]
    public async Task AddClient_WithEmptyName_ReturnsBadRequestAsync(string surname)
    {
        //Arrange
        var addClientRequestModel = new AddClientRequestModel
        {
            Name = " ",
            Surname = surname
        };

        var httpContent = addClientRequestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PostAsync("clients", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Theory]
    [InlineData("3283h29b32783gb28", "Nowak")]
    [InlineData("56446131hgd1568", "Kowalski")]
    public async Task AddClient_WitInvalidName_ReturnsBadRequestAsync(string name, string surname)
    {
        //Arrange
        var addClientRequestModel = new AddClientRequestModel()
        {
            Name = name,
            Surname = surname
        };
        var httpContent = addClientRequestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PostAsync("clients", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Theory]
    [InlineData("Anna", "3283h29b32783gb28")]
    [InlineData("Marek", "56446131hgd1568")]
    public async Task AddClient_WitInvalidSurame_ReturnsBadRequestAsync(string name, string surname)
    {
        //Arrange
        var addClientRequestModel = new AddClientRequestModel()
        {
            Name = name,
            Surname = surname
        };

        var httpContent = addClientRequestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PostAsync("clients", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AddClient_WithoutNameAndSurname_ReturnsBadRequestAsync()
    {
        //Arrange
        var addClientRequestModel = new AddClientRequestModel()
        {
            Name = string.Empty,
            Surname = string.Empty
        };

        var httpContent = addClientRequestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PostAsync("clients", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
