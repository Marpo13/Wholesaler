using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Repositories;
using PersonDb = Wholesaler.Backend.DataAccess.Models.Person;
using WorkdayDb = Wholesaler.Backend.DataAccess.Models.Workday;

namespace Wholesaler.Backend.DataAccess.Repositories
{
    public class UserRepository : IUsersRepository
    {
        private readonly WholesalerContext _context;
        public UserRepository(WholesalerContext context)
        {
            _context = context;
        }
        public Person? GetUserOrDefault(string login)
        {
            var user = _context.People
                .Where(p => p.Login == login)
                .FirstOrDefault();

            if (user == null)
                return default;

            return new Person(user.Id, user.Login, user.Password, user.Role, user.Name, user.Surname);
        }

        public Person? GetUserOrDefault(Guid id)
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

        public Workday? GetWorkdayOrDefault(Guid personId)
        {
            var workdayDb = _context.Workdays
                            .Where(w => w.PersonId == personId)
                            .Where(w => w.Stop == null)
                            .FirstOrDefault();
            
            if (workdayDb == null)
                return default;

            var person = new Person(
                workdayDb.Person.Id, 
                workdayDb.Person.Login, 
                workdayDb.Person.Password, 
                workdayDb.Person.Role, 
                workdayDb.Person.Name, 
                workdayDb.Person.Surname);

            return new Workday(workdayDb.Id, workdayDb.Start, workdayDb.Stop, person);
        }

        public Workday AddWorkday(Workday workday)
        {
            var workdayDb = new WorkdayDb
            {
                Id = workday.Id,
                PersonId = workday.Person.Id,
                Start = workday.Start,
                Stop = workday.Stop
            };

            _context.Workdays.Add(workdayDb);
            _context.SaveChanges();

            return workday;
        }

        public Guid UpdateWorkday(Guid id, DateTime? stopTime)
        {
            var workdayDb = _context.Workdays
                .Where(w => w.Id == id)
                .First();

            workdayDb.Stop = stopTime;

            _context.SaveChanges();

            return id;
        }
    }
}
