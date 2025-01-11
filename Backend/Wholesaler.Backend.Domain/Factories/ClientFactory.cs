using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Factories.Interfaces;
using Wholesaler.Backend.Domain.Requests.People;

namespace Wholesaler.Backend.Domain.Factories;

public class ClientFactory : IClientFactory
{
    public Client Create(CreateClientRequest request)
    {
        if (string.IsNullOrEmpty(request.Name))
            throw new InvalidDataProvidedException("You need to provide name.");
        if (string.IsNullOrEmpty(request.Surname))
            throw new InvalidDataProvidedException("You need to provide surname.");

        foreach (var letter in request.Name)
        {
            if (char.IsDigit(letter))
                throw new InvalidDataProvidedException("You provided an invalid value.");
        }

        foreach (var letter in request.Surname)
        {
            if (char.IsDigit(letter))
                throw new InvalidDataProvidedException("You provided an invalid value.");
        }

        return new(
            request.Name,
            request.Surname);
    }
}
