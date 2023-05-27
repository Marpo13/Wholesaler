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
        Requirement Complete(Requirement requirement);
    }
}
