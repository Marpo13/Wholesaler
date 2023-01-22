using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Domain.ValueObjects;


namespace Wholesaler.Frontend.Domain
{
    public interface IUserService
    {
        Task<ExecutionResultGeneric<UserDto>> TryLoginWithDataFromUserAsync(string loginFromUser, string passwordFromUser);
       
        Task<ExecutionResultGeneric<Guid>> StartWorkingAsync(Guid userId);

        Task<ExecutionResultGeneric<WorkdayDto>> GetWorkdayAsync(Guid workdayid);     

        Task<ExecutionResultGeneric<Guid>> FinishWorkingAsync(Guid userId);

        Task<ExecutionResultGeneric<WorkTaskDto>> AssignTask(Guid workTaskId, Guid userId);

        Task<ExecutionResultGeneric<List<WorkTaskDto>>> GetNotAssignWorkTasks();

        Task<ExecutionResultGeneric<List<UserDto>>> GetEmployees();

        Task<ExecutionResultGeneric<List<WorkTaskDto>>> GetAssignedTaskToAnEmployee(Guid userId);
    }
}
