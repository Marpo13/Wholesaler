using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Factories.Interfaces;
using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Providers.Interfaces;
using Wholesaler.Backend.Domain.Repositories;
using Wholesaler.Backend.Domain.Requests.Storage;

namespace Wholesaler.Backend.Domain.Services
{
    public class StorageService : IStorageService
    {
        private readonly IStorageRepository _storageRepository;
        private readonly IStorageFactory _factory;
        private readonly IRequirementRepository _requirementRepository;
        private readonly IUsersRepository _userRepository;
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly ITransaction _transaction;
        private readonly ITimeProvider _timeProvider;

        public StorageService(IStorageRepository storageRepository, 
            IStorageFactory factory,
            IRequirementRepository requirementRepository,
            IUsersRepository userRepository,
            IDeliveryRepository deliveryRepository,
            ITransaction transaction,
            ITimeProvider timeProvider)
        {
            _storageRepository = storageRepository;
            _factory = factory;
            _requirementRepository = requirementRepository;
            _userRepository = userRepository;
            _deliveryRepository = deliveryRepository;
            _transaction = transaction;
            _timeProvider = timeProvider;
        }

        public Storage Add(CreateStorageRequest request)
        {
            var storage = _factory.Create(request);
            _storageRepository.Add(storage);

            return storage;
        }

        public Storage Deliver(Guid storageId, int quantity, Guid personId)
        {
            var time = _timeProvider.Now();
            _transaction.Begin();

            var storage = _storageRepository.GetOrDefault(storageId);
            var person = _userRepository.GetOrDefault(personId);

            if (storage == null)
                throw new EntityNotFoundException($"There is no storage with id {storageId}");

            if (quantity <= 0)
                throw new InvalidDataProvidedException($"Quantity must be more than 0.");

            if (person == null)
                throw new EntityNotFoundException($"There is no person with id {personId}");

            if (person.Role != Role.Employee)
                throw new UnpermittedActionPerformedException("You need to be an employee to deliver mushrooms.");

            SetStorageState(quantity, storage);
            AddDelivery(quantity, personId, time);

            _transaction.Commit();

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

        private void AddDelivery(int quantity, Guid personId, DateTime time)
        {
            var delivery = new Delivery(quantity, time, personId);
            _deliveryRepository.Add(delivery);
        }

        private void SetStorageState(int quantity, Storage storage)
        {
            var state = storage.State + quantity;
            storage.SetState(state);
            _storageRepository.UpdateState(storage);
        }
    }
}
