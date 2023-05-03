using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Requests.Storage;

namespace Wholesaler.Backend.Domain.Factories.Interfaces
{
    public interface IStorageFactory
    {
        Storage Create(CreateStorageRequest request);
    }
}
