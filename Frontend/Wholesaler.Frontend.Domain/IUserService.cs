
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Domain.ValueObjects;
using static Wholesaler.Frontend.Domain.ValueObjects.ExecutionResultGeneric;


namespace Wholesaler.Frontend.Domain
{
    public interface IUserService
    {
        Task<ExecutionResult<UserDto>> TryLoginWithDataFromUserAsync(string loginFromUser, string passwordFromUser);
       
        Task<ExecutionResult> StartWorking(Guid userId);

        Task<ExecutionResult> FinishWorking(Guid userId);

    }
}
