using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Tests.Builders;
using Wholesaler.Tests.Helpers;
using Xunit;

namespace Wholesaler.Tests.RequirementController;

public class RequirementControllerTestsAdd : WholesalerWebTest
{
    private readonly StorageBuilder _storageBuilder;
    private readonly ClientBuilder _clientBuilder;

    public RequirementControllerTestsAdd(WebApplicationFactory<Program> factory)
        : base(factory)
    {
        _storageBuilder = new();
        _clientBuilder = new();
    }

    [Fact]
    public async Task Add_WithValidModel_ReturnsRequirementDtoAsync()
    {
        //Arrange
        var storage = _storageBuilder.Build();
        var client = _clientBuilder.Build();

        Seed(storage);
        Seed(client);

        var requirementRequestModel = new AddRequirementRequestModel()
        {
            Quantity = 15,
            StorageId = storage.Id,
            ClientId = client.Id
        };

        var httpContent = requirementRequestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PostAsync("requirements", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var requirementDto = await JsonDeserializeHelper.DeserializeAsync<RequirementDto>(response);
        var requirementDb = _dbContext.Requirements.First(r => r.Quantity == requirementRequestModel.Quantity);

        requirementDto.Quantity.Should().Be(requirementRequestModel.Quantity);
        requirementDto.StorageId.Should().Be(requirementRequestModel.StorageId);
        requirementDto.ClientId.Should().Be(requirementRequestModel.ClientId);

        requirementDb.Quantity.Should().Be(requirementRequestModel.Quantity);
        requirementDb.StorageId.Should().Be(requirementRequestModel.StorageId);
        requirementDb.ClientId.Should().Be(requirementRequestModel.ClientId);
    }

    [Fact]
    public async Task Add_WithInvalidClientId_ReturnsNotFoundAsync()
    {
        //Arrange
        var storage = _storageBuilder.Build();
        var client = _clientBuilder.Build();

        Seed(storage);
        Seed(client);

        var requirementRequestModel = new AddRequirementRequestModel()
        {
            Quantity = 15,
            StorageId = storage.Id,
            ClientId = Guid.NewGuid()
        };

        var httpContent = requirementRequestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PostAsync("requirements", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Add_WithInvalidStorageId_ReturnsNotFoundAsync()
    {
        //Arrange
        var storage = _storageBuilder.Build();
        var client = _clientBuilder.Build();

        Seed(storage);
        Seed(client);

        var requirementRequestModel = new AddRequirementRequestModel()
        {
            Quantity = 15,
            StorageId = Guid.NewGuid(),
            ClientId = client.Id
        };

        var httpContent = requirementRequestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PostAsync("requirements", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Add_WithInvalidQuantity_ReturnsBadRequestAsync()
    {
        //Arrange
        var storage = _storageBuilder.Build();
        var client = _clientBuilder.Build();

        Seed(storage);
        Seed(client);

        var requirementRequestModel = new AddRequirementRequestModel()
        {
            Quantity = -15,
            StorageId = storage.Id,
            ClientId = client.Id
        };

        var httpContent = requirementRequestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PostAsync("requirements", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
