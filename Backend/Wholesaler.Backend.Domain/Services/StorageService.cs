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
        private readonly IStorageRepository _repository;
        private readonly IStorageFactory _factory;

        public StorageService(IStorageRepository repository, IStorageFactory factory)
        {
            _repository = repository;
            _factory = factory;
        }

        public Storage Add(CreateStorageRequest request)
        {
            var storage = _factory.Create(request);
            _repository.Add(storage);

            return storage;
        }

        public List<Storage>? GetAll()
        {
            var storages = _repository.GetAll();
            if (storages == null)
                throw new EntityNotFoundException("There are no storages in this base.");

            return storages;
        }

        public Storage Delivery(Guid storageId, int quantity)
        {
            var storage = _repository.GetOrDefault(storageId);
            if (storage == null)
                throw new InvalidDataProvidedException($"There is no storage with id {storageId}");

            if (quantity < 0)
                throw new InvalidDataProvidedException($"Quantity must be more than 0.");
            var state = storage.State + quantity;
            storage.SetState(state);
            _repository.UpdateState(storage);

            return storage;
        }

        public Storage Departure(Guid storageId, int quantity)
        {
            var storage = _repository.GetOrDefault(storageId);
            if (storage == null)
                throw new InvalidDataProvidedException($"There is no storage with id {storageId}");

            if (quantity < 0)
                throw new InvalidDataProvidedException($"Quantity must be more than 0.");
            var state = storage.State - quantity;

            if(state < 0)
                throw new InvalidDataProvidedException($"There are not enough mushrooms. You can only take {storage.State}");

            storage.SetState(state);
            _repository.UpdateState(storage);

            return storage;
        }
    }
}
