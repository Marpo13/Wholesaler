using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Repositories
{
    public interface IWorkTaskRepository
    {
        Guid Add(WorkTask worktask);

        WorkTask GetOrDefault(Guid id);

        WorkTask Update(WorkTask worktask);
    }
}
