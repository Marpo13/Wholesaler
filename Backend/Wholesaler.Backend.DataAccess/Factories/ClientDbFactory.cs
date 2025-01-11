using Wholesaler.Backend.Domain.Entities;
using ClientDb = Wholesaler.Backend.DataAccess.Models.Client;

namespace Wholesaler.Backend.DataAccess.Factories;

public class ClientDbFactory : IClientDbFactory
{
    public Client Create(ClientDb clientDb)
    {
        return new(
            clientDb.Id,
            clientDb.Name,
            clientDb.Surname);
    }
}
