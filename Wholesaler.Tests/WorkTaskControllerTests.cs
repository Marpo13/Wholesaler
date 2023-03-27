using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Tests.Builders;
using Wholesaler.Tests.Helpers;
using Xunit;
using Role = Wholesaler.Backend.Domain.Entities.Role;

namespace Wholesaler.Tests
{
    public class WorkTaskControllerTests : WholesalerWebTest
    {
        private readonly PersonBuilder _personBuilder;
        private readonly WorkTaskBuilder _workTaskBuilder;
        private readonly WorkdayBuilder _workdayBuilder;
        private readonly DateTime _defaultDate = new(2023, 02, 13, 12, 0, 0);

        public WorkTaskControllerTests(WebApplicationFactory<Program> factory) : base(factory)
        {
            _personBuilder = new PersonBuilder();
            _workTaskBuilder = new WorkTaskBuilder();
            _workdayBuilder = new WorkdayBuilder();

            _timeProviderMock
                .Setup(m => m.Now())
                .Returns(_defaultDate);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(8)]
        [InlineData(12)]
        [InlineData(50)]
        [InlineData(125)]
        public async Task Add_WithValidModel_ReturnsWorkTaskId(int rowNumber)
        {
            //Arrange

            var workTaskRequestModel = new AddTaskRequestModel()
            {
                Row = rowNumber
            };

            var httpContent = workTaskRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PostAsync("worktasks", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var workTaskId = await JsonDeserializeHelper.DeserializeAsync<Guid>(response);

            var workTask = _dbContext.WorkTasks.First(w => w.Row == rowNumber);
            workTask.Id.Should().Be(workTaskId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        [InlineData(-100)]
        public async Task Add_WithInvalidModel_ReturnsBadRequest(int row)
        {
            //Arrange

            var workTaskRequestModel = new AddTaskRequestModel()
            {
                Row = row
            };

            var httpContent = workTaskRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PostAsync("worktasks", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Assign_WithValidModel_ReturnsWorkTaskDto()
        {
            //Arrange

            var person = _personBuilder
                .WithRole(Role.Employee)
                .Build();

            var workTask = _workTaskBuilder
                .WithRow(2)
                .Build();

            Seed(person);
            Seed(workTask);

            var assignTaskRequestModel = new AssignTaskRequestModel()
            {
                UserId = person.Id
            };

            var httpContent = assignTaskRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PostAsync($"worktasks/{workTask.Id}/actions/assign", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var workTaskDto = await JsonDeserializeHelper.DeserializeAsync<WorkTaskDto>(response);
            workTaskDto.Row.Should().Be(workTask.Row);
            workTaskDto.IsStarted.Should().Be(workTask.IsStarted);
            workTaskDto.IsFinished.Should().Be(workTask.IsFinished);
            workTaskDto.UserId.Should().Be(workTask.PersonId);

            var workTaskDb = _dbContext.WorkTasks.First(w => w.Id == workTask.Id);
            workTaskDb.Row.Should().Be(workTask.Row);
            workTaskDb.IsStarted.Should().Be(workTask.IsStarted);
            workTaskDb.IsFinished.Should().Be(workTask.IsFinished);
            workTaskDb.PersonId.Should().Be(workTask.PersonId);
        }

        [Fact]
        public async Task Assign_WithAssignedTaskTOTheSamePerson_ReturnsBadRequest()
        {
            //Arrange

            var person = _personBuilder
                .WithRole(Role.Employee)
                .Build();

            var workTask = _workTaskBuilder
                .WithRow(3)
                .WithPersonId(person.Id)
                .Build();

            Seed(person);
            Seed(workTask);

            var assignTaskRequestModel = new AssignTaskRequestModel()
            {
                UserId = person.Id
            };

            var httpContent = assignTaskRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PostAsync($"worktasks/{workTask.Id}/actions/assign", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Assign_WithManagerRole_ReturnsForbidden()
        {
            //Arrange

            var person = _personBuilder
                .WithRole(Role.Manager)
                .Build();

            var workTask = _workTaskBuilder
                .WithRow(4)
                .Build();

            Seed(person);
            Seed(workTask);

            var assignTaskRequestModel = new AssignTaskRequestModel()
            {
                UserId = person.Id
            };

            var httpContent = assignTaskRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PostAsync($"worktasks/{workTask.Id}/actions/assign", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Assign_WithOwnerRole_ReturnsForbidden()
        {
            //Arrange

            var person = _personBuilder
                .WithRole(Role.Owner)
                .Build();

            var workTask = _workTaskBuilder
                .WithRow(5)
                .Build();

            Seed(person);
            Seed(workTask);

            var assignTaskRequestModel = new AssignTaskRequestModel()
            {
                UserId = person.Id
            };

            var httpContent = assignTaskRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PostAsync($"worktasks/{workTask.Id}/actions/assign", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Assign_WithInvalidWorkTask_ReturnsNotFound()
        {
            //Arrange

            var person = _personBuilder
                .WithRole(Role.Employee)
                .Build();

            Seed(person);

            var assignTaskRequestModel = new AssignTaskRequestModel()
            {
                UserId = person.Id
            };

            var httpContent = assignTaskRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PostAsync($"worktasks/{Guid.NewGuid()}/actions/assign", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ChangeOwner_WithValidData_ReturnsWorkTaskDto()
        {
            //Arrange

            var person1 = _personBuilder
                .WithRole(Role.Employee)
                .Build();

            var person2 = _personBuilder
                .WithRole(Role.Employee)
                .Build();

            var workTask = _workTaskBuilder
                .WithRow(6)
                .WithPersonId(person1.Id)
                .Build();

            Seed(person1);
            Seed(person2);
            Seed(workTask);

            var assignTaskRequestModel = new ChangeOwnerRequestModel()
            {
                NewOwnerId = person2.Id
            };

            var httpContent = assignTaskRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PatchAsync($"worktasks/{workTask.Id}/actions/changeOwner", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var workTaskDto = await JsonDeserializeHelper.DeserializeAsync<WorkTaskDto>(response);
            workTaskDto.UserId.Should().Be(person2.Id);

            var workTaskDb = _dbContext.WorkTasks.First(w => w.Id == workTask.Id);
            workTaskDb.PersonId.Should().Be(person2.Id);
        }

        [Fact]
        public async Task ChangeOwner_WithValidModel_ReturnsBadRequest()
        {
            //Arrange

            var person1 = _personBuilder
                .WithRole(Role.Employee)
                .Build();

            var person2 = _personBuilder
                .WithRole(Role.Employee)
                .Build();

            var workTask = _workTaskBuilder
                .WithRow(7)
                .WithPersonId(person1.Id)
                .Build();

            Seed(person1);
            Seed(person2);
            Seed(workTask);

            var assignTaskRequestModel = new ChangeOwnerRequestModel()
            {
                NewOwnerId = person2.Id
            };

            var httpContent = assignTaskRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PatchAsync($"worktasks/{workTask.Id}/actions/changeOwner", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var workTaskDto = await JsonDeserializeHelper.DeserializeAsync<WorkTaskDto>(response);
            workTaskDto.UserId.Should().Be(person2.Id);

            var workTaskDb = _dbContext.WorkTasks.First(w => w.Id == workTask.Id);
            workTaskDb.PersonId.Should().Be(person2.Id);
        }

        [Fact]
        public async Task ChangeOwner_WithNotAssignedTask_ReturnsBadRequest()
        {
            //Arrange

            var person2 = _personBuilder
                .WithRole(Role.Employee)
                .Build();

            var workTask = _workTaskBuilder
                .WithRow(8)
                .Build();

            Seed(person2);
            Seed(workTask);

            var assignTaskRequestModel = new ChangeOwnerRequestModel()
            {
                NewOwnerId = person2.Id
            };

            var httpContent = assignTaskRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PatchAsync($"worktasks/{workTask.Id}/actions/changeOwner", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ChangeOwner_WithInvalidRoleOfPerson_ReturnsForbidden()
        {
            //Arrange

            var person1 = _personBuilder
                .WithRole(Role.Employee)
                .Build();

            var person2 = _personBuilder
                .WithRole(Role.Manager)
                .Build();

            var workTask = _workTaskBuilder
                .WithRow(9)
                .WithPersonId(person1.Id)
                .Build();

            Seed(person1);
            Seed(person2);
            Seed(workTask);

            var assignTaskRequestModel = new ChangeOwnerRequestModel()
            {
                NewOwnerId = person2.Id
            };

            var httpContent = assignTaskRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PatchAsync($"worktasks/{workTask.Id}/actions/changeOwner", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
                
        [Fact]
        public async Task StartWorkTask_WithValidModel_ReturnsWorkTaskDto()
        {
            //Arrange

            var person = _personBuilder
                .WithRole(Role.Employee)
                .Build();

            var workTask = _workTaskBuilder
                .WithRow(10)
                .WithPersonId(person.Id)
                .Build();

            var workday = _workdayBuilder
                .WithStartTime(_defaultDate)
                .WithPersonId(person.Id)
                .Build();

            Seed(person);
            Seed(workTask);
            Seed(workday);

            //Act

            var response = await _client.PostAsync($"worktasks/{workTask.Id}/actions/start", null);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var workTaskDto = await JsonDeserializeHelper.DeserializeAsync<WorkTaskDto>(response);
            workTaskDto.UserId.Should().Be(person.Id);
            workTaskDto.IsStarted.Should().Be(true);
            workTaskDto.IsFinished.Should().Be(false);

            var activityDb = _dbContext.Activities.First(a => a.WorkTaskId == workTask.Id);
            activityDb.Start.Should().Be(_defaultDate);
            activityDb.Stop.Should().Be(null);
            activityDb.PersonId.Should().Be(person.Id);

            var workTaskDb = _dbContext.WorkTasks.First(w => w.Id == workTask.Id);
            workTaskDb.PersonId.Should().Be(person.Id);
            workTaskDb.IsStarted.Should().Be(true);
            workTaskDb.IsFinished.Should().Be(false);
            workTaskDb.Activities.Should().HaveCount(1);
        }

        [Fact]
        public async Task StartWorkTask_WithNotAssignedTask_ReturnsBadRequest()
        {
            //Arrange

            var workTask = _workTaskBuilder
                .WithRow(11)
                .Build();

            Seed(workTask);

            //Act

            var response = await _client.PostAsync($"worktasks/{workTask.Id}/actions/start", null);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task StartWorkTask_WithStartedTask_ReturnsForbidden()
        {
            //Arrange

            var person = _personBuilder
               .WithRole(Role.Employee)
               .Build();

            var workTask = _workTaskBuilder
                .WithRow(12)
                .WithPersonId(person.Id)
                .WithStartedStatus()
                .Build();

            Seed(person);
            Seed(workTask);

            //Act

            var response = await _client.PostAsync($"worktasks/{workTask.Id}/actions/start", null);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task StartWorkTask_WithNotStartedWork_ReturnsForbidden()
        {
            //Arrange

            var person = _personBuilder
               .WithRole(Role.Employee)
               .Build();

            var workTask = _workTaskBuilder
                .WithRow(13)
                .WithPersonId(person.Id)
                .WithStartedStatus()
                .Build();

            Seed(person);
            Seed(workTask);

            //Act

            var response = await _client.PostAsync($"worktasks/{workTask.Id}/actions/start", null);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }
}
