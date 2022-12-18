using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.DataAccess.Repositories;
using Wholesaler.Backend.Domain;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Repositories;

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
        public async Task<IActionResult> LoginAsync (string login, string password)
        {
            try
            {
                _service.LogByLogin(login, password);

                return Ok();
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