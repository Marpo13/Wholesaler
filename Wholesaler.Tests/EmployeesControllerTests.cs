using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Tests.Builders;
using Wholesaler.Tests.Helpers;
using Xunit;

namespace Wholesaler.Tests
{
    public class EmployeesControllerTests : WholesalerWebTest
    {
        private readonly PersonBuilder _personBuilder;

        public EmployeesControllerTests(WebApplicationFactory<Program> factory) : base(factory)
        {
            _personBuilder = new PersonBuilder();
        }

        [Fact]
        public async Task Get_ForValidModel_ReturnsList()
        {
            //Arrange

            var person1 = _personBuilder.Build();
            var person2 = _personBuilder.Build();

            Seed(person1);
            Seed(person2);

            //Act

            var response = await _client.GetAsync("employees");

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var peopleFromResponse = await JsonDeserializeHelper.DeserializeAsync<List<UserDto>>(response);

            var person1Check = peopleFromResponse.First(p => p.Id == person1.Id);
            person1Check.Role.Should().Be(person1.Role.ToString());
            person1Check.Login.Should().Be(person1.Login);
            person1Check.Name.Should().Be(person1.Name);
            person1Check.Surname.Should().Be(person1.Surname);

            var person2Check = peopleFromResponse.First(p => p.Id == person2.Id);
            person2Check.Role.Should().Be(person2.Role.ToString());
            person2Check.Login.Should().Be(person2.Login);
            person2Check.Name.Should().Be(person2.Name);
            person2Check.Surname.Should().Be(person2.Surname);
        }
    }
}
