using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Tests.Builders;
using Wholesaler.Tests.Helpers;
using Xunit;

namespace Wholesaler.Tests.StorageController
{
    public class StorageControllerTestsDelivery : WholesalerWebTest
    {
        private readonly StorageBuilder _storageBuilder;

        public StorageControllerTestsDelivery(WebApplicationFactory<Program> factory) : base(factory)
        {
            _storageBuilder = new StorageBuilder();
        }

        [Theory]
        [InlineData(15)]
        [InlineData(20)]
        [InlineData(100)]
        [InlineData(215)]
        [InlineData(600)]
        public async Task Deliver_WithValidModel_ReturnsStorageDto(int quantity)
        {
            //Arrange

            var storage = _storageBuilder.Build();
            Seed(storage);
            
            var updateStorageRequestModel = new UpdateStorageRequestModel()
            {
                Quantity = quantity
            };

            var id = storage.Id;

            var httpContent = updateStorageRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PatchAsync($"storages/{id}/actions/deliver", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var storageDto = await JsonDeserializeHelper.DeserializeAsync<StorageDto>(response);
            var storageDb = _dbContext.Storages.First(s => s.Id == storage.Id);

            storageDto.State.Should().Be(100 + quantity);
        }

        [Theory]
        [InlineData(15)]
        [InlineData(20)]
        [InlineData(100)]
        [InlineData(215)]
        [InlineData(600)]
        public async Task Deliver_WithInvalidStorageId_ReturnsNotFound(int quantity)
        {
            //Arrange

            var storage = _storageBuilder.Build();
            Seed(storage);

            var updateStorageRequestModel = new UpdateStorageRequestModel()
            {
                Quantity = quantity
            };

            var id = Guid.NewGuid();

            var httpContent = updateStorageRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PatchAsync($"storages/{id}/actions/deliver", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData(-15)]
        [InlineData(-20)]
        [InlineData(-100)]
        [InlineData(-215)]
        [InlineData(-600)]
        [InlineData(0)]
        [InlineData(0.2)]
        public async Task Deliver_WithInvalidQuantity_ReturnsBadRequest(int quantity)
        {
            //Arrange

            var storage = _storageBuilder.Build();
            Seed(storage);

            var updateStorageRequestModel = new UpdateStorageRequestModel()
            {
                Quantity = quantity
            };

            var id = storage.Id;

            var httpContent = updateStorageRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PatchAsync($"storages/{id}/actions/deliver", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}
