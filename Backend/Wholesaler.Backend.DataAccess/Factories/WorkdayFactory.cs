using Wholesaler.Backend.Domain.Entities;
using WorkdayDb = Wholesaler.Backend.DataAccess.Models.Workday;

namespace Wholesaler.Backend.DataAccess.Factories
{
    public class WorkdayFactory : IWorkdayFactory
    {
        public Workday Create(WorkdayDb workdayDb)
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
        }
    }
}
