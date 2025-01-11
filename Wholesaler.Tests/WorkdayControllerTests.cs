using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Tests.Builders;
using Wholesaler.Tests.Helpers;
using Xunit;
using Role = Wholesaler.Backend.Domain.Entities.Role;

namespace Wholesaler.Tests;

public class WorkdayControllerTests : WholesalerWebTest
{
    private readonly PersonBuilder _personBuilder;
    private readonly WorkdayBuilder _workdayBuilder;
    private readonly DateTime _defaultDate = new(2023, 02, 13, 12, 0, 0);

    public WorkdayControllerTests(WebApplicationFactory<Program> factory)
        : base(factory)
    {
        _personBuilder = new();
        _workdayBuilder = new();

        _timeProviderMock
            .Setup(m => m.Now())
            .Returns(_defaultDate);
    }

    [Fact]
    public async Task StartWorkday_WithValidData_ReturnsWorkdayDtoAsync()
    {
        //Arrange
        var person = _personBuilder
            .WithRole(Role.Employee)
            .Build();

        Seed(person);

        var requestModel = new StartWorkdayRequestModel()
        {
            UserId = person.Id
        };

        var httpContent = requestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PostAsync("workdays/actions/start", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var workdayDto = await JsonDeserializeHelper.DeserializeAsync<WorkdayDto>(response);
        var workdayDb = _dbContext.Workdays.First(w => w.Id == workdayDto.Id);

        workdayDto.Start.Should().Be(_defaultDate);
        workdayDto.Stop.Should().Be(null);

        workdayDb.Id.Should().Be(workdayDto.Id);
        workdayDb.Start.Should().Be(workdayDto.Start);
        workdayDb.Stop.Should().Be(workdayDto.Stop);
        workdayDb.PersonId.Should().Be(person.Id);
    }

    [Fact]
    public async Task StartWorkday_WithInvalidId_ReturnsBadRequestAsync()
    {
        //Arrange
        var requestModel = new StartWorkdayRequestModel()
        {
            UserId = Guid.NewGuid()
        };

        var httpContent = requestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PostAsync("workdays/actions/start", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task StartWorkday_WithInvalidRole_ReturnsBadRequestAsync()
    {
        //Arrange
        var person = _personBuilder
            .WithRole(Role.Manager)
            .Build();

        Seed(person);

        var requestModel = new StartWorkdayRequestModel()
        {
            UserId = person.Id
        };

        var httpContent = requestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PostAsync("workdays/actions/start", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task StartWorkday_WithPersonWithActiveWorkday_ReturnsBadRequestAsync()
    {
        //Arrange
        var person = _personBuilder
            .WithRole(Role.Employee)
            .Build();

        var workday = _workdayBuilder
            .WithPersonId(person.Id)
            .Build();

        Seed(person);
        Seed(workday);

        var requestModel = new StartWorkdayRequestModel()
        {
            UserId = person.Id
        };

        var httpContent = requestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PostAsync("workdays/actions/start", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task FinishWorkday_WithValidData_ReturnsWorkdayDtoAsync()
    {
        //Arrange
        var person = _personBuilder
            .WithRole(Role.Employee)
            .Build();

        var workday = _workdayBuilder
            .WithStartTime(_defaultDate)
            .WithPersonId(person.Id)
            .Build();

        Seed(person);
        Seed(workday);

        var requestModel = new FinishWorkdayRequestModel()
        {
            UserId = person.Id
        };

        var httpContent = requestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PostAsync("workdays/actions/finish", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var workdayDto = await JsonDeserializeHelper.DeserializeAsync<WorkdayDto>(response);
        var workdayDb = _dbContext.Workdays.First(w => w.Id == workdayDto.Id);

        workdayDto.Start.Should().Be(_defaultDate);
        workdayDto.Stop.Should().Be(_defaultDate);

        workdayDb.Id.Should().Be(workdayDto.Id);
        workdayDb.Start.Should().Be(workdayDto.Start);
        workdayDb.Stop.Should().Be(workdayDto.Stop);
        workdayDb.PersonId.Should().Be(person.Id);
    }

    [Fact]
    public async Task FinishWorkday_WithInvalidId_ReturnsBadRequestAsync()
    {
        //Arrange
        var requestModel = new FinishWorkdayRequestModel()
        {
            UserId = Guid.NewGuid()
        };

        var httpContent = requestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PostAsync("workdays/actions/finish", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task FinishWorkday_WithNoStartedWorkday_ReturnsBadRequestAsync()
    {
        //Arrange
        var person = _personBuilder
            .WithRole(Role.Employee)
            .Build();

        Seed(person);

        var requestModel = new FinishWorkdayRequestModel()
        {
            UserId = person.Id
        };

        var httpContent = requestModel.ToJsonHttpContent();

        //Act
        var response = await _client.PostAsync("workdays/actions/finish", httpContent);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetWorkday_ForValidId_ReturnsWorkdayDtoAsync()
    {
        //Arrange
        var person = _personBuilder
            .WithRole(Role.Employee)
            .Build();

        var workday = _workdayBuilder
            .WithPersonId(person.Id)
            .WithStartTime(_defaultDate)
            .Build();

        Seed(person);
        Seed(workday);

        //Act
        var response = await _client.GetAsync($"workdays/{workday.Id}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var workdayDto = await JsonDeserializeHelper.DeserializeAsync<WorkdayDto>(response);
        workdayDto.Start.Should().Be(_defaultDate);
        workdayDto.Stop.Should().Be(null);
    }

    [Fact]
    public async Task GetWorkday_ForInvalidId_ReturnsNotFoundAsync()
    {
        //Arrange
        var id = Guid.NewGuid();

        //Act
        var response = await _client.GetAsync($"workdays/{id}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
