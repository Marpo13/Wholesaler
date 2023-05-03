using Wholesaler.Backend.Domain.Entities;
using StorageDb = Wholesaler.Backend.DataAccess.Models.Storage;

namespace Wholesaler.Backend.DataAccess.Factories
{
    public class StorageDbFactory : IStorageDbFactory
    {
        public Storage Create(StorageDb storageDb)
        {
            if (storageDb.Requirements == null)
                return new Storage(storageDb.Id, storageDb.State, storageDb.Name, new List<Requirement>());

            var requirements = storageDb.Requirements.Select(r =>
            {
                var requirement = new Requirement(r.Id, r.Quantity, r.ClientId, r.StorageId);
                return requirement;

            }).ToList();

            return new Storage(storageDb.Id, storageDb.State, storageDb.Name, requirements);
        }
    }
}
