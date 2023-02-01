using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Repositories;
using Wholesaler.Core.Dto.ResponseModels;

namespace Wholesaler.Backend.Api.Controllers
{
    [ApiController]
    [Route("employees")]
    public class EmployeesControler : ControllerBase
    {
        private readonly IUsersRepository _repository;
        public EmployeesControler(IUsersRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]        
        public async Task<ActionResult<List<UserDto>>> GetEmployees()
        {
            try
            {
                var employees = _repository.GetEmployees();

                var listOfEmployees = employees.Select(user =>
                {
                    var userDto = new UserDto()
                    {
                        Id = user.Id,
                        Login = user.Login,
                        Name = user.Name,
                        Surname = user.Surname,
                        Role = user.Role.ToString(),
                    };

                    return userDto;
                });

                return listOfEmployees.ToList();

            }
            catch (Exception ex)
            {
                if (ex is InvalidDataProvidedException)
                    return BadRequest(ex.Message);

                throw;
            }
        }
    }
}
