using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Repositories
{
    public interface IClientRepository
    {
        Client Add(Client client);

        Client Get(Guid id);

        List<Client> GetAll();

        void Delete(Client client);
    }
}
