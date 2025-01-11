using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Tests.Builders;
using Wholesaler.Tests.Helpers;
using Xunit;

namespace Wholesaler.Tests.RequirementController;

public class RequirementControllerTestsGet : WholesalerWebTest
{
    private readonly ClientBuilder _clientBuilder;
    private readonly StorageBuilder _storageBuilder;
    private readonly RequirementBuilder _requirementBuilder;

    public RequirementControllerTestsGet(WebApplicationFactory<Program> factory)
        : base(factory)
    {
        _clientBuilder = new();
        _storageBuilder = new();
        _requirementBuilder = new();
    }

    [Fact]
    public async Task Get_ReturnsListOfRequirementsDtoAsync()
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

        var id = storage1.Id;

        //Act
        var response = await _client.GetAsync($"requirements/withStorageId?storageId={id}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var requirements = await JsonDeserializeHelper.DeserializeAsync<List<RequirementDto>>(response);
        requirements.Count.Should().Be(2);

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
    }
}
