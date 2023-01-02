using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Repositories
{
    public interface IUsersRepository
    {
        Person? GetUserOrDefault(string login);

        Guid AddPerson(Person person);
    }
}
