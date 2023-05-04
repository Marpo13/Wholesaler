using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Repositories
{
    public interface IStorageRepository
    {
        Storage Add(Storage storage);
        Storage? GetOrDefault(Guid storageId);
        List<Storage>? GetAll();
        Storage UpdateState(Storage storage);
    }
}
