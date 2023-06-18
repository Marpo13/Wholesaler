using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Tests.Builders;
using Wholesaler.Tests.Helpers;
using Xunit;

namespace Wholesaler.Tests.StorageController
{
    public class StorageControllerTestsAdd : WholesalerWebTest
    {        
        public StorageControllerTestsAdd(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Add_WithValidModel_ReturnsStorageDto()
        {
            //Arrange

            var storageRequestModel = new AddStorageRequestModel()
            {
                Name = "Storage1"
            };

            var httpContent = storageRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PostAsync("storages", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var storageDto = await JsonDeserializeHelper.DeserializeAsync<StorageDto>(response);
            var storage = _dbContext.Storages.First(s => s.Name == storageRequestModel.Name);

            storageDto.Name.Should().Be(storageRequestModel.Name);

            storageDto.Name.Should().Be(storage.Name);
            storageDto.Id.Should().Be(storage.Id);
            storageDto.State.Should().Be(storage.State);
        }


        [Fact]
        public async Task Add_WithInvalidModel_ReturnsBadRequest()
        {
            //Arrange

            var storageRequestModel = new AddStorageRequestModel()
            {
                Name = ""
            };

            var httpContent = storageRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PostAsync("storages", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}
