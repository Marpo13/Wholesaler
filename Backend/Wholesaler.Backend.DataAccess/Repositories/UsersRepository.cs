using Wholesaler.Backend.DataAccess.Factories;
using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Repositories;
using PersonDb = Wholesaler.Backend.DataAccess.Models.Person;

namespace Wholesaler.Backend.DataAccess.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly WholesalerContext _context;
    private readonly IPersonDbFactory _personFactory;
    private readonly IRoleInfoFactory _roleFactory;

    public UsersRepository(WholesalerContext context, IPersonDbFactory personFactory, IRoleInfoFactory roleFactory)
    {
        _context = context;
        _personFactory = personFactory;
        _roleFactory = roleFactory;
    }

    public Person Get(Guid id)
    {
        var personDb = _context.People
            .Where(p => p.Id == id)
            .FirstOrDefault()
            ?? throw new EntityNotFoundException($"There is no person with id: {id}");

        return _personFactory.Create(personDb);
    }

    public Person? GetOrDefault(string login)
    {
        var personDb = _context.People
            .Where(p => p.Login == login)
            .FirstOrDefault();

        return personDb == null
            ? default
            : _personFactory.Create(personDb);
    }

    public Person? GetOrDefault(Guid id)
    {
        var personDb = _context.People
            .Where(p => p.Id == id)
            .FirstOrDefault();

        return personDb == null
            ? default
            : _personFactory.Create(personDb);
    }

    public Guid AddPerson(Person person)
    {
        var personDb = new PersonDb()
        {
            Id = person.Id,
            Role = person.Role,
            Login = person.Login,
            Password = person.Password,
            Name = person.Name,
            Surname = person.Surname,
            RoleInfo = _roleFactory.Create(person.RoleInfo)
        };

        _context.People.Add(personDb);
        _context.SaveChanges();

        return person.Id;
    }

    public List<Person> GetEmployees()
    {
        var employees = _context.People
            .Where(p => p.Role == Role.Employee)
            .ToList();

        return employees.ConvertAll(employeeDb =>
        {
            return new Person(
                employeeDb.Id,
                employeeDb.Login,
                employeeDb.Password,
                employeeDb.Role,
                employeeDb.Name,
                employeeDb.Surname);
        });
    }
}
