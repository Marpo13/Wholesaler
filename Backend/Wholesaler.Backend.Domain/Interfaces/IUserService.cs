using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Interfaces
{
    public interface IUserService
    {
        Person Login(string username, string password);

        Workday StartWorkday(Guid userId);

        Guid FinishWorkday(Guid userId);

    }
}
