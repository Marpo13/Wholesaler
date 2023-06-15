using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Core.Dto.ResponseModels;

namespace Wholesaler.Backend.Api.Factories.Interfaces
{
    public interface IClientFactory
    {
        ClientDto Create(Client client);
    }
}
