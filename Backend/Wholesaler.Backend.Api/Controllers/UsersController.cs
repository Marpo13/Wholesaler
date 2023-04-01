using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.Api.Factories;
using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Requests.People;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;

namespace Wholesaler.Backend.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IUserDtoFactory _userFactory;

        public UsersController(IUserService service, IUserDtoFactory userFactory)
        {
            _service = service;
            _userFactory = userFactory;
        }

        [HttpPost]
        [Route("actions/login")]
        public async Task<ActionResult<UserDto>> LoginAsync([FromBody] LoginUserRequestModel loginUser)
        {
            var person = _service.Login(loginUser.Login, loginUser.Password);
            var userDto = _userFactory.Create(person);

            return userDto;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> AddPerson([FromBody] AddPersonReqestModel addPerson)
        {
            var request = new CreatePersonRequest(addPerson.Name, addPerson.Surname, addPerson.Role, addPerson.Login, addPerson.Password);
            var person = _service.Add(request);
            var userDto = _userFactory.Create(person);

            return userDto;
        }
    }
}