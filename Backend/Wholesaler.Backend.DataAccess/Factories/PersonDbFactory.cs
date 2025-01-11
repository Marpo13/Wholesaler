using Wholesaler.Backend.Domain.Entities;
using PersonDb = Wholesaler.Backend.DataAccess.Models.Person;

namespace Wholesaler.Backend.DataAccess.Factories;

public class PersonDbFactory : IPersonDbFactory
{
    public Person Create(PersonDb person)
    {
        return new(
            person.Id,
            person.Login,
            person.Password,
            person.Role,
            person.Name,
            person.Surname);
    }
}
