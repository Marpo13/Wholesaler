using Wholesaler.Backend.DataAccess.Factories;
using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Repositories;
using ClientDb = Wholesaler.Backend.DataAccess.Models.Client;

namespace Wholesaler.Backend.DataAccess.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly WholesalerContext _context;
        private readonly IClientDbFactory _clientFactory;

        public ClientRepository(IClientDbFactory clientFactory, WholesalerContext context)
        {
            _context = context;
            _clientFactory = clientFactory;
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

        public List<Client> GetAll()
        {
            var clientsDb = _context.Clients
                .ToList();

            if (clientsDb == null)
                return new List<Client>();

            var clients = clientsDb.Select(clientDb =>
            {
                return _clientFactory.Create(clientDb);

            });

            return clients.ToList();
        }

        public Client Get(Guid id)
        {
            var clientDb = _context.Clients
                .FirstOrDefault(c => c.Id == id);

            if (clientDb == null)
                throw new EntityNotFoundException($"There is no client with id {id}");
            
            return _clientFactory.Create(clientDb);
        }
    }
}
