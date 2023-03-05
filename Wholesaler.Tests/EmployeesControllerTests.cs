using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Wholesaler.Backend.DataAccess;
using Wholesaler.Backend.DataAccess.Models;
using Wholesaler.Core.Dto.ResponseModels;
using Xunit;

namespace Wholesaler.Tests
{
    public class EmployeesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public EmployeesControllerTests(WebApplicationFactory<Program> factory)
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

        private void SeedEmployee(Person person)
        {
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<WholesalerContext>();

            _dbContext.People.Add(person);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task Get_ForValidModel_ReturnsList()
        {
            //Arrange

            var person1 = new Person()
            {
                Id = new Guid("236ceb29-ce97-4e60-96ad-b4af546492a6"),
                Role = Backend.Domain.Entities.Role.Employee,
                Login = "Maria",
                Password = "Jamaria",
                Name = "Maria",
                Surname = "Awaria",
            };

            var person2 = new Person()
            {
                Id = new Guid("c32d8360-538b-460f-8f6f-13a8e962fda0"),
                Role = Backend.Domain.Entities.Role.Employee,
                Login = "Jacek",
                Password = "Jajacek",
                Name = "Jacek",
                Surname = "Placek",
            };

            SeedEmployee(person1);
            SeedEmployee(person2);

            //Act

            var response = await _client.GetAsync("employees");

            //Assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            
            var resultContent = await response.Content.ReadAsStringAsync();
            var responseJson = JsonConvert.DeserializeObject<List<UserDto>>(resultContent);

            var person1Check = responseJson.First(p => p.Id == person1.Id);
            Assert.True(person1Check.Role == "Employee");
            Assert.True(person1Check.Login == person1.Login);
            Assert.True(person1Check.Name == person1.Name);
            Assert.True(person1Check.Surname == person1.Surname);

            var person2Check = responseJson.First(p => p.Id == person1.Id);
            Assert.True(person2Check.Role == "Employee");
            Assert.True(person2Check.Login == person1.Login);
            Assert.True(person2Check.Name == person1.Name);
            Assert.True(person2Check.Surname == person1.Surname);
        }
    }
}
