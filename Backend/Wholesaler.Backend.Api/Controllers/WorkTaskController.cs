using Microsoft.AspNetCore.Mvc;
using Wholesaler.Backend.Api.Factories;
using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Repositories;
using Wholesaler.Backend.Domain.Requests.WorkTasks;
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
        private readonly IWorkTaskDtoFactory _workTaskFactory;

        public WorkTaskController(IWorkTaskRepository workTaskRepository, IWorkTaskService workTaskService, IWorkTaskDtoFactory workTaskFactory)
        {
            _workTaskRepository = workTaskRepository;
            _workTaskService = workTaskService;
            _workTaskFactory = workTaskFactory;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Add([FromBody] AddTaskRequestModel addTask)
        {
            var request = new CreateWorkTaskRequest(addTask.Row);
            var workTask = _workTaskService.Add(request);

            return workTask.Id;
        }

        [HttpPost]
        [Route("{id}/actions/assign")]
        public async Task<ActionResult<WorkTaskDto>> Assign(Guid id, [FromBody] AssignTaskRequestModel assignTask)
        {
            var workTask = _workTaskService.Assign(id, assignTask.UserId);

            var workTaskDto = _workTaskFactory.Create(workTask);

            return workTaskDto;
        }

        [HttpPatch]
        [Route("{workTaskId}/actions/changeOwner")]
        public async Task<ActionResult<WorkTaskDto>> ChangeOwnerOfWorkTask(Guid workTaskId, [FromBody] ChangeOwnerRequestModel changeOwner)
        {
            var workTask = _workTaskService.ChangeOwner(workTaskId, changeOwner.NewOwnerId);

            var workTaskDto = _workTaskFactory.Create(workTask);

            return workTaskDto;
        }

        [HttpPost]
        [Route("{workTaskId}/actions/start")]
        public async Task<ActionResult<WorkTaskDto>> StartWorkTask(Guid workTaskId)
        {
            var workTask = _workTaskService.Start(workTaskId);

            var workTaskDto = _workTaskFactory.Create(workTask);

            return workTaskDto;
        }

        [HttpPost]
        [Route("{workTaskId}/actions/stop")]
        public async Task<ActionResult<WorkTaskDto>> StopWorkTask(Guid workTaskId)
        {
            var workTask = _workTaskService.Stop(workTaskId);

            var workTaskDto = _workTaskFactory.Create(workTask);

            return workTaskDto;
        }

        [HttpPost]
        [Route("{workTaskId}/actions/finish")]
        public async Task<ActionResult<WorkTaskDto>> FinishWorkTask(Guid workTaskId)
        {
            var workTask = _workTaskService.Finish(workTaskId);

            var workTaskDto = _workTaskFactory.Create(workTask);

            return workTaskDto;
        }

        [HttpGet]
        [Route("unassigned")]
        public async Task<ActionResult<List<WorkTaskDto>>> GetNotAssignWorktasks()
        {
            var workday = _workTaskRepository.GetNotAssign();

            var listOfWorkTasks = workday.Select(workTask =>
            {
                var workTaskDto = _workTaskFactory.Create(workTask);

                return workTaskDto;
            });

            return listOfWorkTasks.ToList();
        }

        [HttpGet]
        [Route("assigned")]
        public async Task<ActionResult<List<WorkTaskDto>>> GetAssignedWorktasks()
        {
            var workday = _workTaskRepository.GetAssigned();

            var listOfWorkTasks = workday.Select(workTask =>
            {
                var workTaskDto = _workTaskFactory.Create(workTask);

                return workTaskDto;
            });

            return listOfWorkTasks.ToList();
        }

        [HttpGet]
        [Route("assignedToAnEmployee")]
        public async Task<ActionResult<List<WorkTaskDto>>> GetAssignedToAnEmployee(Guid userId)
        {
            var workTasks = _workTaskRepository.GetAssigned(userId);

            var listOfWorktasksDto = workTasks.Select(workTask =>
            {
                var workTaskDto = _workTaskFactory.Create(workTask);

                return workTaskDto;
            });

            return listOfWorktasksDto.ToList();
        }

        [HttpGet]
        [Route("started")]
        public async Task<ActionResult<List<WorkTaskDto>>> GetStartedWorkTasks()
        {
            var workTasks = _workTaskRepository.GetStarted();

            var listOfWorktasksDto = workTasks.Select(workTask =>
            {
                var workTaskDto = _workTaskFactory.Create(workTask);

                return workTaskDto;
            });

            return listOfWorktasksDto.ToList();
        }

        [HttpGet]
        [Route("finished")]
        public async Task<ActionResult<List<WorkTaskDto>>> GetFinishedWorkTasks()
        {
            var workTasks = _workTaskRepository.GetFinished();

            var listOfWorktasksDto = workTasks.Select(workTask =>
            {
                var workTaskDto = _workTaskFactory.Create(workTask);

                return workTaskDto;
            });

            return listOfWorktasksDto.ToList();
        }
    }
}
