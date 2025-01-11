using Wholesaler.Backend.Domain.Entities;
using StorageDb = Wholesaler.Backend.DataAccess.Models.Storage;

namespace Wholesaler.Backend.DataAccess.Factories;

public interface IStorageDbFactory
{
    Storage Create(StorageDb storageDb);
}
