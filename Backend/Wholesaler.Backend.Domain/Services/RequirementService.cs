using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Factories.Interfaces;
using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Repositories;
using Wholesaler.Backend.Domain.Requests.Requirements;

namespace Wholesaler.Backend.Domain.Services
{
    public class RequirementService : IRequirementService
    {
        private readonly IRequirementFactory _requirementFactory;
        private readonly IRequirementRepository _requirementRepository;

        public RequirementService(IRequirementFactory requirementFactory, IRequirementRepository requirementRepository)
        {
            _requirementFactory = requirementFactory;
            _requirementRepository = requirementRepository;
        }

        public Requirement Add(CreateRequirementRequest request)
        {
            var requirement = _requirementFactory.Create(request);
            _requirementRepository.Add(requirement);

            return requirement;
        }
    }
}
