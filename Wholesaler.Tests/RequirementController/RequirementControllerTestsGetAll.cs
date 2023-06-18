using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Tests.Builders;
using Wholesaler.Tests.Helpers;
using Xunit;

namespace Wholesaler.Tests.RequirementController
{
    public class RequirementControllerTestsGetAll : WholesalerWebTest
    {
        private readonly ClientBuilder _clientBuilder;
        private readonly StorageBuilder _storageBuilder;
        private readonly RequirementBuilder _requirementBuilder;

        public RequirementControllerTestsGetAll(WebApplicationFactory<Program> factory) : base(factory)
        {
            _clientBuilder = new ClientBuilder();
            _storageBuilder = new StorageBuilder();
            _requirementBuilder = new RequirementBuilder();
        }

        [Fact]
        public async Task GetAll_ReturnsListOfRequirementsDto()
        {
            //Arrange

            var client = _clientBuilder.Build();
            var storage1 = _storageBuilder.Build();
            var storage2 = _storageBuilder.Build();
            var requirement1 = _requirementBuilder
                .WithClientId(client.Id)
                .WithStorageId(storage1.Id)
                .Build();
            var requirement2 = _requirementBuilder
                .WithClientId(client.Id)
                .WithStorageId(storage1.Id)
                .Build();
            var requirement3 = _requirementBuilder
                .WithClientId(client.Id)
                .WithStorageId(storage2.Id)
                .Build();

            Seed(client);
            Seed(storage1);
            Seed(storage2);
            Seed(requirement1);
            Seed(requirement2);
            Seed(requirement3);

            //Act

            var response = await _client.GetAsync("requirements");

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var requirements = await JsonDeserializeHelper.DeserializeAsync<List<RequirementDto>>(response);
            //requirements.Count.Should().Be(3);

            var requirementDto1 = requirements.First(r => r.Id == requirement1.Id);
            requirementDto1.Quantity.Should().Be(requirement1.Quantity);
            requirementDto1.ClientId.Should().Be(requirement1.ClientId);
            requirementDto1.StorageId.Should().Be(requirement1.StorageId);
            requirementDto1.Status.Should().Be(requirement1.Status.ToString());
            requirementDto1.DeliveryDate.Should().Be(requirement1.DeliveryDate);

            var requirementDto2 = requirements.First(r => r.Id == requirement2.Id);
            requirementDto2.Quantity.Should().Be(requirement2.Quantity);
            requirementDto2.ClientId.Should().Be(requirement2.ClientId);
            requirementDto2.StorageId.Should().Be(requirement2.StorageId);
            requirementDto2.Status.Should().Be(requirement2.Status.ToString());
            requirementDto2.DeliveryDate.Should().Be(requirement2.DeliveryDate);

            var requirementDto3 = requirements.First(r => r.Id == requirement3.Id);
            requirementDto3.Quantity.Should().Be(requirement3.Quantity);
            requirementDto3.ClientId.Should().Be(requirement3.ClientId);
            requirementDto3.StorageId.Should().Be(requirement3.StorageId);
            requirementDto3.Status.Should().Be(requirement3.Status.ToString());
            requirementDto3.DeliveryDate.Should().Be(requirement3.DeliveryDate);
        }
    }
}
