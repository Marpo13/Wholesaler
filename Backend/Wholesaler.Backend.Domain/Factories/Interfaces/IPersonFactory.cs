using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Requests.People;

namespace Wholesaler.Backend.Domain.Factories.Interfaces
{
    public interface IPersonFactory
    {
        Person Create(CreatePersonRequest request);
    }
}
