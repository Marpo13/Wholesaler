using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Factories.Interfaces;
using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Repositories;
using Wholesaler.Backend.Domain.Requests.Requirements;

namespace Wholesaler.Backend.Domain.Services
{
    public class RequirementService : IRequirementService
    {
        private readonly IRequirementFactory _factory;
        private readonly IRequirementRepository _repository;

        public RequirementService(IRequirementFactory requirementFactory, IRequirementRepository requirementRepository)
        {
            _factory = requirementFactory;
            _repository = requirementRepository;
        }

        public Requirement Add(CreateRequirementRequest request)
        {
            var requirement = _factory.Create(request);
            _repository.Add(requirement);

            return requirement;
        }

        public Requirement EditQuantity(Guid id, int quantity)
        {
            if (quantity < 0)
                throw new InvalidDataProvidedException("Quantity must be more than 0.");

            var requirement = _repository.GetOrDefault(id);
            if (requirement == null)
                throw new EntityNotFoundException($"There is no requirement with id {id}.");

            requirement.UpdateQuantity(quantity);
            _repository.Update(requirement);

            return requirement;
        }

        public List<Requirement> GetAll()
        {
            var requirements = _repository.GetAll();
            if (requirements == null)
                throw new EntityNotFoundException("There are no requirements in this base.");

            return requirements;
        }
    }
}
