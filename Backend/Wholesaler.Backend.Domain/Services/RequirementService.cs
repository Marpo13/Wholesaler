using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Factories.Interfaces;
using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Providers.Interfaces;
using Wholesaler.Backend.Domain.Repositories;
using Wholesaler.Backend.Domain.Requests.Requirements;

namespace Wholesaler.Backend.Domain.Services
{
    public class RequirementService : IRequirementService
    {
        private readonly IRequirementFactory _factory;
        private readonly IRequirementRepository _repository;
        private readonly IStorageService _storageService;
        private readonly ITimeProvider _timeProvider;

        public RequirementService(IRequirementFactory requirementFactory, IRequirementRepository requirementRepository, IStorageService storageService,
            ITimeProvider timeProvider)
        {
            _factory = requirementFactory;
            _repository = requirementRepository;
            _storageService = storageService;
            _timeProvider = timeProvider;
        }

        public Requirement Add(CreateRequirementRequest request)
        {
            var requirement = _factory.Create(request);
            _repository.Add(requirement);

            return requirement;
        }

        public Requirement EditQuantity(Guid id, int quantity)
        {
            if (quantity <= 0)
                throw new InvalidDataProvidedException("Quantity must be more than 0.");

            var requirement = _repository.GetOrDefault(id);
            if (requirement == null)
                throw new EntityNotFoundException($"There is no requirement with id {id}.");
            if (requirement.Status == Status.Completed)
                throw new InvalidDataProvidedException($"Requirement with id {id} is completed.");

            requirement.UpdateQuantity(quantity);
            _repository.Update(requirement);

            return requirement;
        }

        public Requirement Complete(Guid id)
        {
            var time = _timeProvider.Now();

            var requirement = _repository.GetOrDefault(id);
            if (requirement == null)
                throw new EntityNotFoundException($"There is no requirement with id {id}.");
            if (requirement.Status == Status.Completed)
                throw new InvalidDataProvidedException($"Requirement with id {id} is completed.");

            requirement.Complete();
            requirement.SetDate(time);

            _repository.Update(requirement);
            _storageService.Depart(requirement.StorageId, requirement);

            return requirement;
        }
    }
}
