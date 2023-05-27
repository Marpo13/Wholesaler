using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Factories.Interfaces;
using Wholesaler.Backend.Domain.Requests.Storage;

namespace Wholesaler.Backend.Domain.Factories
{
    public class StorageFactory : IStorageFactory
    {
        public Storage Create(CreateStorageRequest request)
        {
            if (request.State < 0)
                throw new InvalidDataProvidedException("You need to provide positive state.");
            if (string.IsNullOrEmpty(request.Name))
                throw new InvalidDataProvidedException("You need to provide name.");

            var storage = new Storage(request.State, request.Name);

            return storage;
        }
    }
}
