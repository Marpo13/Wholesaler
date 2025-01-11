using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Repositories;

public interface IWorkdayRepository
{
    Workday Add(Workday workday);

    Workday Get(Guid id);

    Workday? GetActiveByPersonOrDefault(Guid personId);

    Workday UpdateWorkday(Workday workday);
}
