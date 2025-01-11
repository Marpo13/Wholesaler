using Wholesaler.Backend.Api.Factories.Interfaces;
using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Core.Dto.ResponseModels;

namespace Wholesaler.Backend.Api.Factories;

public class ClientFactory : IClientFactory
{
    public ClientDto Create(Client client)
    {
        return new()
        {
            Id = client.Id,
            Name = client.Name,
            Surname = client.Surname
        };
    }
}
