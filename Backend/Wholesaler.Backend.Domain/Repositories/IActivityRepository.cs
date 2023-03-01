using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Repositories
{
    public interface IActivityRepository
    {
        List<Activity> GetActiveByPerson(Guid personId);
    }
}
