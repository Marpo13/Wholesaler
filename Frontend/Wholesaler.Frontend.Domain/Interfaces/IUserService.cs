using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Domain.ValueObjects;


namespace Wholesaler.Frontend.Domain.Interfaces
{
    public interface IUserService
    {
        Task<ExecutionResultGeneric<UserDto>> TryLoginWithDataFromUserAsync(string loginFromUser, string passwordFromUser);

        Task<ExecutionResultGeneric<Guid>> StartWorkingAsync(Guid userId);        

        Task<ExecutionResultGeneric<Guid>> FinishWorkingAsync(Guid userId);

        Task<ExecutionResultGeneric<WorkTaskDto>> AssignTask(Guid workTaskId, Guid userId);      
        
    }
}
