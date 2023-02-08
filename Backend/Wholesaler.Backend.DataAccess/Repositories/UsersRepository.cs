using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Repositories;
using PersonDb = Wholesaler.Backend.DataAccess.Models.Person;


namespace Wholesaler.Backend.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly WholesalerContext _context;
        public UsersRepository(WholesalerContext context)
        {
            _context = context;
        }
        public Person? Get(Guid id)
        {
            var user = _context.People
                .Where(p => p.Id == id)
                .FirstOrDefault();

            if (user == null)
                throw new InvalidProcedureException($"There is no person with id: {id}");

            var person = new Person(user.Id, user.Login, user.Password, user.Role, user.Name, user.Surname);

            return person;
        }

        public Person? GetOrDefault(string login)
        {
            var user = _context.People
                .Where(p => p.Login == login)
                .FirstOrDefault();

            if (user == null)
                return default;

            return new Person(user.Id, user.Login, user.Password, user.Role, user.Name, user.Surname);
        }

        public Person? GetOrDefault(Guid id)
        {
            var user = _context.People
                .Where(p => p.Id == id)
                .FirstOrDefault();

            if (user == null)
                return default;

            var person = new Person(user.Id, user.Login, user.Password, user.Role, user.Name, user.Surname);

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
