using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Domain.ValueObjects;

namespace Wholesaler.Frontend.Domain.Interfaces
{
    public interface IRequirementRepository
    {
        Task<ExecutionResultGeneric<RequirementDto>> Add(int quantity, Guid clientId, Guid storageId);
        Task<ExecutionResultGeneric<RequirementDto>> EditQuantity(Guid id, int quantity);
        Task<ExecutionResultGeneric<List<RequirementDto>>> GetAllRequirements();
        Task<ExecutionResultGeneric<List<RequirementDto>>> GetRequirements(Guid storageId);
        Task<ExecutionResultGeneric<RequirementDto>> CompleteRequirement(Guid id);

    }
}
