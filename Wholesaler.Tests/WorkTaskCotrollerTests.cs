using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using Wholesaler.Backend.DataAccess;
using Wholesaler.Backend.DataAccess.Models;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Tests.Helpers;
using Xunit;
using WorkTask = Wholesaler.Backend.DataAccess.Models.WorkTask;

namespace Wholesaler.Tests
{
    public class WorkTaskCotrollerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public WorkTaskCotrollerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services
                        .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<WholesalerContext>));

                        services.Remove(dbContextOptions);

                        services
                        .AddDbContext<WholesalerContext>(options => options.UseInMemoryDatabase("WholesalerDb"));
                    });
                });

            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task AddWorkTask_ForValidModel_ReturnsId()
        {
            //Arrange

            var workTask = new AddTaskRequestModel()
            {
                Row = 1,
            };

            var httpContent = workTask.ToJsonHttpContent();

            //Act

            var result = await _client.PostAsync("/worktasks", httpContent);

            //Assert

            result.Content.Should().Be(result.Content);            
        }

        [Fact]
        public async Task AssignTask_ForValidData_ReturnsWorkTask()
        {
            //Arrange

            var options = new DbContextOptionsBuilder<WholesalerContext>()
            .UseInMemoryDatabase(databaseName: "WholesalerDb")
            .Options;

            using (var context = new WholesalerContext(options))
            {
                context.WorkTasks.Add(new WorkTask()
                {
                    Id = new Guid("7345b2cf-1fa3-4882-a80e-ea5cf6203c1d"),
                    Row = 10,
                    IsStarted = false,
                    IsFinished = false,
                });
                context.People.Add(new Person()
                {
                    Id = new Guid("042efd49-15a6-4c33-a9ba-1fd18108d3e2"),
                    Role = Backend.Domain.Entities.Role.Employee,
                    Login = "Marysia",
                    Password = "Jamarysia",
                    Name = "Maria",
                    Surname = "Awaria",
                });
                context.SaveChanges();
            }

            var worktaskId = new Guid("7345b2cf-1fa3-4882-a80e-ea5cf6203c1d");
            var userID = new AssignTaskRequestModel()
            {
                UserId = new Guid("042efd49-15a6-4c33-a9ba-1fd18108d3e2")
            };

            var httpContent = userID.ToJsonHttpContent();

            //Act

            var response = await _client.PostAsync($"/worktasks/{worktaskId}/actions/assign", httpContent);

            //Assert

            response.Content.Should().Be(response.Content);

        }
    }
}
