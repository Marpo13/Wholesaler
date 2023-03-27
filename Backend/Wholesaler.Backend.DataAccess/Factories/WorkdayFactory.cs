using Wholesaler.Backend.Domain.Entities;
using WorkdayDb = Wholesaler.Backend.DataAccess.Models.Workday;

namespace Wholesaler.Backend.DataAccess.Factories
{
    public class WorkdayFactory : IWorkdayFactory
    {
        private readonly IPersonDbFactory _personDbFactory;

        public WorkdayFactory(IPersonDbFactory personDbFactory)
        {
            _personDbFactory = personDbFactory;
        }

        public Workday Create(WorkdayDb workdayDb)
        {
            var person = _personDbFactory.Create(workdayDb.Person);

            var workday = new Workday(workdayDb.Id, workdayDb.Start, workdayDb.Stop, person);

            return workday;
        }
    }
}
