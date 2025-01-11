using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Factories.Interfaces;
using Wholesaler.Backend.Domain.Requests.People;

namespace Wholesaler.Backend.Domain.Factories;

public class PersonFactory : IPersonFactory
{
    public Person Create(CreatePersonRequest request)
    {
        if (request.Name == null)
            throw new InvalidDataProvidedException("You need to provide name.");
        if (request.Surname == null)
            throw new InvalidDataProvidedException("You need to provide surname.");
        if (request.Login == null)
            throw new InvalidDataProvidedException("You need to provide login.");
        if (request.Role == null)
            throw new InvalidDataProvidedException("You need to provide role.");
        if (request.Password == null)
            throw new InvalidDataProvidedException("You need to provide password.");

        var role = Enum.TryParse(request.Role, out Role result)
            ? result
            : throw new InvalidDataProvidedException("You entered an invalid value for role");

        return new(
            request.Login,
            request.Password,
            role,
            request.Name,
            request.Surname);
    }
}
