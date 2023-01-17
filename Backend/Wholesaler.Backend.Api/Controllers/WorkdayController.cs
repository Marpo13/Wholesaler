using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.Domain;
using Wholesaler.Backend.Domain.Exceptions;
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
            try
            {
                var workday = _service.StartWorkday(request.UserId);

                return workday.Id;
            }
            catch (Exception ex)
            {
                if (ex is InvalidDataProvidedException)
                    return BadRequest(ex.Message);

                throw;
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<WorkdayDto>> GetWorkdayAsync(Guid id)
        {
            try
            {
                var workday = _workdayRepository.GetOrDefault(id);

                return Ok(new WorkdayDto()
                {
                    Id = workday.Id,
                    Start = workday.Start,
                    Stop = workday.Stop,
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
        [Route("actions/finish")]
        public async Task<ActionResult<Guid>> FinishWorkdayAsync([FromBody] FinishWorkdayRequestModel request)
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
