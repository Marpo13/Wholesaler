using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Repositories
{
    public interface IClientRepository
    {
        Client Add(Client client);

        Client? GetOrDefault(Guid id);

        void Delete(Client client);
    }
}
