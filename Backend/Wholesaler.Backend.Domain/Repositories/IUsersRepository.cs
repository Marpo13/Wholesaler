using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Repositories
{
    public interface IUsersRepository
    {
        Person GetUser(string login);
    }
}
