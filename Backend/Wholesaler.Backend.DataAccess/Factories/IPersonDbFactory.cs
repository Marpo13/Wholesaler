using Wholesaler.Backend.Domain.Entities;
using PersonDb = Wholesaler.Backend.DataAccess.Models.Person;

namespace Wholesaler.Backend.DataAccess.Factories
{
    public interface IPersonDbFactory
    {
        Person Create(PersonDb person);
    }
}
