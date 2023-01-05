using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.Domain;
using Wholesaler.Backend.Domain.Exceptions;
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
        [Route("start")]
        public async Task<ActionResult<Guid>> StartWorkday([FromBody] StartWorkdayRequestModel request)
        {
            try
            {
                var workdayId = _service.StartWorkday(request.UserId);
                return workdayId;
            }
            catch (Exception ex)
            {
                if (ex is InvalidDataProvidedException)
                    return BadRequest(ex.Message);

                throw;
            }
        }

        [HttpPost]
        [Route("finish")]
        public async Task<ActionResult<Guid>> FinishWorkday([FromBody] FinishWorkdayRequestModel request)
        {
            try
            {
                var workdayId = _service.FinishWorkday(request.UserId);
                return workdayId;
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
