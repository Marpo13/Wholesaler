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
    [Route("worktasks")]
    public class WorkTaskController : ControllerBase
    {
        private readonly IWorkTaskRepository _workTaskRepository;
        private readonly IWorkTaskService _workTaskService;
        private readonly IUsersRepository _usersRepository;

        public WorkTaskController(IWorkTaskRepository workTaskRepository, IUsersRepository usersRepository, IWorkTaskService workTaskService)
        {
            _workTaskRepository = workTaskRepository;
            _usersRepository = usersRepository;
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
        [Route("{id}/actions/assign")]
        public async Task<ActionResult<WorkTaskDto>> Assign(Guid id, [FromBody] AssignTaskRequestModel assignTask)
        {
            try
            {
                var workTask = _workTaskService.Assign(id, assignTask.UserId);

                var workTaskDto = new WorkTaskDto()
                {
                    Id = workTask.Id,
                    Row = workTask.Row,
                    UserId = workTask.Person.Id,                    
                };

                return workTaskDto;
            }
            catch(Exception ex)
            {
                if(ex is InvalidDataProvidedException || ex is InvalidOperationException)
                    return BadRequest(ex.Message);

                throw;
            }
            
        }

        [HttpGet]
        [Route("unassigned")]
        public async Task<ActionResult<List<WorkTaskDto>>> GetNotAssignWorktasks()
        {
            try
            {
                var workday = _workTaskRepository.GetNotAssign();

                var listOfWorkTasks = workday.Select(workTask =>
                {
                    var workTaskDto = new WorkTaskDto()
                    {
                        Id = workTask.Id,
                        Row = workTask.Row,
                    };

                    return workTaskDto;
                });

                return listOfWorkTasks.ToList();
            }

            catch (Exception ex)
            {
                if (ex is InvalidDataProvidedException)
                    return BadRequest(ex.Message);

                throw;
            }
        }

        [HttpGet]
        [Route("action/assignedToAnEmployee")]
        public async Task<ActionResult<List<WorkTaskDto>>> GetAssignedToAnEmployee(Guid userId)
        {
            try
            {
                var worktasks = _workTaskRepository.GetAssigned(userId);

                var listOfWorktasksDto = worktasks.Select(workTask =>
                {
                    var workTaskDto = new WorkTaskDto()
                    {
                        Id = workTask.Id,
                        Row = workTask.Row, 
                        UserId = workTask.Person.Id,                        
                    };

                    return workTaskDto;
                });

                return listOfWorktasksDto.ToList();
            }
            catch(Exception ex)
            {
                if (ex is InvalidOperationException || ex is InvalidDataProvidedException)
                    return BadRequest(ex.Message);

                throw;
            }
        }

    }
}
