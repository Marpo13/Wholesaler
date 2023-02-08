using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Domain.ValueObjects;

namespace Wholesaler.Frontend.Domain.Interfaces
{
    public interface IWorkTaskRepository
    {
        Task<ExecutionResultGeneric<List<WorkTaskDto>>> GetNotAssignWorkTasks();

        Task<ExecutionResultGeneric<List<WorkTaskDto>>> GetAssignedTask();

        Task<ExecutionResultGeneric<List<WorkTaskDto>>> GetAssignedTaskToAnEmployee(Guid userId);
    }
}
