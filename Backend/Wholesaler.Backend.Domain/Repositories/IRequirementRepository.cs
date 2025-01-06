using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Repositories
{
    public interface IRequirementRepository
    {
        Requirement Add(Requirement requirement);
        Requirement? GetOrDefault(Guid id);
        Requirement Update(Requirement requirement);
        List<Requirement> GetAll();
        List<Requirement> Get(Guid storageId);
        List<Requirement> GetByStatus(string status);
        Task<List<Requirement>> GetByCustomFiltersAsync(Dictionary<string, string> customFilters);
    }
}
