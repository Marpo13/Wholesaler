using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.Domain;

namespace Wholesaler.Backend.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
                
        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> LoginAsync (string login, string password)
        {
            _service.LogByLogin(login, password);

            return Ok();
        }
    }
}