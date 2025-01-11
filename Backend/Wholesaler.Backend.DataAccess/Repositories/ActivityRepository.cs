using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Repositories;

namespace Wholesaler.Backend.DataAccess.Repositories;

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

        return activitiesDb.ConvertAll(activityDb =>
            new Activity(
            activityDb.Id,
            activityDb.Start,
            activityDb.Stop,
            activityDb.PersonId));
    }
}
