using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Wholesaler.Backend.DataAccess.Models;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Tests.Helpers;
using Xunit;

namespace Wholesaler.Tests.ClientController
{
    public class ClientControllerTestsAdd : WholesalerWebTest
    {
        public ClientControllerTestsAdd(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("Patrycja", "Ptak")]
        [InlineData("Natalia", "Nowak")]
        [InlineData("Bogumiła", "Bogacka")]
        [InlineData("Katarzyna", "Kowalska")]
        [InlineData("Iga", "Igła")]
        public async Task AddClient_WithValidData_ReturnsClientDto(string name, string surname)
        {
            //Arrange

            var addClientRequestModel = new AddClientRequestModel()
            {
                Name = name,
                Surname = surname,
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
        public async Task AddClient_WithoutName_ReturnsBadRequest(string surname)
        {
            //Arrange

            var addClientRequestModel = new AddClientRequestModel()
            {
                Name = "",
                Surname = surname,
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
        public async Task AddClient_WithoutSurname_ReturnsBadRequest(string name)
        {
            //Arrange

            var addClientRequestModel = new AddClientRequestModel()
            {
                Name = name,
                Surname = "",
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
        public async Task AddClient_WithEmptySurname_ReturnsBadRequest(string name)
        {
            //Arrange

            var addClientRequestModel = new AddClientRequestModel
            {
                Name = name,
                Surname = " ",
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
        public async Task AddClient_WithEmptyName_ReturnsBadRequest(string surname)
        {
            //Arrange

            var addClientRequestModel = new AddClientRequestModel
            {
                Name = " ",
                Surname = surname,
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
        public async Task AddClient_WitInvalidName_ReturnsBadRequest(string name, string surname)
        {
            //Arrange

            var addClientRequestModel = new AddClientRequestModel()
            {
                Name = name,
                Surname = surname,
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
        public async Task AddClient_WitInvalidSurame_ReturnsBadRequest(string name, string surname)
        {
            //Arrange

            var addClientRequestModel = new AddClientRequestModel()
            {
                Name = name,
                Surname = surname,
            };

            var httpContent = addClientRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PostAsync("clients", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        [Fact]
        public async Task AddClient_WithoutNameAndSurname_ReturnsBadRequest()
        {
            //Arrange

            var addClientRequestModel = new AddClientRequestModel()
            {
                Name = "",
                Surname = "",
            };

            var httpContent = addClientRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PostAsync("clients", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
