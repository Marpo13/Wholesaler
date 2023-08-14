using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Tests.Builders;
using Wholesaler.Tests.Helpers;
using Xunit;

namespace Wholesaler.Tests.ClientController
{
    public class ClientControllerTestsGet : WholesalerWebTest
    {
        private readonly ClientBuilder _clientBuilder;

        public ClientControllerTestsGet(WebApplicationFactory<Program> factory) : base(factory)
        {
            _clientBuilder = new ClientBuilder();
        }

        [Fact]
        public async Task GetClient_WithValidId_ReturnsClientDto()
        {
            //Arrange

            var client = _clientBuilder.Build();
            Seed(client);

            var id = client.Id;

            //Act

            var response = await _client.GetAsync($"clients/{id}");

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var clientDto = await JsonDeserializeHelper.DeserializeAsync<ClientDto>(response);

            clientDto.Id.Should().Be(client.Id);
            clientDto.Name.Should().Be(client.Name);
            clientDto.Surname.Should().Be(client.Surname);
        }

        [Fact]
        public async Task GetClient_WithInvalidId_ReturnsNotFound()
        {
            //Arrange

            var client = _clientBuilder.Build();
            Seed(client);

            var id = Guid.NewGuid();

            //Act

            var response = await _client.GetAsync($"clients/{id}");

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
