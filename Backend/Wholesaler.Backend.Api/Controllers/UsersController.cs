using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.DataAccess.Repositories;
using Wholesaler.Backend.Domain;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Repositories;
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

        [HttpGet]
        public async Task<ActionResult<UserDto>> LoginAsync (string login, string password)
        {
            try
            {
                var person = _service.LogByLogin(login, password);

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
        public async Task<ActionResult<Guid>> AddPerson()
        {
            var id = _repository.AddPerson();

            return id;
        }
    }
}