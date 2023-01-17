using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.Domain;
using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Repositories;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;

namespace Wholesaler.Backend.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IUsersRepository _repository;

        public UsersController(IUserService service, IUsersRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        [HttpPost]
        [Route("actions/login")]
        public async Task<ActionResult<UserDto>> LoginAsync ([FromBody] LoginUserRequestModel loginUser)
        {
            try
            {
                var person = _service.Login(loginUser.Login, loginUser.Password);

                return Ok(new UserDto()
                {
                    Id = person.Id,
                    Login = person.Login,
                    Role = person.Role.ToString(),                    
                });
            }
            catch (Exception ex)
            {
                if (ex is InvalidDataProvidedException)
                    return BadRequest(ex.Message);

                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddPerson([FromBody] AddPersonReqestModel addPerson)
        {
            var person = new Person(addPerson.Login, addPerson.Password, Enum.Parse<Role>(addPerson.Role), addPerson.Name, addPerson.Surname);
            
            var id = _repository.AddPerson(person);

            return id;
        }        
    }
}