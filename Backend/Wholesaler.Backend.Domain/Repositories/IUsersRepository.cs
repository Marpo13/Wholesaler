using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Repositories
{
    public interface IUsersRepository
    {
        Person? GetUserOrDefault(string login);

        Person? GetUserOrDefault(Guid id);

        Guid AddPerson();

        Workday AddWorkday(Workday workday);

    }
}
