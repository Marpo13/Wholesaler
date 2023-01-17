using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Repositories
{
    public interface IWorkdayRepository
    {
        Workday Add(Workday workday);      

        Workday? GetOrDefault(Guid id);

        List<Workday> GetByPersonAsync(Guid personId);

        Workday? GetActiveByPersonOrDefaultAsync(Guid personId);

        Workday UpdateWorkday(Workday workday);
        
    }
}
