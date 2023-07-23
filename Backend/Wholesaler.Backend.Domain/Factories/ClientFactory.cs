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
            if(request.Name == null || request.Name == string.Empty || request.Name.Contains(" "))
                throw new InvalidDataProvidedException("You need to provide name.");
            if(request.Surname == null || request.Surname == string.Empty || request.Surname.Contains(" "))
                throw new InvalidDataProvidedException("You need to provide surname.");

            foreach (char letter in request.Name)
            {
                if (char.IsDigit(letter))
                    throw new InvalidDataProvidedException("You provided an invalid value.");                
            }

            foreach (char letter in request.Surname)
            {
                if (char.IsDigit(letter))
                    throw new InvalidDataProvidedException("You provided an invalid value.");
            }

            var client = new Client(
                request.Name,
                request.Surname);

            return client;
        }
    }
}
