using Wholesaler.Backend.Domain.Entities;
using PersonDb = Wholesaler.Backend.DataAccess.Models.Person;

namespace Wholesaler.Backend.DataAccess.Factories
{
    public class PersonDbFactory : IPersonDbFactory
    {
        public Person Create(PersonDb personDb)
        {
            var person = new Person(
                personDb.Id,
                personDb.Login,
                personDb.Password,
                personDb.Role,
                personDb.Name,
                personDb.Surname);

            return person;
        }
    }
}
