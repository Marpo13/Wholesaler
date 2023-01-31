using Microsoft.EntityFrameworkCore;
using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Repositories;
using PersonDb = Wholesaler.Backend.DataAccess.Models.Person;
using WorkTaskDb = Wholesaler.Backend.DataAccess.Models.WorkTask;

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

        public WorkTask Get(Guid id)
        {
            var workTaskDb = _context.WorkTasks
                .Include(w => w.Person)
                .Where(w => w.Id == id)
                .FirstOrDefault();

            if (workTaskDb == null)
                throw new InvalidOperationException($"There is no not assigned worktask with id: {id}");

            if (workTaskDb.Person == null)
                return new WorkTask(workTaskDb.Id, workTaskDb.Row);

            var person = new Person(
                    workTaskDb.Person.Login,
                    workTaskDb.Person.Password,
                    workTaskDb.Person.Role,
                    workTaskDb.Person.Name,
                    workTaskDb.Person.Surname);

            return new WorkTask(workTaskDb.Id, workTaskDb.Row, workTaskDb.Start, workTaskDb.Start, person);
        }

        public WorkTask Update(WorkTask workTask)
        {
            var workTaskDb = _context.WorkTasks
                .Include(w => w.Person)
                .Where(w => w.Id == workTask.Id)
                .FirstOrDefault();

            if (workTaskDb == null)
                throw new InvalidOperationException($"There is no worktask with id: {workTask.Id}");

            workTaskDb.PersonId = workTask.Person.Id;

            _context.SaveChanges();

            return workTask;
        }

        public List<WorkTask> GetNotAssign()
        {
            var workTasksDbList = _context.WorkTasks                
                .Where(w => w.Person == null)                
                .ToList();

            var listOfWorkTasks = workTasksDbList.Select(worktaskDb =>
            {
                var workTask = new WorkTask(worktaskDb.Id, worktaskDb.Row);

                return workTask;
            });

            return listOfWorkTasks.ToList();
        }

        public List<WorkTask> GetAssigned(Guid userId)
        {            
            var workTasksDbList = _context.WorkTasks
                .Include(w => w.Person)
                .Where(w => w.PersonId == userId)
                .ToList();

            if (workTasksDbList.Count == 0)
                return new List<WorkTask>();                       

            var listOfWorkTasks = workTasksDbList.Select(workTaskDb =>
            {
                var person = new Person(
                    workTaskDb.Person.Login,
                    workTaskDb.Person.Password,
                    workTaskDb.Person.Role,
                    workTaskDb.Person.Name,
                    workTaskDb.Person.Surname);

                var worktask = new WorkTask(workTaskDb.Id, workTaskDb.Row, workTaskDb.Start, workTaskDb.Stop, person);

                return worktask;
            });

            return listOfWorkTasks.ToList();
        }

    }
}
