using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.Domain;
using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Repositories;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;

namespace Wholesaler.Backend.Api.Controllers
{
    [ApiController]
    [Route("worktask")]
    public class WorkTaskController : ControllerBase
    {
        private readonly IUsersRepository _userRepository;
        private readonly IWorkTaskRepository _workTaskRepository;
        private readonly IWorkTaskService _workTaskService;

        public WorkTaskController(IUsersRepository userRepository, IWorkTaskRepository workTaskRepository, IWorkTaskService workTaskService)
        {
            _userRepository = userRepository;
            _workTaskRepository = workTaskRepository;
            _workTaskService = workTaskService;
        }

        [HttpPost]        
        public async Task<ActionResult<Guid>> Add([FromBody] AddTaskRequestModel addTask)
        {
            var task = new WorkTask(addTask.Row);

            var id = _workTaskRepository.Add(task);

            return id;
        }

        [HttpPost]
        [Route("actions/assign")]
        public async Task<ActionResult<WorkTaskDto>> Assign([FromBody] AssignTaskRequestModel assignTask)
        {            
            var workTask = _workTaskService.Assign(assignTask.WorkTaskId, assignTask.UserId);

            var workTaskDto = new WorkTaskDto()
            {
                Id = workTask.Id,
                UserId = assignTask.UserId,
            };

            return workTaskDto;
        }
    }
}
