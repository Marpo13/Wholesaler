using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Tests.Builders;
using Wholesaler.Tests.Helpers;
using Xunit;

namespace Wholesaler.Tests.RequirementController;

public class RequirementControllerTestsEditQuantity : WholesalerWebTest
{
    private readonly ClientBuilder _clientBuilder;
    private readonly StorageBuilder _storageBuilder;
    private readonly RequirementBuilder _requirementBuilder;
    private readonly DateTime _defaultDate = new(2023, 02, 13, 12, 0, 0);

    public RequirementControllerTestsEditQuantity(WebApplicationFactory<Program> factory)
        : base(factory)
    {
        _clientBuilder = new();
        _storageBuilder = new();
        _requirementBuilder = new();

        _timeProviderMock
            .Setup(m => m.Now())
            .Returns(_defaultDate);
    }

    [Fact]
    public async Task Edit_WithValidModel_ReturnsRequirementDtoAsync()
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

        var updateRequestModel = new UdpateRequirementRequestModel()
        {
            Quantity = 50
        };

        var httpContent = updateRequestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PatchAsync($"requirements/{id}", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var requirementDto = await JsonDeserializeHelper.DeserializeAsync<RequirementDto>(response);

        requirementDto.Quantity.Should().Be(updateRequestModel.Quantity);
    }

    [Fact]
    public async Task Edit_WithInvalidId_ReturnsNotFoundAsync()
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

        var updateRequestModel = new UdpateRequirementRequestModel()
        {
            Quantity = 50
        };

        var httpContent = updateRequestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PatchAsync($"requirements/{id}", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Edit_WithInvalidQuantity_ReturnsBadRequestAsync()
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

        var updateRequestModel = new UdpateRequirementRequestModel()
        {
            Quantity = -50
        };

        var httpContent = updateRequestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PatchAsync($"requirements/{id}", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Edit_WithCompletedStatus_ReturnsBadRequestAsync()
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

        var updateRequestModel = new UdpateRequirementRequestModel()
        {
            Quantity = 50
        };

        var httpContent = updateRequestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PatchAsync($"requirements/{requirement.Id}", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
