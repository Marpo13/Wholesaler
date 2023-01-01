using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.Domain;
using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Core.Dto.RequestModels;

namespace Wholesaler.Backend.Api.Controllers
{
    [ApiController]
    [Route("workday")]
    public class WorkdayController : ControllerBase
    {
        private readonly IUserService _service;

        public WorkdayController(IUserService service)
        {
            _service = service;
        }
        
        [HttpPost]
        public async Task<ActionResult<Guid>> StartWorkday([FromBody] StartWorkdayRequestModel request)
        {
            var workdayId = _service.StartWorkday(request.UserId);

            return workdayId;
        }
    }
}
