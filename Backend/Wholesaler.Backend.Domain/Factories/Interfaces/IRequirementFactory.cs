using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Requests.Requirements;

namespace Wholesaler.Backend.Domain.Factories.Interfaces
{
    public interface IRequirementFactory
    {
        Requirement Create(CreateRequirementRequest request);
    }
}
