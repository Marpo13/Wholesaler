using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Repositories
{
    public interface IRequirementRepository
    {
        Requirement Add(Requirement requirement);
    }
}
