using Microsoft.EntityFrameworkCore;
using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Repositories;
using WorkdayDb = Wholesaler.Backend.DataAccess.Models.Workday;

namespace Wholesaler.Backend.DataAccess.Repositories
{
    public class WorkdayRepository : IWorkdayRepository
    {
        private readonly WholesalerContext _context;
        public WorkdayRepository(WholesalerContext context)
        {
            _context = context;
        }

        public Workday? GetOrDefault(Guid id)
        {
            var workdayDb = _context.Workdays
                .Include(w => w.Person)
                .Where(w => w.Id == id)                
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

            var workday = new Workday(workdayDb.Id, workdayDb.Start, workdayDb.Stop, person);

            return workday;
        }

        public List<Workday> GetByPersonAsync(Guid personId)
        {
            var workdayDbList = _context.Workdays
                            .Include(w => w.Person)
                            .Where(w => w.PersonId == personId)
                            .ToList();

            var listOfWorkdays = workdayDbList.Select(workdayDb =>
            {
                var person = new Person(
                workdayDb.Person.Id,
                workdayDb.Person.Login,
                workdayDb.Person.Password,
                workdayDb.Person.Role,
                workdayDb.Person.Name,
                workdayDb.Person.Surname);

                var workday = new Workday(workdayDb.Id, workdayDb.Start, workdayDb.Stop, person);
                return workday;
            });

            return listOfWorkdays.ToList();
        }

        public Workday? GetActiveByPersonOrDefaultAsync(Guid personId)
        {
            var workdayDb = _context.Workdays
                            .Include(w => w.Person)
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

            var workday = new Workday(workdayDb.Id, workdayDb.Start, workdayDb.Stop, person);

            return workday;
        }

        public Workday Add(Workday workday)
        {
            var workdayDb = new WorkdayDb
            {
                Id = workday.Id,
                PersonId = workday.Person.Id,
                Start = workday.Start,
                Stop = workday.Stop,                
            };

            _context.Workdays.Add(workdayDb);
            _context.SaveChanges();

            return workday;
        }

        public Workday UpdateWorkday(Workday workday)
        {
            var workdayDb = _context.Workdays
                .Where(w => w.Id == workday.Id)
                .FirstOrDefault();

            if (workdayDb == null)
                throw new InvalidProcedureException($"There is no workday with id: {workday.Id}");

            workdayDb.Stop = workday.Stop;

            _context.SaveChanges();

            return workday;
        }     

    }
}
