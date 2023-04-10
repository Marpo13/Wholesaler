using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Factories;
using Wholesaler.Backend.Domain.Factories.Interfaces;
using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Repositories;
using Wholesaler.Backend.Domain.Requests.People;

namespace Wholesaler.Backend.Domain.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientFactory _clientFactory;
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientFactory clientFactory, IClientRepository clientRepository)
        {
            _clientFactory = clientFactory;
            _clientRepository = clientRepository;
        }

        public Client Add(CreateClientRequest request)
        {
            var client = _clientFactory.Create(request);
            _clientRepository.Add(client);

            return client;
        }

        public void Delete(Guid id)
        {
            var client = _clientRepository.GetOrDefault(id);
            _clientRepository.Delete(client);
        }

        public Client Get(Guid id)
        {
            var client = _clientRepository.GetOrDefault(id);
            if (client == null)
                throw new InvalidDataProvidedException($"There is no client with id {id}");

            return client;
        }

        public List<Client>? GetAll()
        {
            var clients = _clientRepository.GetAll();
            if (clients == null)
                throw new EntityNotFoundException("There are no clients in this base.");

            return clients;
        }
    }
}
