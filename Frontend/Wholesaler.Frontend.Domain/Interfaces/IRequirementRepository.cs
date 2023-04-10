using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Domain.ValueObjects;

namespace Wholesaler.Frontend.Domain.Interfaces
{
    public interface IRequirementRepository
    {
        Task<ExecutionResultGeneric<RequirementDto>> Add(int quantity, Guid clientId);
    }
}
