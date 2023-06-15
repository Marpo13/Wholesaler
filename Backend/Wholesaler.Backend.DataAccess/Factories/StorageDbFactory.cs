using Wholesaler.Backend.Domain.Entities;
using StorageDb = Wholesaler.Backend.DataAccess.Models.Storage;

namespace Wholesaler.Backend.DataAccess.Factories
{
    public class StorageDbFactory : IStorageDbFactory
    {
        public Storage Create(StorageDb storageDb)
        {            
            return new Storage(
                storageDb.Id, 
                storageDb.State, 
                storageDb.Name);
        }
    }
}
