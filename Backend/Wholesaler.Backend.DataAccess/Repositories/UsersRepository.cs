using Wholesaler.Backend.DataAccess.Factories;
using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Repositories;
using PersonDb = Wholesaler.Backend.DataAccess.Models.Person;


namespace Wholesaler.Backend.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly WholesalerContext _context;
        private readonly IPersonDbFactory _personFactory;

        public UsersRepository(WholesalerContext context, IPersonDbFactory personFactory)
        {
            _context = context;
            _personFactory = personFactory;
        }
        public Person? Get(Guid id)
        {
            var personDb = _context.People
                .Where(p => p.Id == id)
                .FirstOrDefault();

            if (personDb == null)
                throw new InvalidProcedureException($"There is no person with id: {id}");

            var person = _personFactory.Create(personDb);

            return person;
        }

        public Person? GetOrDefault(string login)
        {
            var personDb = _context.People
                .Where(p => p.Login == login)
                .FirstOrDefault();

            if (personDb == null)
                return default;

            var person = _personFactory.Create(personDb);

            return person;
        }

        public Person? GetOrDefault(Guid id)
        {
            var personDb = _context.People
                .Where(p => p.Id == id)
                .FirstOrDefault();

            if (personDb == null)
                return default;

            var person = _personFactory.Create(personDb);

            return person;
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
                Surname = person.Surname
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

            var listOfEmployees = employees.Select(employeeDb =>
            {
                var employee = new Person(
                    employeeDb.Id,
                    employeeDb.Login,
                    employeeDb.Password,
                    employeeDb.Role,
                    employeeDb.Name,
                    employeeDb.Surname);               

                return employee;
            });

            return listOfEmployees.ToList();
        }
    }
}
