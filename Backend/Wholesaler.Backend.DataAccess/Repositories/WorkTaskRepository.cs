using Microsoft.EntityFrameworkCore;
using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Repositories;
using WorkTaskDb = Wholesaler.Backend.DataAccess.Models.WorkTask;
using ActivityDb = Wholesaler.Backend.DataAccess.Models.Activity;

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
            };

            _context.WorkTasks.Add(workTaskDb);
            _context.SaveChanges();

            return worktask.Id;
        }

        public WorkTask Get(Guid id)
        {
            var workTaskDb = _context.WorkTasks
                .Include(w => w.Person)
                .Include(w => w.Activities)
                .Where(w => w.Id == id)
                .FirstOrDefault();

            if (workTaskDb == null)
                throw new InvalidProcedureException($"There is no not assigned worktask with id: {id}");

            if (workTaskDb.Person == null)
                return new WorkTask(workTaskDb.Id, workTaskDb.Row);

            var person = new Person(
                    workTaskDb.Person.Id,
                    workTaskDb.Person.Login,
                    workTaskDb.Person.Password,
                    workTaskDb.Person.Role,
                    workTaskDb.Person.Name,
                    workTaskDb.Person.Surname);

            if (workTaskDb.Activities == null)
            {
                var emptyActivities = new List<Activity>();
                return new WorkTask(workTaskDb.Id, workTaskDb.Row, emptyActivities, workTaskDb.IsFinished, person);
            }

            var activities = workTaskDb.Activities.Select(activityDb =>
            {
                var activity = new Activity(activityDb.Id, activityDb.Start, activityDb.Stop, activityDb.PersonId);
                return activity;
            });

            return new WorkTask(workTaskDb.Id, workTaskDb.Row, activities.ToList(), workTaskDb.IsFinished, person);
        }

        public WorkTask Update(WorkTask workTask)
        {
            var workTaskDb = _context.WorkTasks
                .Include(w => w.Person)
                .Include(w => w.Activities)
                .Where(w => w.Id == workTask.Id)
                .FirstOrDefault();

            if (workTaskDb == null)
                throw new InvalidProcedureException($"There is no worktask with id: {workTask.Id}");

            if (workTaskDb.Activities == null)
                workTaskDb.Activities = new List<ActivityDb>();

            var activities = workTask.Activities.Select(activity =>
            {                
                var activityDb = workTaskDb.Activities.FirstOrDefault(a => a.Id == activity.Id);                
                if (activityDb == null)
                {
                    var newActivity = new ActivityDb()
                    {
                        Id = activity.Id,
                        Start = activity.Start,
                        Stop = activity.Stop,
                        PersonId = activity.PersonId,
                        WorkTaskId = workTask.Id,
                    };
                    _context.Activity.Add(newActivity);

                    return newActivity;
                }
                activityDb.Start = activity.Start;
                activityDb.Stop = activity.Stop;
                activityDb.PersonId = activity.PersonId;
                
                return activityDb;
            });

            workTaskDb.Row = workTask.Row;
            workTaskDb.Activities = activities.ToList();
            workTaskDb.IsFinished = workTask.IsFinished;
            workTaskDb.PersonId = workTask.Person?.Id;
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

        public List<WorkTask> GetAssign(Guid userId)
        {
            var workTasksDbList = _context.WorkTasks
                .Include(w => w.Person)
                .Include(w => w.Activities)
                .Where(w => w.PersonId == userId)
                .ToList();

            var listOfWorkTasks = workTasksDbList.Select(workTaskDb =>
            {
                var person = new Person(
                    workTaskDb.Person.Id,
                    workTaskDb.Person.Login,
                    workTaskDb.Person.Password,
                    workTaskDb.Person.Role,
                    workTaskDb.Person.Name,
                    workTaskDb.Person.Surname);

                var activities = workTaskDb.Activities.Select(activityDb =>
                {
                    var activity = new Activity(activityDb.Id, activityDb.Start, activityDb.Stop, activityDb.PersonId);
                    return activity;
                });

                var worktask = new WorkTask(workTaskDb.Id, workTaskDb.Row, activities.ToList(), workTaskDb.IsFinished, person);

                return worktask;
            });

            return listOfWorkTasks.ToList();
        }

        public List<WorkTask> GetAssigned()
        {
            var workTasksDbList = _context.WorkTasks
                .Include(w => w.Person)
                .Include(w => w.Activities)
                .Where(w => w.Person != null)
                .ToList();

            var listOfWorkTasks = workTasksDbList.Select(workTaskDb =>
            {
                var person = new Person(
                    workTaskDb.Person.Id,
                    workTaskDb.Person.Login,
                    workTaskDb.Person.Password,
                    workTaskDb.Person.Role,
                    workTaskDb.Person.Name,
                    workTaskDb.Person.Surname);

                var activities = workTaskDb.Activities.Select(activityDb =>
                {
                    var activity = new Activity(activityDb.Id, activityDb.Start, activityDb.Stop, activityDb.PersonId);
                    return activity;
                });

                var workTask = new WorkTask(
                    workTaskDb.Id,
                    workTaskDb.Row,
                    activities.ToList(),
                    workTaskDb.IsFinished,
                    person);

                return workTask;
            });

            return listOfWorkTasks.ToList();
        }
    }
}
