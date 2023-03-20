using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Requests.People;

namespace Wholesaler.Backend.Domain.Interfaces
{
    public interface IUserService
    {
        Person Login(string username, string password);

        Workday StartWorkday(Guid userId);

        Workday FinishWorkday(Guid userId);

        Person Add(CreatePersonRequest request);

    }
}
