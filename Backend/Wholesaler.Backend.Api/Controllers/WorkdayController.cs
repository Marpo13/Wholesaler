using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Repositories;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;

namespace Wholesaler.Backend.Api.Controllers
{
    [ApiController]
    [Route("workdays")]
    public class WorkdayController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IWorkdayRepository _workdayRepository;

        public WorkdayController(IUserService service, IWorkdayRepository workdayRepository)
        {
            _service = service;
            _workdayRepository = workdayRepository;
        }

        [HttpPost]
        [Route("actions/start")]
        public async Task<ActionResult<Guid>> StartWorkdayAsync([FromBody] StartWorkdayRequestModel request)
        {
            var workday = _service.StartWorkday(request.UserId);

            return Ok(new WorkdayDto()
            {
                Id = workday.Id,
                Start = workday.Start,
                Stop = workday.Stop,
            });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<WorkdayDto>> GetWorkdayAsync(Guid id)
        {
            var workday = _workdayRepository.GetOrDefault(id);

            return Ok(new WorkdayDto()
            {
                Id = workday.Id,
                Start = workday.Start,
                Stop = workday.Stop,
            });
        }

        [HttpPost]
        [Route("actions/finish")]
        public async Task<ActionResult<WorkdayDto>> FinishWorkdayAsync([FromBody] FinishWorkdayRequestModel request)
        {
            var workday = _service.FinishWorkday(request.UserId);

            return Ok(new WorkdayDto()
            {
                Id = workday.Id,
                Start = workday.Start,
                Stop = workday.Stop,
            });
        }
    }
}
