using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Requests.Requirements;
using Wholesaler.Backend.Domain.Responses.Requirements;

namespace Wholesaler.Backend.Domain.Interfaces
{
    public interface IRequirementService
    {
        Requirement Add(CreateRequirementRequest request);
        Requirement EditQuantity(Guid id, int quantity);
        Requirement Complete(Guid id);
        Task<GetByCustomFiltersResponse> GetByCustomFiltersAsync(Dictionary<string, string> customFilters);
    }
}
