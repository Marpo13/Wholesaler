using Wholesaler.Backend.Domain.Entities;
using WorkTaskDb = Wholesaler.Backend.DataAccess.Models.WorkTask;

namespace Wholesaler.Backend.DataAccess.Factories
{
    public class WorkTaskFactory : IWorkTaskFactory
    {
        public List<WorkTask> Create(List<WorkTaskDb> workTasksDbList)
        {
            var workTasks = workTasksDbList.Select(workTaskDb =>
            {
                if (workTaskDb.Person is null)
                    return new WorkTask(
                    workTaskDb.Id,
                    workTaskDb.Row,
                    workTaskDb.IsStarted,
                    workTaskDb.IsFinished);

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
                    return new WorkTask(workTaskDb.Id, workTaskDb.Row, emptyActivities, workTaskDb.IsStarted, workTaskDb.IsFinished, person);
                }

                var activities = workTaskDb.Activities.Select(activityDb =>
                {
                    var activity = new Activity(activityDb.Id, activityDb.Start, activityDb.Stop, activityDb.PersonId);
                    return activity;
                });

                return new WorkTask(
                    workTaskDb.Id,
                    workTaskDb.Row,
                    activities.ToList(),
                    workTaskDb.IsStarted,
                    workTaskDb.IsFinished,
                    person);
            });

            return workTasks.ToList();
        }
    }
}
