using Wholesaler.Backend.Api.Factories.Interfaces;
using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Core.Dto.ResponseModels;

namespace Wholesaler.Backend.Api.Factories
{
    public class StorageDtoFactory : IStorageDtoFactory
    {
        public StorageDto Create(Storage storage)
        {
            return new StorageDto()
            {
                Id = storage.Id,
                Name = storage.Name,
                State = storage.State,
            };
        }
    }
}
