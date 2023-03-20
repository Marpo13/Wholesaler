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
        public WorkTask(Guid id, int row, bool isStarted, bool isFinished)
        {
            Id = id;
            Row = row;
            _activities = new List<Activity>();
            IsStarted = isStarted;
            IsFinished = isFinished;
        }       

        public void AssignPerson(Person person)
        {
            if (_activities.Any(a => a.IsOpen))
            {
                var activity = _activities
                    .First(a => a.IsOpen);

                activity.Close(DateTime.Now);
            }

            Person = person;
        }

        public void Start(DateTime time)
        {
            var activity = new Activity(Guid.NewGuid(), time, null, Person.Id);

            IsStarted = true;
            _activities.Add(activity);
        }

        public void Stop(DateTime time)
        {
            var openedActivity = _activities
                .FirstOrDefault(a => a.IsOpen);

            if (openedActivity == default)
                return;
            if (Person == null)
                throw new InvalidDataProvidedException("Firstly you need to assign worktask to person.");
            if (openedActivity.PersonId != Person.Id)
                throw new InvalidDataProvidedException("Person who owns activity is not match with person who owns a task.");
            
            openedActivity.Close(time);
        }

        public void Finish(DateTime time)
        {      
            Stop(time);
            IsFinished = true;
        }
    }
}
