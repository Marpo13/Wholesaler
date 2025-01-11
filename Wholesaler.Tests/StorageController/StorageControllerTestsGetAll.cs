using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Tests.Builders;
using Wholesaler.Tests.Helpers;
using Xunit;

namespace Wholesaler.Tests.StorageController;

public class StorageControllerTestsGetAll : WholesalerWebTest
{
    private readonly StorageBuilder _storageBuilder;

    public StorageControllerTestsGetAll(WebApplicationFactory<Program> factory)
        : base(factory)
    {
        _storageBuilder = new();
    }

    [Fact]
    public async Task GetAll_ReturnsListOfStoragesDtoAsync()
    {
        //Arrange
        var storage1 = _storageBuilder.Build();
        var storage2 = _storageBuilder.Build();

        Seed(storage1);
        Seed(storage2);

        //Act
        var response = await _client.GetAsync("storages");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var storages = await JsonDeserializeHelper.DeserializeAsync<List<StorageDto>>(response);

        var storageDto1 = storages.First(s => s.Id == storage1.Id);
        storageDto1.Id.Should().Be(storage1.Id);
        storageDto1.Name.Should().Be(storage1.Name);
        storageDto1.State.Should().Be(storage1.State);

        var storageDto2 = storages.First(s => s.Id == storage2.Id);
        storageDto2.Id.Should().Be(storage2.Id);
        storageDto2.Name.Should().Be(storage2.Name);
        storageDto2.State.Should().Be(storage2.State);
    }
}
