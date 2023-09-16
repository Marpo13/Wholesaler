using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Domain.ValueObjects;

namespace Wholesaler.Frontend.Domain.Interfaces
{
    public interface IStorageRepository
    {
        Task<ExecutionResultGeneric<StorageDto>> Add(string name);
        Task<ExecutionResultGeneric<List<StorageDto>>> GetAllStorages();
        Task<ExecutionResultGeneric<StorageDto>> Deliver(Guid id, int quantity, Guid personId);
    }
}
