using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Repositories;
using ClientDb = Wholesaler.Backend.DataAccess.Models.Client;

namespace Wholesaler.Backend.DataAccess.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly WholesalerContext _context;

        public ClientRepository(WholesalerContext context)
        {
            _context = context;
        }

        public Client Add(Client client)
        {
            var clientDb = new ClientDb()
            {
                Id = client.Id,
                Name = client.Name,
                Surname = client.Surname,
            };

            _context.Add(clientDb);
            _context.SaveChanges();

            return client;
        }

        public void Delete(Client client)
        {
            var clientDb = _context.Clients
                .FirstOrDefault(c => c.Id == client.Id);

            if (clientDb == null)
                throw new EntityNotFoundException($"There is no client with id {client.Id}");

            _context.Remove(clientDb);
            _context.SaveChanges();
        }

        public Client? GetOrDefault(Guid id)
        {
            var clientDb = _context.Clients
                .FirstOrDefault(c => c.Id == id);

            if (clientDb == null)
                return default;

            if (clientDb.Requirements == null)
            {
                var emptyRequirements = new List<Requirement>();

                return new Client(clientDb.Id,clientDb.Name,clientDb.Surname, emptyRequirements);
            }

            var requirements = clientDb.Requirements.Select(requirementDb =>
            {
                var requirement = new Requirement(requirementDb.Id, requirementDb.Quantity, requirementDb.ClientId);

                return requirement;

            }).ToList();

            return new Client(clientDb.Id, clientDb.Name, clientDb.Surname, requirements);
        }
    }
}
