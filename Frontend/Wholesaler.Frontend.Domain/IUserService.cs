
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Domain.ValueObjects;


namespace Wholesaler.Frontend.Domain
{
    public interface IUserService
    {
        Task<ExecutionResultGeneric<UserDto>> TryLoginWithDataFromUserAsync(string loginFromUser, string passwordFromUser);
       
        Task<ExecutionResultGeneric<Guid>> StartWorkingAsync(Guid userId);

        Task<ExecutionResultGeneric<WorkdayDto>> GetWorkdayAsync(Guid workdayid);
     

        Task<ExecutionResult> FinishWorking(Guid userId);

    }
}
