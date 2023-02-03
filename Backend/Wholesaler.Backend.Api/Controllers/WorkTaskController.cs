using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Repositories;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;

namespace Wholesaler.Backend.Api.Controllers
{
    [ApiController]
    [Route("worktask")]
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
        [Route("actions/assign")]
        public async Task<ActionResult<WorkTaskDto>> Assign([FromBody] AssignTaskRequestModel assignTask)
        {
            try
            {
                var workTask = _workTaskService.Assign(assignTask.WorkTaskId, assignTask.UserId);

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

        [HttpPatch]
        [Route("actions/changeOwner/{workTaskId}")]
        public async Task<ActionResult<WorkTaskDto>> ChangeOwnerOfWorkTask(Guid workTaskId, [FromBody] ChangeOwnerRequestModel changeOwner)
        {
            try
            {
                var workTask = _workTaskService.ChangeOwner(workTaskId, changeOwner.NewOwnerId);

                var workTaskDto = new WorkTaskDto()
                {
                    Id = workTask.Id,
                    Row = workTask.Row,
                    UserId = workTask.Person.Id,
                };

                return workTaskDto;
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException || ex is InvalidDataProvidedException)
                    return BadRequest(ex.Message);

                throw;
            }
        }

        [HttpPost]
        [Route("actions/start/{workTaskId}")]
        public async Task<ActionResult<WorkTaskDto>> StartWorkTask(Guid workTaskId)
        {
            try
            {
                var workTask = _workTaskService.Start(workTaskId);

                var workTaskDto = new WorkTaskDto()
                {
                    Id = workTask.Id,
                    Row = workTask.Row,
                    UserId = workTask.Person.Id,
                    Start = workTask.Start,
                    Stop = workTask.Stop,
                };

                return workTaskDto;

            }
            catch(Exception ex)
            {
                if (ex is InvalidOperationException || ex is InvalidDataProvidedException)
                    return BadRequest(ex.Message);

                throw;
            }
        }

        [HttpPost]
        [Route("actions/finish/{workTaskId}")]
        public async Task<ActionResult<WorkTaskDto>> FinishWorkTask(Guid workTaskId)
        {
            try
            {
                var workTask = _workTaskService.Stop(workTaskId);

                var workTaskDto = new WorkTaskDto()
                {
                    Id = workTask.Id,
                    Row = workTask.Row,
                    UserId = workTask.Person.Id,
                    Start = workTask.Start,
                    Stop = workTask.Stop,
                };

                return workTaskDto;

            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException || ex is InvalidDataProvidedException)
                    return BadRequest(ex.Message);

                throw;
            }
        }

        [HttpGet]
        [Route("actions/getNotAssigned")]
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
        [Route("actions/getAssigned")]
        public async Task<ActionResult<List<WorkTaskDto>>> GetAssignWorktasks()
        {
            try
            {
                var workday = _workTaskRepository.GetAssign();

                var listOfWorkTasks = workday.Select(workTask =>
                {
                    var workTaskDto = new WorkTaskDto()
                    {
                        Id = workTask.Id,
                        Row = workTask.Row,
                        UserId = workTask.Person.Id,
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
        [Route("actions/getAssignedToAnEmployee")]
        public async Task<ActionResult<List<WorkTaskDto>>> GetAssignedToAnEmployee(Guid userId)
        {
            try
            {
                var person = _usersRepository.Get(userId);
                var worktasks = _workTaskRepository.GetAssign(userId);

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
