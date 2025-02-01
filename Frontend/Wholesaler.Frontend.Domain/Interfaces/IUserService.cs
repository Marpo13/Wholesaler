using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Domain.ValueObjects;

namespace Wholesaler.Frontend.Domain.Interfaces;

public interface IUserService
{
    Task<ExecutionResultGeneric<UserDto>> TryLoginWithDataFromUserAsync(string loginFromUser, string passwordFromUser);

    Task<ExecutionResultGeneric<WorkdayDto>> StartWorkingAsync(Guid userId);

    Task<ExecutionResultGeneric<WorkdayDto>> FinishWorkingAsync(Guid userId);

    Task<ExecutionResultGeneric<WorkTaskDto>> AssignTaskAsync(Guid workTaskId, Guid userId);

    Task<ExecutionResultGeneric<WorkTaskDto>> ChangeOwnerAsync(Guid workTaskId, Guid newOwnerId);

    Task<ExecutionResultGeneric<WorkTaskDto>> StartWorkTaskAsync(Guid workTaskId);

    Task<ExecutionResultGeneric<WorkTaskDto>> StopWorkTaskAsync(Guid workTaskId);

    Task<ExecutionResultGeneric<WorkTaskDto>> FinishWorkTaskAsync(Guid workTaskId);
}
