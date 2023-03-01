using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Repositories;

namespace Wholesaler.Backend.DataAccess.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly WholesalerContext _context;

        public ActivityRepository(WholesalerContext context)
        {
            _context = context;
        }

        public List<Activity> GetActiveByPerson(Guid personId)
        {
            var activitiesDb = _context.Activities
                .Where(a => a.PersonId == personId)
                .Where(a => a.Stop == null)
                .ToList();

            if (activitiesDb == null)
                return new List<Activity>();

            var listOfActivities = activitiesDb.Select(activityDb =>
            {
                var activity = new Activity(activityDb.Id, activityDb.Start, activityDb.Stop, activityDb.PersonId);

                return activity;
            });

            return listOfActivities.ToList();
        }
    }
}
