using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Repositories
{
    public interface IUsersRepository
    {
        Person? GetUserOrDefault(string login);

        Person? GetUserOrDefault(Guid id);

        Guid AddPerson(Person person);

        Workday AddWorkday(Workday workday);

        Workday? GetWorkdayOrDefault(Guid id);

        Guid UpdateWorkday(Guid id, DateTime? stopTime);

    }
}
