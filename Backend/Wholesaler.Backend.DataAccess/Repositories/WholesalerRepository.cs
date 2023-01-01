using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Repositories;
using PersonDb = Wholesaler.Backend.DataAccess.Models.Person;
using WorkdayDb = Wholesaler.Backend.DataAccess.Models.Workday;

namespace Wholesaler.Backend.DataAccess.Repositories
{
    public class WholesalerRepository : IUsersRepository
    {
        private readonly WholesalerContext _context; 
        public WholesalerRepository(WholesalerContext context)
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

            return new Person(user.Id, user.Login, user.Password, user.Role, user.Name, user.Surname);
        }

        public Guid AddPerson()
        {
            var person = new PersonDb()
            {
                Id = Guid.NewGuid(),
                Role = 0,
                Login = "Basia",
                Password = "Jabasia",
                Name = "Barbara",
                Surname = "Nowak"
            };

            _context.People.Add(person);           
            _context.SaveChanges();

            return person.Id;
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

            var createdWorkday = _context.Workdays.Add(workdayDb);
            _context.SaveChanges();

            return workday;
        }

    }
}
