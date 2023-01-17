using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Repositories;
using WorkTaskDb = Wholesaler.Backend.DataAccess.Models.WorkTask;
using PersonDb = Wholesaler.Backend.DataAccess.Models.Person;

namespace Wholesaler.Backend.DataAccess.Repositories
{
    public class WorkTaskRepository : IWorkTaskRepository
    {
        private readonly WholesalerContext _context;

        public WorkTaskRepository(WholesalerContext context)
        {
            _context = context;
        }
        
        public Guid Add(WorkTask worktask)
        {            
            var workTaskDb = new WorkTaskDb()
            {
                Id = worktask.Id,
                Row = worktask.Row,
                Start = worktask.Start,
                Stop = worktask.Stop,                
            };

            _context.Add(workTaskDb);
            _context.SaveChanges();

            return worktask.Id;
        }

        public WorkTask GetOrDefault(Guid id)
        {
            var workTaskDb = _context.WorkTasks                
                .Where(w => w.Id == id)
                .FirstOrDefault();

            if (workTaskDb == null)
                throw default;                      

            return new WorkTask(workTaskDb.Id, workTaskDb.Row);
        }

        public WorkTask Update(WorkTask workTask)
        {
            var workTaskDb = _context.WorkTasks                
                .Where(w => w.Id == workTask.Id)
                .FirstOrDefault();

            if (workTaskDb == null)
                throw default;

            var person = new PersonDb()
            {
                Id = workTask.Person.Id,
                Role = workTask.Person.Role,
                Login = workTask.Person.Login,
                Password = workTask.Person.Password,
                Name = workTask.Person.Name,
                Surname = workTask.Person.Surname,               
            };                

            workTaskDb.Person = person;
            workTaskDb.PersonId = person.Id;

            _context.SaveChanges();

            return workTask;
        }
    }
}
