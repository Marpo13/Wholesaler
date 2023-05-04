using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Requests.Storage;

namespace Wholesaler.Backend.Domain.Interfaces
{
    public interface IStorageService
    {
        Storage Add (CreateStorageRequest request);
        List<Storage>? GetAll();
        Storage Delivery (Guid storageId, int quantity);
        Storage Departure (Guid storageId, int quantity);
    }
}
