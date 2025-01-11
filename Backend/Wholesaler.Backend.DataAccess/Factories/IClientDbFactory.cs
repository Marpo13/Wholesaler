using Wholesaler.Backend.Domain.Entities;
using ClientDb = Wholesaler.Backend.DataAccess.Models.Client;

namespace Wholesaler.Backend.DataAccess.Factories;

public interface IClientDbFactory
{
    Client Create(ClientDb clientDb);
}
