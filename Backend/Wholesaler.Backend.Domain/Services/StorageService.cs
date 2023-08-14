using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Factories.Interfaces;
using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Repositories;
using Wholesaler.Backend.Domain.Requests.Storage;

namespace Wholesaler.Backend.Domain.Services
{
    public class StorageService : IStorageService
    {
        private readonly IStorageRepository _storageRepository;
        private readonly IStorageFactory _factory;
        private readonly IRequirementRepository _requirementRepository;

        public StorageService(IStorageRepository storageRepository, IStorageFactory factory, IRequirementRepository requirementRepository)
        {
            _storageRepository = storageRepository;
            _factory = factory;
            _requirementRepository = requirementRepository;
        }

        public Storage Add(CreateStorageRequest request)
        {
            var storage = _factory.Create(request);
            _storageRepository.Add(storage);

            return storage;
        }

        public Storage Deliver(Guid storageId, int quantity)
        {            
            var storage = _storageRepository.GetOrDefault(storageId);

            if (storage == null)
                throw new EntityNotFoundException($"There is no storage with id {storageId}");

            if (quantity <= 0)
                throw new InvalidDataProvidedException($"Quantity must be more than 0.");

            var state = storage.State + quantity;
            storage.SetState(state);
            _storageRepository.UpdateState(storage);

            return storage;
        }

        public Storage Depart(Guid storageId, Requirement requirement)
        {
            var storage = _storageRepository.GetOrDefault(storageId);
            if (storage == null)
                throw new InvalidDataProvidedException($"There is no storage with id {storageId}");

            var quantity = requirement.Quantity;

            var state = storage.State - quantity;

            if(state < 0)
                throw new InvalidDataProvidedException($"There are not enough mushrooms. You can only take {storage.State}");

            storage.SetState(state);
            _storageRepository.UpdateState(storage);

            return storage;
        }
    }
}
