using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Tests.Builders;
using Wholesaler.Tests.Helpers;
using Xunit;
using Status = Wholesaler.Backend.Domain.Entities.Status;

namespace Wholesaler.Tests.RequirementController
{
    public class RequirementControllerTestsComplete : WholesalerWebTest
    {
        private readonly RequirementBuilder _requirementBuilder;
        private readonly ClientBuilder _clientBuilder;
        private readonly StorageBuilder _storageBuilder;
        private readonly DateTime _defaultDate = new(2023, 02, 13, 12, 0, 0);

        public RequirementControllerTestsComplete(WebApplicationFactory<Program> factory) : base(factory)
        {
            _clientBuilder = new ClientBuilder();
            _storageBuilder = new StorageBuilder();
            _requirementBuilder = new RequirementBuilder();

            _timeProviderMock
                .Setup(m => m.Now())
                .Returns(_defaultDate);
        }

        [Fact]
        public async Task Complete_WithValidId_ReturnsReguirementDto()
        {
            //Arrange

            var client = _clientBuilder.Build();
            var storage = _storageBuilder.Build();
            var requirement = _requirementBuilder
                .WithClientId(client.Id)
                .WithStorageId(storage.Id)
                .Build();

            Seed(client);
            Seed(storage);
            Seed(requirement);

            var id = requirement.Id;

            //Act

            var response = await _client.PatchAsync($"requirements/{id}/actions/complete", null);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var requirementDto = JsonDeserializeHelper.DeserializeAsync<RequirementDto>(response);

            //requirementDto.Status.Should().Be(Status.Completed.ToString());
        }
                
        public async Task Complete_WithInvalidId_ReturnsNotFound()
        {
            //Arrange

            var client = _clientBuilder.Build();
            var storage = _storageBuilder.Build();
            var requirement = _requirementBuilder
                .WithClientId(client.Id)
                .WithStorageId(storage.Id)
                .Build();

            Seed(client);
            Seed(storage);
            Seed(requirement);

            var id = Guid.NewGuid();

            //Act

            var response = await _client.PatchAsync($"requirements/{id}/actions/complete", null);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        public async Task Complete_WithCompletedRequirement_ReturnsBadRequest()
        {
            //Arrange

            var client = _clientBuilder.Build();
            var storage = _storageBuilder.Build();
            var requirement = _requirementBuilder
                .WithClientId(client.Id)
                .WithStorageId(storage.Id)
                .Completed(_defaultDate)
                .Build();

            Seed(client);
            Seed(storage);
            Seed(requirement);

            var id = requirement.Id;

            //Act

            var response = await _client.PatchAsync($"requirements/{id}/actions/complete", null);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
