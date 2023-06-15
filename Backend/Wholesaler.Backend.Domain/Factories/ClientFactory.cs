using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Factories.Interfaces;
using Wholesaler.Backend.Domain.Requests.People;

namespace Wholesaler.Backend.Domain.Factories
{
    public class ClientFactory : IClientFactory
    {
        public Client Create(CreateClientRequest request)
        {
            if(request.Name == null || request.Name == string.Empty)
                throw new InvalidDataProvidedException("You need to provide name.");
            if(request.Surname == null || request.Surname == string.Empty)
                throw new InvalidDataProvidedException("You need to provide surname.");

            var client = new Client(
                request.Name,
                request.Surname);

            return client;
        }
    }
}
