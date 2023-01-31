using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Repositories
{
    public interface IUsersRepository
    {
        Person? Get(Guid id);

        Person? GetOrDefault(string login);

        Person? GetOrDefault(Guid id);

        Guid AddPerson(Person person);       

        List<Person> GetEmployees();
    }
}
