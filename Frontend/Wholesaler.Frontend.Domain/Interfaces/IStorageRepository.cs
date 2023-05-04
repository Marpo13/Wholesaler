using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Domain.ValueObjects;

namespace Wholesaler.Frontend.Domain.Interfaces
{
    public interface IStorageRepository
    {
        Task<ExecutionResultGeneric<StorageDto>> Add(string name);
        Task<ExecutionResultGeneric<List<StorageDto>>> GetAllStorages();
        Task<ExecutionResultGeneric<StorageDto>> Delivery(Guid id, int quantity);
        Task<ExecutionResultGeneric<StorageDto>> Departure(Guid id, int quantity);
    }
}
