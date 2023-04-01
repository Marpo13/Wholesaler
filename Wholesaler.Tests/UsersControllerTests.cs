using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Wholesaler.Backend.DataAccess.Models;
using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Tests.Builders;
using Wholesaler.Tests.Helpers;
using Xunit;

namespace Wholesaler.Tests
{
    public class UsersControllerTests : WholesalerWebTest
    {
        private readonly PersonBuilder _personBuilder;

        public UsersControllerTests(WebApplicationFactory<Program> factory) : base(factory)
        {
            _personBuilder = new PersonBuilder();
        }

        [Fact]
        public async Task Login_WithValidData_ReturnsUserDto()
        {
            //Arrange

            var person = _personBuilder
                .WithRole(Role.Employee)
                .Build();

            Seed(person);

            var loginRequestModel = new LoginUserRequestModel()
            {
                Login = person.Login,
                Password = person.Password
            };

            var httpContent = loginRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PostAsync("users/actions/login", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var personFromResponse = await JsonDeserializeHelper.DeserializeAsync<UserDto>(response);

            personFromResponse.Id.Should().Be(person.Id);
            personFromResponse.Login.Should().Be(person.Login);
            personFromResponse.Role.Should().Be(person.Role.ToString());
            personFromResponse.Name.Should().Be(person.Name);
            personFromResponse.Surname.Should().Be(person.Surname);
        }

        [Fact]
        public async Task Login_WithInvalidData_ReturnsBadRequest()
        {
            //Arrange

            var loginRequestModel = new LoginUserRequestModel()
            {
                Login = "Magdalena",
                Password = "Jamagdalena",
            };

            var httpContent = loginRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PostAsync("users/actions/login", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Login_WithInvalidPassword_ReturnsBadRequest()
        {
            //Arrange

            var person = _personBuilder
                .WithRole(Role.Employee)
                .Build();

            Seed(person);

            var loginRequestModel = new LoginUserRequestModel()
            {
                Login = person.Login,
                Password = "JaHenryk"
            };

            var httpContent = loginRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PostAsync("users/actions/login", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task AddPerson_WithValidModel_ReturnsUserDto()
        {
            //Arrange

            var addRequestModel = new AddPersonReqestModel()
            {
                Name = "Łucja",
                Surname = "Absolucja",
                Role = "Manager",
                Login = "Łucja",
                Password = "Jałucja",
            };

            var httpContent = addRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PostAsync("users", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var userDto = await JsonDeserializeHelper.DeserializeAsync<UserDto>(response);
            var personDb = _dbContext.People.First(p => p.Login == addRequestModel.Login);

            personDb.Id.Should().Be(userDto.Id);
            personDb.Name.Should().Be(addRequestModel.Name);
            personDb.Surname.Should().Be(addRequestModel.Surname);
            personDb.Role.ToString().Should().Be(addRequestModel.Role);
            personDb.Login.Should().Be(addRequestModel.Login);
            personDb.Password.Should().Be(addRequestModel.Password);

            userDto.Name.Should().Be(addRequestModel.Name);
            userDto.Surname.Should().Be(addRequestModel.Surname);
            userDto.Role.ToString().Should().Be(addRequestModel.Role);
            userDto.Login.Should().Be(addRequestModel.Login);
        }

        [Fact]
        public async Task AddPerson_WithNoPassword_ReturnsBadRequest()
        {
            //Arrange

            var addRequestModel = new AddPersonReqestModel()
            {
                Name = "Maria",
                Surname = "Awaria",
                Role = "Manager",
                Login = "Maria",
            };

            var httpContent = addRequestModel.ToJsonHttpContent();

            //Act

            var response = await _client.PostAsync("users", httpContent);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
