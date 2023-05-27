using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Requests.People;

namespace Wholesaler.Backend.Domain.Interfaces
{
    public interface IClientService
    {
        Client Add(CreateClientRequest request);

        void Delete(Guid id);
    }
}
