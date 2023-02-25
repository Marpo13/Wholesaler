using Wholesaler.Backend.Domain.Exceptions;

namespace Wholesaler.Backend.Domain.Entities
{
    public class WorkTask
    {
        private List<Activity> _activities;

        public Guid Id { get; }
        public int Row { get; }
        public IReadOnlyList<Activity> Activities => _activities;
        public bool IsStarted { get; private set; }
        public bool IsFinished { get; private set; }
        public Person? Person { get; private set; }

        public WorkTask(Guid id, int row, List<Activity> activities, bool isStarted, bool isFinished, Person? person)
        {
            Id = id;
            Row = row;
            _activities = activities;
            IsStarted = isStarted;
            IsFinished = isFinished;
            Person = person;
        }
        public WorkTask(int row)
        {
            Id = Guid.NewGuid();
            Row = row;
            _activities = new List<Activity>();
        }
        public WorkTask(Guid id, int row)
        {
            Id = id;
            Row = row;
            _activities = new List<Activity>();
        }

        public void AssignPerson(Person person)
        {
            if (Activities.Any(a => a.IsOpen))
            {
                var activity = Activities
                    .First(a => a.IsOpen);

                activity.Close();
            }

            Person = person;
        }

        public void Start()
        {
            if (Activities.Any(a => a.IsOpen))
                throw new InvalidDataProvidedException("You can not start another activity, because one is already open.");
            if (Person == null)
                throw new InvalidDataProvidedException("Firstly you need to assign worktask to person."); 

            var activity = new Activity(Guid.NewGuid(), DateTime.Now, null, Person.Id);

            _activities.Add(activity);
        }

        public void Stop()
        {
            var openedActivity = Activities
                .FirstOrDefault(a => a.IsOpen);

            if (openedActivity == default)
                return;
            if (Person == null)
                throw new InvalidDataProvidedException("Firstly you need to assign worktask to person.");
            if (openedActivity.PersonId != Person.Id)
                throw new InvalidDataProvidedException("Person who owns activity is not match with person who owns a task.");
            
            openedActivity.Close();
        }

        public void StartStatus()
        {
            Start();
            IsStarted = true;
        }

        public void FinishStatus()
        {      
            Stop();
            IsFinished = true;
        }
    }
}
