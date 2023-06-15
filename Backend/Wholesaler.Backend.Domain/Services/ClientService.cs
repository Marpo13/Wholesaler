using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
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
            var client = _clientRepository.Get(id);
            _clientRepository.Delete(client);
        }
    }
}
